using Kezyma.ModOrganizerSetup.Helpers;
using Kezyma.ModOrganizerSetup.Models;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Kezyma.ModOrganizerSetup.Services
{
    public class ModOrganizerService : BaseService
    {
        public const string ModOrganizerVersions = "https://raw.githubusercontent.com/Kezyma/ModOrganizer-Plugins/main/directory/mod_organizer_versions.json";
        public const string ModOrganizerGames = "https://raw.githubusercontent.com/Kezyma/ModOrganizer-Plugins/main/directory/mod_organizer_games.json";
        public static string ModOrganizerDataPath => Path.Combine(DataPath, "MOData");

        private readonly ZipExtractionService _zipExtractionService;

        public ModOrganizerService()
        {
            _zipExtractionService = new ZipExtractionService();
        }

        private List<ModOrganizerVersion> _versions = null;
        public List<ModOrganizerVersion> GetMOVersions()
        {
            if (_versions != null) return _versions;

            if (!Directory.Exists(ModOrganizerDataPath))
            {
                Directory.CreateDirectory(ModOrganizerDataPath);
            }

            var directoryPath = Path.Combine(ModOrganizerDataPath, "versions.json");
            if (!File.Exists(directoryPath) || File.GetLastWriteTimeUtc(directoryPath) < DateTime.UtcNow.AddDays(-1))
            {
                var uri = new Uri(ModOrganizerVersions);
                using var client = new HttpClient();
                var response = client.GetAsync(uri).Result;
                using var fs = new FileStream(directoryPath, FileMode.OpenOrCreate, FileAccess.Write);
                response.Content.CopyToAsync(fs).Wait();
            }

            var manifestTxt = File.ReadAllText(directoryPath);
            _versions = JsonConvert.DeserializeObject<List<ModOrganizerVersion>>(manifestTxt);
            return _versions;
        }

        public async Task DownloadVersion(string version, IProgress<int> progress = null)
        {
            var dlUrl = _versions.FirstOrDefault(x => x.Version == version)?.Url ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(dlUrl))
            {
                var targetPath = Path.Combine(ModOrganizerDataPath, $"ModOrganizer-{version}.7z");
                if (!File.Exists(targetPath))
                {
                    var client = new HttpClient();
                    var stream = client.GetStreamAsync(dlUrl).Result;
                    var request = WebRequest.CreateHttp(dlUrl);
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1";
                    request.Method = "HEAD";
                    using var res = await request.GetResponseAsync();
                    var length = res.ContentLength;
                    using var fs = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write);
                    stream.CopyToAsync(fs, (int)length, progress).Wait();
                }
            }
        }

        public async Task ExtractVersion(string version, string destination, IProgress<int> progress = null)
        {
            var targetPath = Path.Combine(ModOrganizerDataPath, $"ModOrganizer-{version}.7z");
            _zipExtractionService.ExtractZip(targetPath, destination);
        }

        public async Task CopyInstaller(string version, string moPath)
        {
            var downloadDir = $"{moPath}\\downloads";
            if (!Directory.Exists(downloadDir)) Directory.CreateDirectory(downloadDir);
            var targetPath = Path.Combine(ModOrganizerDataPath, $"ModOrganizer-{version}.7z");
            if (File.Exists(targetPath))
            {
                var dlUrl = _versions.FirstOrDefault(x => x.Version == version);
                if (dlUrl != null)
                {
                    var dlFileName = dlUrl.Url.Split("/").Last();
                    var destPath = Path.Combine(downloadDir, dlFileName);
                    File.Copy(targetPath, destPath, true);
                    var metaTxt = $"[General]\r\nremoved=false\r\ninstalled=true\r\ndirectURL={dlUrl.Url}\r\ninstalled=true";
                    var metaPath = destPath + ".meta";
                    File.WriteAllText(metaPath, metaTxt);
                }
            }
        }

        private List<MOGameMap> _mogames;
        public async Task<bool> GameIsValid(string gamePath)
        {
            if (Directory.Exists(gamePath))
            {
                var allFiles = Directory.GetFiles(gamePath, "*.exe", SearchOption.AllDirectories);
                if (_mogames == null)
                {
                    var gameListPath = Path.Combine(ModOrganizerDataPath, "games.json");
                    if (!File.Exists(gameListPath) || File.GetLastWriteTimeUtc(gameListPath) < DateTime.UtcNow.AddDays(-1))
                    {
                        var uri = new Uri(ModOrganizerGames);
                        using var client = new HttpClient();
                        var response = client.GetAsync(uri).Result;
                        using var fs = new FileStream(gameListPath, FileMode.OpenOrCreate, FileAccess.Write);
                        response.Content.CopyToAsync(fs).Wait();
                    }
                    var gameListTxt = File.ReadAllText(gameListPath);
                    _mogames = JsonConvert.DeserializeObject<List<MOGameMap>>(gameListTxt);
                }
                var validExecutables = _mogames.SelectMany(x => x.GameExe).ToArray();
                return allFiles.Any(x => validExecutables.Contains(x.Split("\\").Last()));
            }
            return false;
        }

        public async Task GeneratePortableIni(string gamePath, string moPath, string moVersion)
        {
            if (await GameIsValid(gamePath))
            {
                var allFiles = Directory.GetFiles(gamePath, "*.exe", SearchOption.AllDirectories);
                var executable1 = string.Empty;
                var executable2 = string.Empty;
                var validExecutables = _mogames.SelectMany(x => x.GameExe).ToArray();
                MOGameMap game = _mogames.FirstOrDefault(x => allFiles.Any(f => x.GameExe.Contains(f.Split("\\").Last())));
                if (game != null)
                {
                    var lt = "\r\n";
                    var iniTxt = $"[General]{lt}";
                    iniTxt += $"gameName={game.Name}{lt}";
                    iniTxt += $"selected_profile=@ByteArray(Default){lt}";
                    iniTxt += $"gamePath=@ByteArray({gamePath.Replace("\\", "\\\\")}){lt}";
                    iniTxt += $"version={moVersion}{lt}";
                    iniTxt += $"first_start=false{lt}";
                    iniTxt += lt;

                    var moIniPath = Path.Combine(moPath, "ModOrganizer.ini");
                    File.WriteAllText(moIniPath, iniTxt);
                }
            }
        }

        public async Task CopyStockGame(string moPath, string gamePath, IProgress<int> progress)
        {
            if (Directory.Exists(moPath) && Directory.Exists(gamePath))
            {
                var stockGamePath = Path.Combine(moPath, "game");
                if (!Directory.Exists(stockGamePath)) Directory.CreateDirectory(stockGamePath);
                CopyFilesRecursively(gamePath, stockGamePath, progress);
            }
        }

        private void CopyFilesRecursively(string serverDirectorty, string localDirectory, IProgress<int> progress)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(serverDirectorty, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(serverDirectorty, localDirectory));

            //Copy all the files & Replaces any files with the same name
            var files = Directory.GetFiles(serverDirectorty, "*.*", SearchOption.AllDirectories);

            int processed = 0;
            //Reset progress value to 0.
            progress?.Report(0);

            foreach (string newPath in files)
            {
                File.Copy(newPath, newPath.Replace(serverDirectorty, localDirectory), true);
                //Report in percentage 1-100%. If you have a ton of files, you should reduce the number of calling progress?.Report() e.g. using dividing by modulo.
                progress?.Report((++processed / files.Length) * 100);
            }

            //If files weren't found.
            progress?.Report(100);
        }
    }
}

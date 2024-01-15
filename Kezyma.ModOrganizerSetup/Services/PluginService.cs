using Kezyma.ModOrganizerSetup.Helpers;
using Kezyma.ModOrganizerSetup.Models;
using Newtonsoft.Json;
using System.Net;

namespace Kezyma.ModOrganizerSetup.Services
{
    public class PluginService : BaseService
    {
        public const string PluginDirectory = "https://raw.githubusercontent.com/Kezyma/ModOrganizer-Plugins/main/directory/plugin_directory.json";
        public static string PluginDataPath => Path.Combine(DataPath, "PluginData");
        public static string PluginManifestPath => Path.Combine(PluginDataPath, "Manifests");
        public static string PluginInstallerPath => Path.Combine(PluginDataPath, "Installers");

        private readonly ZipExtractionService _zipExtractionService;

        public NexusApiService NexusApiService { get; set; }
        public PluginService()
        {
            _zipExtractionService = new ZipExtractionService();
        }

        private List<PluginDirectoryItem> _directory = null;
        public List<PluginDirectoryItem> GetPluginDirectory()
        {
            if (_directory != null) return _directory;

            if (!Directory.Exists(PluginDataPath))
            {
                Directory.CreateDirectory(PluginDataPath);
            }

            var directoryPath = Path.Combine(PluginDataPath, "directory.json");
            if (!File.Exists(directoryPath) || File.GetLastWriteTimeUtc(directoryPath) < DateTime.UtcNow.AddDays(-1))
            {
                var uri = new Uri(PluginDirectory);
                using var client = new HttpClient();
                var response = client.GetAsync(uri).Result;
                using var fs = new FileStream(directoryPath, FileMode.OpenOrCreate, FileAccess.Write);
                response.Content.CopyToAsync(fs).Wait();
            }

            var manifestTxt = File.ReadAllText(directoryPath);
            _directory = JsonConvert.DeserializeObject<List<PluginDirectoryItem>>(manifestTxt);
            return _directory;
        }


        private readonly Dictionary<string, PluginManifest> _manifests = new();
        public async Task<PluginManifest> GetPluginManifest(string plugin)
        {
            if (_manifests.ContainsKey(plugin)) return _manifests[plugin];
            var directory = GetPluginDirectory();

            if (!Directory.Exists(PluginManifestPath))
            {
                Directory.CreateDirectory(PluginManifestPath);
            }

            var pluginItem = directory.FirstOrDefault(x => x.Identifier == plugin);
            if (pluginItem != null)
            {
                var manifestPath = Path.Combine(PluginManifestPath, $"{plugin}.json");
                if (!File.Exists(manifestPath) || File.GetLastWriteTimeUtc(manifestPath) < DateTime.UtcNow.AddDays(-1))
                {
                    var uri = new Uri(pluginItem.Manifest);
                    using var client = new HttpClient();
                    var response = await client.GetAsync(uri);
                    using var fs = new FileStream(manifestPath, FileMode.OpenOrCreate, FileAccess.Write);
                    await response.Content.CopyToAsync(fs);
                }

                var manifestTxt = await File.ReadAllTextAsync(manifestPath);
                var manifest = JsonConvert.DeserializeObject<PluginManifest>(manifestTxt);
                _manifests.Add(plugin, manifest);
                return manifest;
            }
            return null;
        }

        public void ConfigurePluginFinder(string moPath)
        {
            var pluginDir = $"{moPath}\\plugins";
            var installDir = new Dictionary<string, PluginFinderInstall>();
            if (Directory.Exists(pluginDir))
            {
                var plugins = new[] { "rootbuilder", "pluginfinder", "profilesync", "reinstaller", "shortcutter", "curationclub", "openmwplayer" };
                foreach (var plugin in plugins)
                {
                    var dir = Path.Combine(pluginDir, plugin);
                    if (Directory.Exists(dir))
                    {
                        var files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
                        var newInst = new PluginFinderInstall
                        {
                            Version = _manifests[plugin].Versions.OrderByDescending(x => x.Released).FirstOrDefault().Version,
                            PluginFiles = files.Select(x => x.Replace(pluginDir, "")).ToArray(),
                            LocaleFiles = new string[] { },
                            DataFiles = new[] { $"data/{plugin}" }
                        };
                        installDir.Add(plugin, newInst);
                    }
                }
                var pluginDataDir = Path.Combine(pluginDir, "data");
                if (!Directory.Exists(pluginDataDir)) Directory.CreateDirectory(pluginDataDir);
                var pfDataDir = Path.Combine(pluginDataDir, "pluginfinder");
                if (!Directory.Exists(pfDataDir)) Directory.CreateDirectory(pfDataDir);
                var pfJson = JsonConvert.SerializeObject(installDir);
                File.WriteAllText(Path.Combine(pfDataDir, "InstalledPlugins.json"), pfJson);

                File.Move(Path.Combine(pluginDir, "pluginfinder", "pluginfinder", "plugin_directory.json"), Path.Combine(pfDataDir, "plugin_directory.json"));
            }
        }

        public async Task CopyInstaller(string plugin, string moPath)
        {
            var downloadDir = $"{moPath}\\downloads";
            if (!Directory.Exists(downloadDir)) Directory.CreateDirectory(downloadDir);
            var pluginItem = await GetPluginManifest(plugin);
            if (pluginItem != null)
            {
                var version = pluginItem.Versions.OrderByDescending(x => x.Released).FirstOrDefault();
                if (version != null)
                {
                    var verUrl = version.DownloadUrl;
                    var verExt = verUrl.Split(".").Last();
                    var generateMeta = true;
                    if (!string.IsNullOrEmpty(verUrl))
                    {
                        var targetPath = Path.Combine(PluginInstallerPath, $"{plugin}-{version.Version}.{verExt}");
                        var destPath = Path.Combine(downloadDir, $"{plugin}-{version.Version}.{verExt}");
                        if (NexusApiService != null)
                        {
                            var plugins = Directory.GetFiles(PluginInstallerPath);
                            var modId = NexusApiService.PluginMods[plugin].ModId;
                            var path = plugins.FirstOrDefault(x => x.Contains($"-{modId}-"));
                            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                            {
                                targetPath = path;
                                destPath = Path.Combine(downloadDir, path.Split("\\").Last());
                                generateMeta = false;
                                if (NexusApiService.Validate())
                                {
                                    var pd = NexusApiService.PluginFile(plugin);
                                    var metaTxt = $"[General]\r\ngameName={NexusApiService.PluginMods[plugin].MetaGame}\r\nmodID={NexusApiService.PluginMods[plugin].ModId}\r\nfileID={pd.file_id}\r\nurl=\r\nname={pd.name}\r\ndescription={pd.description.Replace("\n"," ").Replace("\r", "")}\r\nversion={pd.version}\r\nfileCategory=1\r\nrepository=Nexus\r\ninstalled=true\r\nuninstalled=false\r\nremoved=false\r\ncategory={NexusApiService.PluginMods[plugin].CategoryId}\r\n";
                                    var metaPath = destPath + ".meta";
                                    File.WriteAllText(metaPath, metaTxt);
                                }
                            }
                        }

                        if (File.Exists(targetPath))
                        {
                            File.Copy(targetPath, destPath);
                            if (generateMeta)
                            {
                                var metaTxt = $"[General]\r\nremoved=false\r\ninstalled=true\r\ndirectURL={version.DownloadUrl}\r\ninstalled=true";
                                var metaPath = destPath + ".meta";
                                File.WriteAllText(metaPath, metaTxt);
                            }
                        }
                    }
                }
            }
        }

        public async Task DownloadPlugin(string plugin, IProgress<int> progress = null)
        {
            if (!Directory.Exists(PluginInstallerPath))
            {
                Directory.CreateDirectory(PluginInstallerPath);
            }

            var pluginItem = await GetPluginManifest(plugin);
            if (pluginItem != null)
            {
                var version = pluginItem.Versions.OrderByDescending(x => x.Released).FirstOrDefault();
                if (version != null)
                {
                    var verUrl = version.DownloadUrl;
                    var verExt = verUrl.Split(".").Last();
                    if (!string.IsNullOrEmpty(verUrl))
                    {
                        var targetPath = Path.Combine(PluginInstallerPath, $"{plugin}-{version.Version}.{verExt}");
                        if (NexusApiService != null && NexusApiService.Validate())
                        {
                            verUrl = NexusApiService.PluginDownloadUrl(plugin);
                            var fileName = verUrl.Split("/").Last().Split("?").First();
                            verExt = fileName.Split(".").Last();
                            targetPath = Path.Combine(PluginInstallerPath, fileName);
                        }
                        if (!File.Exists(targetPath))
                        {
                            var url = new Uri(verUrl);
                            var client = new HttpClient();
                            var stream = client.GetStreamAsync(url).Result;
                            var request = WebRequest.CreateHttp(url);
                            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1";
                            request.Method = "HEAD";
                            using var res = await request.GetResponseAsync();
                            var length = res.ContentLength;
                            using var fs = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write);
                            stream.CopyToAsync(fs, (int)length, progress).Wait();
                        }
                    }
                }
            }
        }

        public async Task ExtractPlugin(string plugin, string destination, IProgress<int> progress = null)
        {
            var pluginItem = await GetPluginManifest(plugin);
            if (pluginItem != null)
            {
                var version = pluginItem.Versions.OrderByDescending(x => x.Released).FirstOrDefault();
                if (version != null)
                {
                    var verUrl = version.DownloadUrl;
                    var verExt = verUrl.Split(".").Last();
                    if (!string.IsNullOrEmpty(verUrl))
                    {
                        var targetPath = Path.Combine(PluginInstallerPath, $"{plugin}-{version.Version}.{verExt}");
                        if (NexusApiService != null)
                        {
                            var plugins = Directory.GetFiles(PluginInstallerPath);
                            var modId = NexusApiService.PluginMods[plugin].ModId;
                            var path = plugins.FirstOrDefault(x => x.Contains($"-{modId}-"));
                            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                            {
                                targetPath = path;
                            }
                        }
                        _zipExtractionService.ExtractZip(targetPath, destination);
                    }
                }
            }
            
        }
    }
}

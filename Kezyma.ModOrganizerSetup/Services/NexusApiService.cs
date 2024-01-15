using Kezyma.ModOrganizerSetup.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Services
{
    public class NexusApiService
    {
        private string _apiKey;
        private bool? _valid = null;
        private const string ValidateUrl = "https://api.nexusmods.com/v1/users/validate.json";
        private static string FileListUrl(string gameId, int modId) => $"https://api.nexusmods.com/v1/games/{gameId}/mods/{modId}/files.json";
        private static string FileDownloadUrl(string gameId, int modId, int fileId) => $"https://api.nexusmods.com/v1/games/{gameId}/mods/{modId}/files/{fileId}/download_link.json";
        public Dictionary<string, (string GameId, int ModId, string MetaGame, int CategoryId)> PluginMods = new Dictionary<string, (string GameId, int ModId, string MetaGame, int CategoryId)>
        {
            { "rootbuilder", ("skyrimspecialedition", 31720, "SkyrimSE", 39) },
            { "profilesync", ("skyrimspecialedition", 60690, "SkyrimSE", 39) },
            { "pluginfinder", ("skyrimspecialedition", 59869, "SkyrimSE", 39) },
            { "reinstaller", ("skyrimspecialedition", 59292, "SkyrimSE", 39) },
            { "shortcutter", ("skyrimspecialedition", 59827, "SkyrimSE", 39) },
            { "curationclub", ("skyrimspecialedition", 60552, "SkyrimSE", 39) },
            { "openmwplayer", ("morrowind", 52345, "Morrowind", 15) }
        };

        public NexusApiService() { }

        public bool SetApiKey(string apiKey)
        {
            _apiKey = apiKey;
            return Validate();
        }

        public bool Validate()
        {
            if (_valid.HasValue) return _valid.Value;
            if (string.IsNullOrWhiteSpace(_apiKey)) return false;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("apiKey", _apiKey);
            var res = client.GetStringAsync(ValidateUrl).Result;
            var json = JsonConvert.DeserializeObject<NexusValidation>(res);
            if (json != null)
            {
                _valid = json.is_premium;
            }
            return _valid.Value;
        }

        public NexusFile PluginFile(string plugin)
        {
            if (PluginMods.ContainsKey(plugin))
            {
                var pluginData = PluginMods[plugin];
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("apiKey", _apiKey);
                var res = client.GetStringAsync(FileListUrl(pluginData.GameId, pluginData.ModId)).Result;
                var json = JsonConvert.DeserializeObject<NexusFileList>(res);
                if (json != null)
                {
                    return json.files.FirstOrDefault(x => x.category_name == "MAIN");
                }
            }
            return null;
        }

        public NexusFileDownload PluginDownload(string plugin)
        {
            var primary = PluginFile(plugin);
            if (primary != null)
            {
                var pluginData = PluginMods[plugin];
                var fileId = primary.file_id;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("apiKey", _apiKey);
                var dlRes = client.GetStringAsync(FileDownloadUrl(pluginData.GameId, pluginData.ModId, fileId)).Result;
                var dlJson = JsonConvert.DeserializeObject<List<NexusFileDownload>>(dlRes);
                if (dlJson != null)
                {
                    return dlJson.FirstOrDefault(x => x.short_name == "Nexus CDN");
                }
            }
            return null;
        }

        public string PluginDownloadUrl(string plugin)
        {
            var pd = PluginDownload(plugin);
            if (pd != null)
            {
                return pd.URI;
            }
            return null;
        }
    }
}

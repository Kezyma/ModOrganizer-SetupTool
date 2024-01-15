namespace Kezyma.ModOrganizerSetup.Models
{
    public class InstallParamaters
    {
        public string InstallPath { get; set; }
        public string GamePath { get; set; }
        public string Version { get; set; }
        public List<string> Plugins { get; set; }
        public bool KeepInstallers { get; set; }
        public bool StockGame { get; set; }
        public bool WabbajackMode { get; set; }
        public bool Portable { get; set; }
        public string NexusAPIKey { get; set; }
    }
}
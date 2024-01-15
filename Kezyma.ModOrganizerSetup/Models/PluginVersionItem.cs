namespace Kezyma.ModOrganizerSetup.Models
{
    public class PluginVersionItem
    {
        public string Version { get; set; }
        public DateTime Released { get; set; }  
        public string MinSupport { get; set; }
        public string MaxSupport { get; set; }
        public string MinWorking { get; set; }
        public string MaxWorking { get; set; }
        public List<string> ReleaseNotes { get; set; }
        public string DownloadUrl { get; set; }
        public List<string> PluginPath { get; set; }
        public List<string> LocalePath { get; set; }
        public List<string> DataPath { get; set; }
    }
}

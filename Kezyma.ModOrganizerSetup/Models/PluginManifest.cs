namespace Kezyma.ModOrganizerSetup.Models
{
    public class PluginManifest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string NexusUrl { get; set; }
        public string GithubUrl { get; set; }
        public string DocsUrl { get; set; }
        public List<PluginVersionItem> Versions { get; set; }
    }
}

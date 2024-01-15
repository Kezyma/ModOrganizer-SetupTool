namespace Kezyma.ModOrganizerSetup.Models
{
    public class NexusFile
    {
        public List<int> id { get; set; }
        public object uid { get; set; }
        public int file_id { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }
        public bool is_primary { get; set; }
        public int size { get; set; }
        public string file_name { get; set; }
        public int uploaded_timestamp { get; set; }
        public DateTime uploaded_time { get; set; }
        public string mod_version { get; set; }
        public string external_virus_scan_url { get; set; }
        public string description { get; set; }
        public int size_kb { get; set; }
        public int size_in_bytes { get; set; }
        public object changelog_html { get; set; }
        public string content_preview_link { get; set; }
    }
}

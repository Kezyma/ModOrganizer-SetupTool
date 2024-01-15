namespace Kezyma.ModOrganizerSetup.Models
{
    public class NexusFileUpdate
    {
        public int old_file_id { get; set; }
        public int new_file_id { get; set; }
        public string old_file_name { get; set; }
        public string new_file_name { get; set; }
        public int uploaded_timestamp { get; set; }
        public DateTime uploaded_time { get; set; }
    }
}

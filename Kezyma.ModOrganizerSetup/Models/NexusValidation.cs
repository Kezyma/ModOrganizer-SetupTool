using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Models
{
    public class NexusValidation
    {
        public int user_id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public bool is_premium { get; set; }
        public bool is_supporter { get; set; }
        public string email { get; set; }
        public string profile_url { get; set; }

    }
}

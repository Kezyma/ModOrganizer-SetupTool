using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Models
{
    public class ArgumentCollection
    {
        public string InstallPath { get; set; }
        public string GamePath { get; set; }
        public string MOVersion { get; set; }
        public List<string> Plugins { get; set; }
        public bool StockGame { get; set; }
        public bool Portable { get; set; }
        public bool Cleanup { get; set; }
        public bool Wabbajack { get; set; }
        public string NexusAPIKey { get; set; }
        public bool Install { get; set; }
    }
}

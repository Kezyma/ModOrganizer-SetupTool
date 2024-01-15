using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Models
{
    public class PluginFinderInstall
    {
        public string Version { get; set; }
        public string[] DataFiles { get; set; }
        public string[] LocaleFiles { get; set; }
        public string[] PluginFiles { get; set; } 
    }
}

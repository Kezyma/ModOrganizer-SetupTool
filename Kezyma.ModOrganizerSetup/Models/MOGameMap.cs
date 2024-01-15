using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Models
{
    public class MOGameMap
    {
        public string Name { get; set; }
        public string[] GameExe { get; set; }
        public string GameData { get; set; }
        public int? SteamAppId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Models
{
    public class NexusFileList
    {
        public List<NexusFile> files { get; set; }
        public List<NexusFileUpdate> file_updates { get; set; }
    }
}

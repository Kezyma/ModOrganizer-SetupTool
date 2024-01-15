using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Services
{
    public class BaseService
    {
        public static string DataPath => Path.GetFullPath("Setup");
    }
}

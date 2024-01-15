using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kezyma.ModOrganizerSetup.Services
{
    public class ZipExtractionService : BaseService
    {
        public const string SzaExe = "https://github.com/Kezyma/ModOrganizer-Plugins/raw/main/src/pluginfinder/modules/7za.exe";
        public static string SzaExePath => Path.Combine(DataPath, "7za.exe");

        public void ExtractZip(string zipPath, string destPath)
        {
            if (!File.Exists(SzaExePath))
            {
                Download7za();
            }

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = SzaExePath,
                    CreateNoWindow = true,
                    Arguments = $"x \"{zipPath}\" -o\"{destPath}\" -y"
                }
            };
            proc.Start();
            proc.WaitForExit();
        }

        public void Download7za()
        {
            var uri = new Uri(SzaExe);
            using var client = new HttpClient();
            var response = client.GetAsync(uri).Result;
            using var fs = new FileStream(SzaExePath, FileMode.OpenOrCreate, FileAccess.Write);
            response.Content.CopyToAsync(fs).Wait();
        }

    }
}

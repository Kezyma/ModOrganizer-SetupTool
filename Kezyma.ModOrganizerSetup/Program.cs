using Kezyma.ModOrganizerSetup.Models;

namespace Kezyma.ModOrganizerSetup
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var config = new ArgumentCollection
            {
                Plugins = new List<string>()
            };

            if (args.Contains("-rb")) config.Plugins.Add("rootbuilder");
            if (args.Contains("-ps")) config.Plugins.Add("profilesync");
            if (args.Contains("-pf")) config.Plugins.Add("pluginfinder");
            if (args.Contains("-sc")) config.Plugins.Add("shortcutter");
            if (args.Contains("-ri")) config.Plugins.Add("reinstaller");
            if (args.Contains("-op")) config.Plugins.Add("openmwplayer");
            if (args.Contains("-cc")) config.Plugins.Add("curationclub");

            config.Portable = args.Contains("-portable");
            config.StockGame = args.Contains("-stockgame");
            config.Cleanup = args.Contains("-cleanup");
            config.Wabbajack = args.Contains("-wj");
            config.Install = args.Contains("-run");

            var moVer = args.FirstOrDefault(x => x.StartsWith("-mo:"));
            var instP = args.FirstOrDefault(x => x.StartsWith("-ip:"));
            var gameP = args.FirstOrDefault(x => x.StartsWith("-gp:"));
            var apiK = args.FirstOrDefault(x => x.StartsWith("-nk:"));

            if (moVer != null) config.MOVersion = GetArgParamater(moVer, "-mo:");
            if (instP != null) config.InstallPath = GetArgParamater(instP, "-ip:");
            if (gameP != null) config.GamePath = GetArgParamater(gameP, "-gp:");
            if (apiK != null) config.NexusAPIKey = GetArgParamater(apiK, "-nk:");

            Application.Run(new InstallerTool(config));
        }

        private static string GetArgParamater(string arg, string prefix) 
        {
            if (arg.StartsWith(prefix))
            {
                arg = arg.Substring(prefix.Length);
                if (arg.StartsWith("\"") && arg.EndsWith("\""))
                {
                    arg = arg.Substring(1, arg.Length - 1);
                }
            }
            return arg;
        }
    }
}
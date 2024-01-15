using Kezyma.ModOrganizerSetup.Models;
using Kezyma.ModOrganizerSetup.Services;
using System;
using System.Diagnostics;

namespace Kezyma.ModOrganizerSetup
{
    public partial class InstallerTool : Form
    {
        private readonly ModOrganizerService _moService;
        private readonly PluginService _pluginService;
        private readonly NexusApiService _nexusApiService;
        private string _installPath;
        private string _gamePath;

        public InstallerTool(ArgumentCollection args = null)
        {
            _moService = new ModOrganizerService();
            _pluginService = new PluginService();
            _nexusApiService = new NexusApiService();
            _pluginService.NexusApiService = _nexusApiService;
            InitializeComponent();
            InitialiseInstallerTool();
            if (args != null) ConfigureArgs(args);
        }

        private void ConfigureArgs(ArgumentCollection args)
        {
            if (args != null)
            {
                rootbuilderChk.Checked = args.Plugins.Contains("rootbuilder");
                profilesyncChk.Checked = args.Plugins.Contains("profilesync");
                pluginfinderChk.Checked = args.Plugins.Contains("pluginfinder");
                reinstallerChk.Checked = args.Plugins.Contains("reinstaller");
                shortcutterChk.Checked = args.Plugins.Contains("shortcutter");
                openmwplayerChk.Checked = args.Plugins.Contains("openmwplayer");
                curationclubChk.Checked = args.Plugins.Contains("curationclub");

                keepfilesChk.Checked = !args.Cleanup;
                portableChk.Checked = args.Portable;
                wabbajackChk.Checked = args.Wabbajack;
                stockgameChk.Checked = args.StockGame;

                if (!string.IsNullOrWhiteSpace(args.InstallPath))
                {
                    if (Directory.Exists(args.InstallPath))
                    {
                        _installPath = args.InstallPath;
                        installPathLbl.Text = args.InstallPath;
                    }
                }
                if (!string.IsNullOrWhiteSpace(args.GamePath))
                {
                    if (Directory.Exists(args.GamePath))
                    {
                        if (_moService.GameIsValid(args.GamePath).Result)
                        {
                            _gamePath = args.GamePath;
                            gamePathLbl.Text = args.GamePath;
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(args.NexusAPIKey))
                {
                    nexusapiTxt.Text = args.NexusAPIKey;
                }

                SetGamePathBtn();

                if (args.Install)
                {
                    installBtn_Click(null, null);
                }
            }
        }

        public void InitialiseInstallerTool()
        {
            // Get current MO versions and add them to the dropdown.
            var versions = _moService.GetMOVersions();
            foreach (var version in versions)
            {
                moVersionDdl.Items.Add(version.Version);
            }
            moVersionDdl.SelectedItem = moVersionDdl.Items[0];

            // Get current plugin version info.
            var directory = _pluginService.GetPluginDirectory();
        }

        private void installPathBtn_Click(object sender, EventArgs e)
        {
            using var fb = new FolderBrowserDialog();
            var res = fb.ShowDialog();
            if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(fb.SelectedPath))
            {
                _installPath = fb.SelectedPath;
                installPathLbl.Text = _installPath;
                installBtn.Enabled = true;
            }
            else
            {
                installBtn.Enabled = false;
            }
        }

        private void installBtn_Click(object sender, EventArgs e)
        {
            installBtn.Enabled = false;
            var param = new InstallParamaters
            {
                InstallPath = _installPath,
                GamePath = _gamePath,
                Version = moVersionDdl.SelectedItem as string,
                KeepInstallers = keepfilesChk.Checked,
                Portable = portableChk.Checked,
                StockGame = stockgameChk.Checked,
                WabbajackMode = wabbajackChk.Checked,
                NexusAPIKey = nexusapiTxt.Text,
                Plugins = new List<string>()
            };
            if (rootbuilderChk.Checked) param.Plugins.Add("rootbuilder");
            if (profilesyncChk.Checked) param.Plugins.Add("profilesync");
            if (pluginfinderChk.Checked) param.Plugins.Add("pluginfinder");
            if (reinstallerChk.Checked) param.Plugins.Add("reinstaller");
            if (shortcutterChk.Checked) param.Plugins.Add("shortcutter");
            if (openmwplayerChk.Checked) param.Plugins.Add("openmwplayer");
            if (curationclubChk.Checked) param.Plugins.Add("curationclub");

            installWorker.RunWorkerAsync(param);
        }

        private void UpdateProgress(int progress, string description)
        {
            installProg.Value = progress;
            installLbl.Text = description;
        }

        private void EnableLaunchBtn(string path)
        {
            _lastMoInstallPath = path;
            launchMoBtn.Visible = true;
            launchMoBtn.Enabled = true;
        }

        private void ApiKeyWarning()
        {
            MessageBox.Show("Either your API key is incorrect, you are not a premium user or there is an issue with NexusMods servers. Install will continue using Github sources.", "Nexus API Error");
        }

        private void installWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var param = e.Argument as InstallParamaters;

            if (!string.IsNullOrWhiteSpace(param.NexusAPIKey))
            {
                var useNexus = _nexusApiService.SetApiKey(param.NexusAPIKey);
            }

            // Download selected MO2 version.
            var moDlProg = new Progress<int>(per =>
            {
                Invoke(new Action(() => UpdateProgress(per, $"Downloading MO v{param.Version}")));
            });
            _moService.DownloadVersion(param.Version, moDlProg).Wait();
            Invoke(new Action(() => UpdateProgress(100, $"Downloaded MO v{param.Version}")));

            // Download all selected plugins.
            foreach (var plugin in param.Plugins)
            {
                var pluginDlProg = new Progress<int>(per =>
                {
                    Invoke(new Action(() => UpdateProgress(per, $"Downloading {plugin}")));
                });
                _pluginService.DownloadPlugin(plugin, pluginDlProg).Wait();
                Invoke(new Action(() => UpdateProgress(100, $"Downloaded {plugin}")));
            }

            // Extract MO to the destination.
            Invoke(new Action(() => UpdateProgress(0, $"Extracting MO v{param.Version}")));
            var moExtProg = new Progress<int>(per =>
            {
                Invoke(new Action(() => UpdateProgress(per, $"Extracting MO v{param.Version}")));
            });
            _moService.ExtractVersion(param.Version, param.InstallPath, moExtProg);
            Invoke(new Action(() => UpdateProgress(100, $"Extracted MO v{param.Version}")));

            // Download all selected plugins.
            foreach (var plugin in param.Plugins)
            {
                var pluginExtProg = new Progress<int>(per =>
                {
                    Invoke(new Action(() => UpdateProgress(per, $"Extracting {plugin}")));
                });
                _pluginService.ExtractPlugin(plugin, $"{param.InstallPath}\\plugins\\", pluginExtProg).Wait();
                Invoke(new Action(() => UpdateProgress(100, $"Extracting {plugin}")));
            }

            if (!param.KeepInstallers)
            {
                Directory.Delete(BaseService.DataPath, true);
            }

            if (param.Plugins.Contains("pluginfinder"))
            {
                // Configure pre-installed plugin settings.
                // Invoke(new Action(() => UpdateProgress(0, $"Configuring Plugins")));
                // _pluginService.ConfigurePluginFinder(param.InstallPath);
            }

            if (param.WabbajackMode)
            {
                // Copy all the installers to MO downloads.
                // Generate .meta files for each installer if needed.
                foreach (var plugin in param.Plugins)
                {
                    Invoke(new Action(() => UpdateProgress(0, $"Copying Installers")));
                    _pluginService.CopyInstaller(plugin, param.InstallPath).Wait();
                    _moService.CopyInstaller(param.Version, param.InstallPath).Wait();
                }
            }

            if (param.StockGame && !string.IsNullOrWhiteSpace(param.GamePath))
            {
                // Create the stock game folder and copy the game to it.
                var stockGameProg = new Progress<int>(prog =>
                {
                    Invoke(new Action(() => UpdateProgress(prog, "Copying Game")));
                });
                _moService.CopyStockGame(param.InstallPath, param.GamePath, stockGameProg).Wait();

                // Set the game path to this, so the ini is generated for the stock game.
                param.GamePath = Path.Combine(param.InstallPath, "game");
            }

            if (param.Portable && !string.IsNullOrWhiteSpace(param.GamePath))
            {
                // Generate a ModOrganizer.ini for the current setup.
                Invoke(new Action(() => UpdateProgress(0, $"Generating Config")));
                _moService.GeneratePortableIni(param.GamePath, param.InstallPath, param.Version).Wait();
            }

            Invoke(new Action(() => UpdateProgress(100, $"Install Complete")));
            Invoke(new Action(() => CheckInstallEnabled(true)));
            Invoke(new Action(() => EnableLaunchBtn($"{param.InstallPath}\\ModOrganizer.exe")));
        }

        private void gamePathBtn_Click(object sender, EventArgs e)
        {
            using var fb = new FolderBrowserDialog();
            var res = fb.ShowDialog();
            if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(fb.SelectedPath))
            {
                var isValid = _moService.GameIsValid(fb.SelectedPath).Result;
                if (isValid)
                {
                    _gamePath = fb.SelectedPath;
                    gamePathLbl.Text = _gamePath;
                    CheckInstallEnabled();
                }
                else
                {
                    MessageBox.Show($"The selected game is not supported by this configuration tool.", "Invalid Game");
                }
            }
        }

        private void stockgameChk_CheckedChanged(object sender, EventArgs e)
        {
            SetGamePathBtn();
        }

        private void SetGamePathBtn()
        {
            gamePathBtn.Enabled = stockgameChk.Checked || portableChk.Checked;
            CheckInstallEnabled();
        }

        private void portableChk_CheckedChanged(object sender, EventArgs e)
        {
            SetGamePathBtn();
        }

        private void CheckInstallEnabled(bool overrideWorker = false)
        {
            installBtn.Enabled = !string.IsNullOrWhiteSpace(_installPath) && (!gamePathBtn.Enabled || !string.IsNullOrWhiteSpace(_gamePath)) && (!installWorker.IsBusy || overrideWorker);
        }

        private void nexusapiBtn_Click(object sender, EventArgs e)
        {
            var mb = MessageBox.Show("Login to your NexusMods account, scroll to the bottom and copy your Personal API Key.", "NexusMods API");
            if (mb == DialogResult.OK)
            {
                Process.Start(new ProcessStartInfo("https://www.nexusmods.com/users/myaccount?tab=api") { UseShellExecute = true });
            }
        }

        private void nexusapiTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private string _lastMoInstallPath = null;
        private void launchMoBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_lastMoInstallPath))
            {
                Process.Start(_lastMoInstallPath);
            }
        }
    }
}
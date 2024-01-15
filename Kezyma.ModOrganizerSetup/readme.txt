Settings

Stock Game - Copies the selected game to a folder in Mod Organizer called `game` used for creating completely portable setups or with Wabbajack lists.
Portable Setup - Configures Mod Organizer to run as a portable instance with the selected game. If Stock Game is enabled, it will be configured to run with the copy.
Keep Installers - Saves the downloaded installers to reduce install time if setting up multiple Mod Organizer installations.
Wabbajack - Copies all the downloaded installers to the Mod Organizer downloads directory and generates meta files for them, used if creating Wabbajack lists.
Nexus API Key - If a premium NexusMods API key is provided, installers will be sourced from NexusMods instead of Github. 

Command Line Arguments

-rb (Install Root Builder)
-ps (Install Profile Sync)
-pf (Install Plugin Finder)
-sc (Install Shortcutter)
-ri (Install Reinstaller)
-op (Install OpenMW Player)
-cc (Install Curation Club)

-portable (Configure as a portable instance, requires a game path)
-stockgame (Make a stock game folder, requires a game path)
-cleanup (Delete all downloads after installing)
-wj (Copy installers to downloads and generate meta files for them)
-run (Automatically start the install)

-mo:"x" (Specify the version of Mod Organizer to install)
-ip:"x" (Specify the path to install Mod Organizer to)
-gp:"x" (Specify the path to the game)
-nk:"x" (Specify a premium NexusMods API key)
namespace Kezyma.ModOrganizerSetup
{
    partial class InstallerTool
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallerTool));
            this.installPathBtn = new System.Windows.Forms.Button();
            this.moVersionLbl = new System.Windows.Forms.Label();
            this.installPathLbl = new System.Windows.Forms.Label();
            this.gamePathBtn = new System.Windows.Forms.Button();
            this.gamePathLbl = new System.Windows.Forms.Label();
            this.moVersionDdl = new System.Windows.Forms.ComboBox();
            this.rootbuilderChk = new System.Windows.Forms.CheckBox();
            this.selectPluginsLbl = new System.Windows.Forms.Label();
            this.profilesyncChk = new System.Windows.Forms.CheckBox();
            this.pluginfinderChk = new System.Windows.Forms.CheckBox();
            this.reinstallerChk = new System.Windows.Forms.CheckBox();
            this.shortcutterChk = new System.Windows.Forms.CheckBox();
            this.curationclubChk = new System.Windows.Forms.CheckBox();
            this.openmwplayerChk = new System.Windows.Forms.CheckBox();
            this.optionsLbl = new System.Windows.Forms.Label();
            this.stockgameChk = new System.Windows.Forms.CheckBox();
            this.installBtn = new System.Windows.Forms.Button();
            this.portableChk = new System.Windows.Forms.CheckBox();
            this.keepfilesChk = new System.Windows.Forms.CheckBox();
            this.installLbl = new System.Windows.Forms.Label();
            this.installProg = new System.Windows.Forms.ProgressBar();
            this.installWorker = new System.ComponentModel.BackgroundWorker();
            this.wabbajackChk = new System.Windows.Forms.CheckBox();
            this.nexusapiBtn = new System.Windows.Forms.Button();
            this.nexusapiLbl = new System.Windows.Forms.Label();
            this.nexusapiTxt = new System.Windows.Forms.TextBox();
            this.launchMoBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // installPathBtn
            // 
            this.installPathBtn.Location = new System.Drawing.Point(12, 12);
            this.installPathBtn.Name = "installPathBtn";
            this.installPathBtn.Size = new System.Drawing.Size(108, 23);
            this.installPathBtn.TabIndex = 0;
            this.installPathBtn.Text = "Select Path";
            this.installPathBtn.UseVisualStyleBackColor = true;
            this.installPathBtn.Click += new System.EventHandler(this.installPathBtn_Click);
            // 
            // moVersionLbl
            // 
            this.moVersionLbl.AutoSize = true;
            this.moVersionLbl.Location = new System.Drawing.Point(12, 73);
            this.moVersionLbl.Name = "moVersionLbl";
            this.moVersionLbl.Size = new System.Drawing.Size(127, 15);
            this.moVersionLbl.TabIndex = 1;
            this.moVersionLbl.Text = "Mod Organizer Version";
            // 
            // installPathLbl
            // 
            this.installPathLbl.AutoSize = true;
            this.installPathLbl.Location = new System.Drawing.Point(126, 16);
            this.installPathLbl.Name = "installPathLbl";
            this.installPathLbl.Size = new System.Drawing.Size(16, 15);
            this.installPathLbl.TabIndex = 2;
            this.installPathLbl.Text = "...";
            // 
            // gamePathBtn
            // 
            this.gamePathBtn.Enabled = false;
            this.gamePathBtn.Location = new System.Drawing.Point(12, 41);
            this.gamePathBtn.Name = "gamePathBtn";
            this.gamePathBtn.Size = new System.Drawing.Size(108, 23);
            this.gamePathBtn.TabIndex = 3;
            this.gamePathBtn.Text = "Select Game";
            this.gamePathBtn.UseVisualStyleBackColor = true;
            this.gamePathBtn.Click += new System.EventHandler(this.gamePathBtn_Click);
            // 
            // gamePathLbl
            // 
            this.gamePathLbl.AutoSize = true;
            this.gamePathLbl.Location = new System.Drawing.Point(126, 45);
            this.gamePathLbl.Name = "gamePathLbl";
            this.gamePathLbl.Size = new System.Drawing.Size(16, 15);
            this.gamePathLbl.TabIndex = 4;
            this.gamePathLbl.Text = "...";
            // 
            // moVersionDdl
            // 
            this.moVersionDdl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moVersionDdl.FormattingEnabled = true;
            this.moVersionDdl.Location = new System.Drawing.Point(145, 70);
            this.moVersionDdl.Name = "moVersionDdl";
            this.moVersionDdl.Size = new System.Drawing.Size(67, 23);
            this.moVersionDdl.TabIndex = 5;
            // 
            // rootbuilderChk
            // 
            this.rootbuilderChk.AutoSize = true;
            this.rootbuilderChk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rootbuilderChk.Location = new System.Drawing.Point(101, 102);
            this.rootbuilderChk.Name = "rootbuilderChk";
            this.rootbuilderChk.Size = new System.Drawing.Size(91, 19);
            this.rootbuilderChk.TabIndex = 6;
            this.rootbuilderChk.Text = "Root Builder";
            this.rootbuilderChk.UseVisualStyleBackColor = true;
            // 
            // selectPluginsLbl
            // 
            this.selectPluginsLbl.AutoSize = true;
            this.selectPluginsLbl.Location = new System.Drawing.Point(12, 103);
            this.selectPluginsLbl.Name = "selectPluginsLbl";
            this.selectPluginsLbl.Size = new System.Drawing.Size(80, 15);
            this.selectPluginsLbl.TabIndex = 7;
            this.selectPluginsLbl.Text = "Select Plugins";
            // 
            // profilesyncChk
            // 
            this.profilesyncChk.AutoSize = true;
            this.profilesyncChk.Location = new System.Drawing.Point(101, 127);
            this.profilesyncChk.Name = "profilesyncChk";
            this.profilesyncChk.Size = new System.Drawing.Size(88, 19);
            this.profilesyncChk.TabIndex = 9;
            this.profilesyncChk.Text = "Profile Sync";
            this.profilesyncChk.UseVisualStyleBackColor = true;
            // 
            // pluginfinderChk
            // 
            this.pluginfinderChk.AutoSize = true;
            this.pluginfinderChk.Location = new System.Drawing.Point(101, 152);
            this.pluginfinderChk.Name = "pluginfinderChk";
            this.pluginfinderChk.Size = new System.Drawing.Size(96, 19);
            this.pluginfinderChk.TabIndex = 10;
            this.pluginfinderChk.Text = "Plugin Finder";
            this.pluginfinderChk.UseVisualStyleBackColor = true;
            // 
            // reinstallerChk
            // 
            this.reinstallerChk.AutoSize = true;
            this.reinstallerChk.Location = new System.Drawing.Point(101, 177);
            this.reinstallerChk.Name = "reinstallerChk";
            this.reinstallerChk.Size = new System.Drawing.Size(80, 19);
            this.reinstallerChk.TabIndex = 11;
            this.reinstallerChk.Text = "Reinstaller";
            this.reinstallerChk.UseVisualStyleBackColor = true;
            // 
            // shortcutterChk
            // 
            this.shortcutterChk.AutoSize = true;
            this.shortcutterChk.Location = new System.Drawing.Point(101, 202);
            this.shortcutterChk.Name = "shortcutterChk";
            this.shortcutterChk.Size = new System.Drawing.Size(85, 19);
            this.shortcutterChk.TabIndex = 12;
            this.shortcutterChk.Text = "Shortcutter";
            this.shortcutterChk.UseVisualStyleBackColor = true;
            // 
            // curationclubChk
            // 
            this.curationclubChk.AutoSize = true;
            this.curationclubChk.Location = new System.Drawing.Point(101, 227);
            this.curationclubChk.Name = "curationclubChk";
            this.curationclubChk.Size = new System.Drawing.Size(100, 19);
            this.curationclubChk.TabIndex = 13;
            this.curationclubChk.Text = "Curation Club";
            this.curationclubChk.UseVisualStyleBackColor = true;
            // 
            // openmwplayerChk
            // 
            this.openmwplayerChk.AutoSize = true;
            this.openmwplayerChk.Location = new System.Drawing.Point(101, 252);
            this.openmwplayerChk.Name = "openmwplayerChk";
            this.openmwplayerChk.Size = new System.Drawing.Size(112, 19);
            this.openmwplayerChk.TabIndex = 14;
            this.openmwplayerChk.Text = "OpenMW Player";
            this.openmwplayerChk.UseVisualStyleBackColor = true;
            // 
            // optionsLbl
            // 
            this.optionsLbl.AutoSize = true;
            this.optionsLbl.Location = new System.Drawing.Point(244, 104);
            this.optionsLbl.Name = "optionsLbl";
            this.optionsLbl.Size = new System.Drawing.Size(82, 15);
            this.optionsLbl.TabIndex = 15;
            this.optionsLbl.Text = "Other Options";
            // 
            // stockgameChk
            // 
            this.stockgameChk.AutoSize = true;
            this.stockgameChk.Location = new System.Drawing.Point(333, 103);
            this.stockgameChk.Name = "stockgameChk";
            this.stockgameChk.Size = new System.Drawing.Size(89, 19);
            this.stockgameChk.TabIndex = 16;
            this.stockgameChk.Text = "Stock Game";
            this.stockgameChk.UseVisualStyleBackColor = true;
            this.stockgameChk.CheckedChanged += new System.EventHandler(this.stockgameChk_CheckedChanged);
            // 
            // installBtn
            // 
            this.installBtn.Enabled = false;
            this.installBtn.Location = new System.Drawing.Point(12, 277);
            this.installBtn.Name = "installBtn";
            this.installBtn.Size = new System.Drawing.Size(108, 23);
            this.installBtn.TabIndex = 8;
            this.installBtn.Text = "Install";
            this.installBtn.UseVisualStyleBackColor = true;
            this.installBtn.Click += new System.EventHandler(this.installBtn_Click);
            // 
            // portableChk
            // 
            this.portableChk.AutoSize = true;
            this.portableChk.Location = new System.Drawing.Point(333, 128);
            this.portableChk.Name = "portableChk";
            this.portableChk.Size = new System.Drawing.Size(103, 19);
            this.portableChk.TabIndex = 17;
            this.portableChk.Text = "Portable Setup";
            this.portableChk.UseVisualStyleBackColor = true;
            this.portableChk.CheckedChanged += new System.EventHandler(this.portableChk_CheckedChanged);
            // 
            // keepfilesChk
            // 
            this.keepfilesChk.AutoSize = true;
            this.keepfilesChk.Checked = true;
            this.keepfilesChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepfilesChk.Location = new System.Drawing.Point(333, 153);
            this.keepfilesChk.Name = "keepfilesChk";
            this.keepfilesChk.Size = new System.Drawing.Size(101, 19);
            this.keepfilesChk.TabIndex = 18;
            this.keepfilesChk.Text = "Keep Installers";
            this.keepfilesChk.UseVisualStyleBackColor = true;
            // 
            // installLbl
            // 
            this.installLbl.AutoSize = true;
            this.installLbl.Location = new System.Drawing.Point(232, 281);
            this.installLbl.Name = "installLbl";
            this.installLbl.Size = new System.Drawing.Size(16, 15);
            this.installLbl.TabIndex = 19;
            this.installLbl.Text = "...";
            // 
            // installProg
            // 
            this.installProg.Location = new System.Drawing.Point(126, 277);
            this.installProg.Name = "installProg";
            this.installProg.Size = new System.Drawing.Size(100, 23);
            this.installProg.TabIndex = 20;
            // 
            // installWorker
            // 
            this.installWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.installWorker_DoWork);
            // 
            // wabbajackChk
            // 
            this.wabbajackChk.AutoSize = true;
            this.wabbajackChk.Location = new System.Drawing.Point(333, 178);
            this.wabbajackChk.Name = "wabbajackChk";
            this.wabbajackChk.Size = new System.Drawing.Size(84, 19);
            this.wabbajackChk.TabIndex = 21;
            this.wabbajackChk.Text = "Wabbajack";
            this.wabbajackChk.UseVisualStyleBackColor = true;
            // 
            // nexusapiBtn
            // 
            this.nexusapiBtn.Location = new System.Drawing.Point(333, 202);
            this.nexusapiBtn.Name = "nexusapiBtn";
            this.nexusapiBtn.Size = new System.Drawing.Size(108, 23);
            this.nexusapiBtn.TabIndex = 22;
            this.nexusapiBtn.Text = "Get";
            this.nexusapiBtn.UseVisualStyleBackColor = true;
            this.nexusapiBtn.Click += new System.EventHandler(this.nexusapiBtn_Click);
            // 
            // nexusapiLbl
            // 
            this.nexusapiLbl.AutoSize = true;
            this.nexusapiLbl.Location = new System.Drawing.Point(244, 206);
            this.nexusapiLbl.Name = "nexusapiLbl";
            this.nexusapiLbl.Size = new System.Drawing.Size(83, 15);
            this.nexusapiLbl.TabIndex = 24;
            this.nexusapiLbl.Text = "Nexus API Key";
            // 
            // nexusapiTxt
            // 
            this.nexusapiTxt.Location = new System.Drawing.Point(244, 231);
            this.nexusapiTxt.Name = "nexusapiTxt";
            this.nexusapiTxt.PasswordChar = '*';
            this.nexusapiTxt.Size = new System.Drawing.Size(197, 23);
            this.nexusapiTxt.TabIndex = 25;
            this.nexusapiTxt.TextChanged += new System.EventHandler(this.nexusapiTxt_TextChanged);
            // 
            // launchMoBtn
            // 
            this.launchMoBtn.Location = new System.Drawing.Point(333, 277);
            this.launchMoBtn.Name = "launchMoBtn";
            this.launchMoBtn.Size = new System.Drawing.Size(108, 23);
            this.launchMoBtn.TabIndex = 26;
            this.launchMoBtn.Text = "Launch";
            this.launchMoBtn.UseVisualStyleBackColor = true;
            this.launchMoBtn.Visible = false;
            this.launchMoBtn.Click += new System.EventHandler(this.launchMoBtn_Click);
            // 
            // InstallerTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 312);
            this.Controls.Add(this.launchMoBtn);
            this.Controls.Add(this.nexusapiTxt);
            this.Controls.Add(this.nexusapiLbl);
            this.Controls.Add(this.nexusapiBtn);
            this.Controls.Add(this.wabbajackChk);
            this.Controls.Add(this.installProg);
            this.Controls.Add(this.installLbl);
            this.Controls.Add(this.keepfilesChk);
            this.Controls.Add(this.portableChk);
            this.Controls.Add(this.stockgameChk);
            this.Controls.Add(this.optionsLbl);
            this.Controls.Add(this.openmwplayerChk);
            this.Controls.Add(this.curationclubChk);
            this.Controls.Add(this.shortcutterChk);
            this.Controls.Add(this.reinstallerChk);
            this.Controls.Add(this.pluginfinderChk);
            this.Controls.Add(this.profilesyncChk);
            this.Controls.Add(this.installBtn);
            this.Controls.Add(this.selectPluginsLbl);
            this.Controls.Add(this.rootbuilderChk);
            this.Controls.Add(this.moVersionDdl);
            this.Controls.Add(this.gamePathLbl);
            this.Controls.Add(this.gamePathBtn);
            this.Controls.Add(this.installPathLbl);
            this.Controls.Add(this.moVersionLbl);
            this.Controls.Add(this.installPathBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InstallerTool";
            this.Text = "Kezyma\'s Mod Organizer Setup Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button installPathBtn;
        private Label moVersionLbl;
        private Label installPathLbl;
        private Button gamePathBtn;
        private Label gamePathLbl;
        private ComboBox moVersionDdl;
        private CheckBox rootbuilderChk;
        private Label selectPluginsLbl;
        private CheckBox profilesyncChk;
        private CheckBox pluginfinderChk;
        private CheckBox reinstallerChk;
        private CheckBox shortcutterChk;
        private CheckBox curationclubChk;
        private CheckBox openmwplayerChk;
        private Label optionsLbl;
        private CheckBox stockgameChk;
        private Button installBtn;
        private CheckBox portableChk;
        private CheckBox keepfilesChk;
        private Label installLbl;
        private ProgressBar installProg;
        private System.ComponentModel.BackgroundWorker installWorker;
        private CheckBox wabbajackChk;
        private Button nexusapiBtn;
        private Label nexusapiLbl;
        private TextBox nexusapiTxt;
        private Button launchMoBtn;
    }
}
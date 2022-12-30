namespace CCW.GoW
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.discordStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.discordStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.consoleBox = new System.Windows.Forms.TextBox();
            this.serverList = new System.Windows.Forms.ListBox();
            this.serverListLabel = new System.Windows.Forms.Label();
            this.inviteLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // discordStatus
            // 
            this.discordStatus.Name = "discordStatus";
            this.discordStatus.Size = new System.Drawing.Size(88, 17);
            this.discordStatus.Text = "Not Connected";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileItem
            // 
            this.fileItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configItem,
            this.debugToolsMenuItem,
            this.inviteLinkToolStripMenuItem,
            this.exitItem});
            this.fileItem.Name = "fileItem";
            this.fileItem.Size = new System.Drawing.Size(37, 20);
            this.fileItem.Text = "File";
            // 
            // configItem
            // 
            this.configItem.Name = "configItem";
            this.configItem.Size = new System.Drawing.Size(180, 22);
            this.configItem.Text = "Config";
            this.configItem.Click += new System.EventHandler(this.ConfigMenuItem_Click);
            // 
            // debugToolsMenuItem
            // 
            this.debugToolsMenuItem.Name = "debugToolsMenuItem";
            this.debugToolsMenuItem.Size = new System.Drawing.Size(180, 22);
            this.debugToolsMenuItem.Text = "Debug Tools";
            this.debugToolsMenuItem.Click += new System.EventHandler(this.DebugToolsMenuItem_Click);
            // 
            // exitItem
            // 
            this.exitItem.Name = "exitItem";
            this.exitItem.Size = new System.Drawing.Size(180, 22);
            this.exitItem.Text = "Exit";
            this.exitItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discordStatusLabel,
            this.discordStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // discordStatusLabel
            // 
            this.discordStatusLabel.Name = "discordStatusLabel";
            this.discordStatusLabel.Size = new System.Drawing.Size(106, 17);
            this.discordStatusLabel.Text = "Discord API Status:";
            // 
            // consoleBox
            // 
            this.consoleBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.consoleBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleBox.CausesValidation = false;
            this.consoleBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.consoleBox.Location = new System.Drawing.Point(12, 27);
            this.consoleBox.MaxLength = 0;
            this.consoleBox.Multiline = true;
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.ReadOnly = true;
            this.consoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.consoleBox.ShortcutsEnabled = false;
            this.consoleBox.Size = new System.Drawing.Size(582, 398);
            this.consoleBox.TabIndex = 0;
            this.consoleBox.TabStop = false;
            this.consoleBox.WordWrap = false;
            // 
            // serverList
            // 
            this.serverList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverList.FormattingEnabled = true;
            this.serverList.ItemHeight = 15;
            this.serverList.Location = new System.Drawing.Point(600, 45);
            this.serverList.Name = "serverList";
            this.serverList.Size = new System.Drawing.Size(188, 379);
            this.serverList.TabIndex = 3;
            // 
            // serverListLabel
            // 
            this.serverListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverListLabel.AutoSize = true;
            this.serverListLabel.Location = new System.Drawing.Point(600, 27);
            this.serverListLabel.Name = "serverListLabel";
            this.serverListLabel.Size = new System.Drawing.Size(91, 15);
            this.serverListLabel.TabIndex = 4;
            this.serverListLabel.Text = "Available Guilds";
            // 
            // inviteLinkToolStripMenuItem
            // 
            this.inviteLinkToolStripMenuItem.Name = "inviteLinkToolStripMenuItem";
            this.inviteLinkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.inviteLinkToolStripMenuItem.Text = "Invite Link";
            this.inviteLinkToolStripMenuItem.Click += new System.EventHandler(this.InviteMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.serverListLabel);
            this.Controls.Add(this.serverList);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainWindow";
            this.Text = "Guilds of Warcraft";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip mainMenu;
        private ToolStripMenuItem fileItem;
        private ToolStripMenuItem configItem;
        private ToolStripMenuItem exitItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel discordStatusLabel;
        public ListBox serverList;
        public TextBox consoleBox;
        public ToolStripStatusLabel discordStatus;
        private Label serverListLabel;
        private ToolStripMenuItem debugToolsMenuItem;
        private ToolStripMenuItem inviteLinkToolStripMenuItem;
    }
}
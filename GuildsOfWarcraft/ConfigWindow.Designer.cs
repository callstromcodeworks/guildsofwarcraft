namespace CCW.GoW
{
    partial class ConfigWindow
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
            this.ClientIdLabel = new System.Windows.Forms.Label();
            this.ClientIdTextBox = new System.Windows.Forms.TextBox();
            this.ClientSecretLabel = new System.Windows.Forms.Label();
            this.ClientSecretTextBox = new System.Windows.Forms.TextBox();
            this.CommandListListBox = new System.Windows.Forms.ListBox();
            this.CommandListLabel = new System.Windows.Forms.Label();
            this.DiscordGroupBox = new System.Windows.Forms.GroupBox();
            this.CommandGroupBox = new System.Windows.Forms.GroupBox();
            this.AddCommandButton = new System.Windows.Forms.Button();
            this.RemoveCommandButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelConfigButton = new System.Windows.Forms.Button();
            this.DiscordGroupBox.SuspendLayout();
            this.CommandGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClientIdLabel
            // 
            this.ClientIdLabel.AutoSize = true;
            this.ClientIdLabel.Location = new System.Drawing.Point(6, 25);
            this.ClientIdLabel.Name = "ClientIdLabel";
            this.ClientIdLabel.Size = new System.Drawing.Size(55, 15);
            this.ClientIdLabel.TabIndex = 0;
            this.ClientIdLabel.Text = "Client ID:";
            // 
            // ClientIdTextBox
            // 
            this.ClientIdTextBox.Location = new System.Drawing.Point(94, 22);
            this.ClientIdTextBox.Name = "ClientIdTextBox";
            this.ClientIdTextBox.Size = new System.Drawing.Size(194, 23);
            this.ClientIdTextBox.TabIndex = 1;
            // 
            // ClientSecretLabel
            // 
            this.ClientSecretLabel.AutoSize = true;
            this.ClientSecretLabel.Location = new System.Drawing.Point(6, 71);
            this.ClientSecretLabel.Name = "ClientSecretLabel";
            this.ClientSecretLabel.Size = new System.Drawing.Size(76, 15);
            this.ClientSecretLabel.TabIndex = 0;
            this.ClientSecretLabel.Text = "Client Secret:";
            // 
            // ClientSecretTextBox
            // 
            this.ClientSecretTextBox.Location = new System.Drawing.Point(96, 68);
            this.ClientSecretTextBox.Name = "ClientSecretTextBox";
            this.ClientSecretTextBox.Size = new System.Drawing.Size(194, 23);
            this.ClientSecretTextBox.TabIndex = 2;
            // 
            // CommandListListBox
            // 
            this.CommandListListBox.FormattingEnabled = true;
            this.CommandListListBox.ItemHeight = 15;
            this.CommandListListBox.Location = new System.Drawing.Point(108, 32);
            this.CommandListListBox.Name = "CommandListListBox";
            this.CommandListListBox.Size = new System.Drawing.Size(354, 244);
            this.CommandListListBox.TabIndex = 3;
            // 
            // CommandListLabel
            // 
            this.CommandListLabel.AutoSize = true;
            this.CommandListLabel.Location = new System.Drawing.Point(6, 32);
            this.CommandListLabel.Name = "CommandListLabel";
            this.CommandListLabel.Size = new System.Drawing.Size(88, 15);
            this.CommandListLabel.TabIndex = 0;
            this.CommandListLabel.Text = "Command List:";
            // 
            // DiscordGroupBox
            // 
            this.DiscordGroupBox.Controls.Add(this.ClientIdTextBox);
            this.DiscordGroupBox.Controls.Add(this.ClientIdLabel);
            this.DiscordGroupBox.Controls.Add(this.ClientSecretTextBox);
            this.DiscordGroupBox.Controls.Add(this.ClientSecretLabel);
            this.DiscordGroupBox.Location = new System.Drawing.Point(12, 28);
            this.DiscordGroupBox.Name = "DiscordGroupBox";
            this.DiscordGroupBox.Size = new System.Drawing.Size(462, 112);
            this.DiscordGroupBox.TabIndex = 3;
            this.DiscordGroupBox.TabStop = false;
            this.DiscordGroupBox.Text = "Discord API";
            // 
            // CommandGroupBox
            // 
            this.CommandGroupBox.Controls.Add(this.AddCommandButton);
            this.CommandGroupBox.Controls.Add(this.RemoveCommandButton);
            this.CommandGroupBox.Controls.Add(this.CommandListLabel);
            this.CommandGroupBox.Controls.Add(this.CommandListListBox);
            this.CommandGroupBox.Location = new System.Drawing.Point(12, 146);
            this.CommandGroupBox.Name = "CommandGroupBox";
            this.CommandGroupBox.Size = new System.Drawing.Size(462, 320);
            this.CommandGroupBox.TabIndex = 4;
            this.CommandGroupBox.TabStop = false;
            this.CommandGroupBox.Text = "Commands";
            // 
            // AddCommandButton
            // 
            this.AddCommandButton.Location = new System.Drawing.Point(404, 291);
            this.AddCommandButton.Name = "AddCommandButton";
            this.AddCommandButton.Size = new System.Drawing.Size(23, 23);
            this.AddCommandButton.TabIndex = 4;
            this.AddCommandButton.Text = "+";
            this.AddCommandButton.UseVisualStyleBackColor = true;
            this.AddCommandButton.Click += new System.EventHandler(this.AddCommandButton_Click);
            // 
            // RemoveCommandButton
            // 
            this.RemoveCommandButton.Location = new System.Drawing.Point(433, 291);
            this.RemoveCommandButton.Name = "RemoveCommandButton";
            this.RemoveCommandButton.Size = new System.Drawing.Size(23, 23);
            this.RemoveCommandButton.TabIndex = 5;
            this.RemoveCommandButton.Text = "-";
            this.RemoveCommandButton.UseVisualStyleBackColor = true;
            this.RemoveCommandButton.Click += new System.EventHandler(this.RemoveCommandButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(340, 516);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(64, 23);
            this.SaveButton.TabIndex = 6;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // CancelConfigButton
            // 
            this.CancelConfigButton.Location = new System.Drawing.Point(410, 516);
            this.CancelConfigButton.Name = "CancelConfigButton";
            this.CancelConfigButton.Size = new System.Drawing.Size(64, 23);
            this.CancelConfigButton.TabIndex = 7;
            this.CancelConfigButton.Text = "Cancel";
            this.CancelConfigButton.UseVisualStyleBackColor = true;
            // 
            // ConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 551);
            this.Controls.Add(this.CancelConfigButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CommandGroupBox);
            this.Controls.Add(this.DiscordGroupBox);
            this.Name = "ConfigWindow";
            this.Text = "ConfigWindow";
            this.DiscordGroupBox.ResumeLayout(false);
            this.DiscordGroupBox.PerformLayout();
            this.CommandGroupBox.ResumeLayout(false);
            this.CommandGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label ClientIdLabel;
        private TextBox ClientIdTextBox;
        private Label ClientSecretLabel;
        private TextBox ClientSecretTextBox;
        private ListBox CommandListListBox;
        private Label CommandListLabel;
        private GroupBox DiscordGroupBox;
        private GroupBox CommandGroupBox;
        private Button AddCommandButton;
        private Button RemoveCommandButton;
        private Button SaveButton;
        private Button CancelConfigButton;
    }
}
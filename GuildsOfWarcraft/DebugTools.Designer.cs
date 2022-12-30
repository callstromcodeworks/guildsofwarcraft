namespace CCW.GoW
{
    partial class DebugTools
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
            this.authCodeLabel = new System.Windows.Forms.Label();
            this.authCodeTextBox = new System.Windows.Forms.TextBox();
            this.tokenLabel = new System.Windows.Forms.Label();
            this.tokenTextBox = new System.Windows.Forms.TextBox();
            this.clientCredTokenLabel = new System.Windows.Forms.Label();
            this.clientCredTokenTextBox = new System.Windows.Forms.TextBox();
            this.getAuthCodeButton = new System.Windows.Forms.Button();
            this.getTokenButton = new System.Windows.Forms.Button();
            this.getCliCredTokenButton = new System.Windows.Forms.Button();
            this.queryTextBox = new System.Windows.Forms.TextBox();
            this.queryResultsTextBox = new System.Windows.Forms.TextBox();
            this.sendRequestButton = new System.Windows.Forms.Button();
            this.queryLabel = new System.Windows.Forms.Label();
            this.queryResultsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // authCodeLabel
            // 
            this.authCodeLabel.AutoSize = true;
            this.authCodeLabel.Location = new System.Drawing.Point(18, 24);
            this.authCodeLabel.Name = "authCodeLabel";
            this.authCodeLabel.Size = new System.Drawing.Size(67, 15);
            this.authCodeLabel.TabIndex = 0;
            this.authCodeLabel.Text = "Auth Code:";
            // 
            // authCodeTextBox
            // 
            this.authCodeTextBox.Location = new System.Drawing.Point(91, 21);
            this.authCodeTextBox.Name = "authCodeTextBox";
            this.authCodeTextBox.Size = new System.Drawing.Size(100, 23);
            this.authCodeTextBox.TabIndex = 1;
            // 
            // tokenLabel
            // 
            this.tokenLabel.AutoSize = true;
            this.tokenLabel.Location = new System.Drawing.Point(44, 66);
            this.tokenLabel.Name = "tokenLabel";
            this.tokenLabel.Size = new System.Drawing.Size(41, 15);
            this.tokenLabel.TabIndex = 2;
            this.tokenLabel.Text = "Token:";
            // 
            // tokenTextBox
            // 
            this.tokenTextBox.Location = new System.Drawing.Point(91, 63);
            this.tokenTextBox.Name = "tokenTextBox";
            this.tokenTextBox.Size = new System.Drawing.Size(100, 23);
            this.tokenTextBox.TabIndex = 3;
            // 
            // clientCredTokenLabel
            // 
            this.clientCredTokenLabel.AutoSize = true;
            this.clientCredTokenLabel.Location = new System.Drawing.Point(226, 24);
            this.clientCredTokenLabel.Name = "clientCredTokenLabel";
            this.clientCredTokenLabel.Size = new System.Drawing.Size(41, 15);
            this.clientCredTokenLabel.TabIndex = 4;
            this.clientCredTokenLabel.Text = "Token:";
            // 
            // clientCredTokenTextBox
            // 
            this.clientCredTokenTextBox.Location = new System.Drawing.Point(273, 21);
            this.clientCredTokenTextBox.Name = "clientCredTokenTextBox";
            this.clientCredTokenTextBox.Size = new System.Drawing.Size(100, 23);
            this.clientCredTokenTextBox.TabIndex = 5;
            // 
            // getAuthCodeButton
            // 
            this.getAuthCodeButton.Location = new System.Drawing.Point(10, 165);
            this.getAuthCodeButton.Name = "getAuthCodeButton";
            this.getAuthCodeButton.Size = new System.Drawing.Size(123, 23);
            this.getAuthCodeButton.TabIndex = 6;
            this.getAuthCodeButton.Text = "Get Auth Code";
            this.getAuthCodeButton.UseVisualStyleBackColor = true;
            this.getAuthCodeButton.Click += new System.EventHandler(this.GetAuthCodeButton_Click);
            // 
            // getTokenButton
            // 
            this.getTokenButton.Location = new System.Drawing.Point(10, 194);
            this.getTokenButton.Name = "getTokenButton";
            this.getTokenButton.Size = new System.Drawing.Size(121, 23);
            this.getTokenButton.TabIndex = 7;
            this.getTokenButton.Text = "Get Token";
            this.getTokenButton.UseVisualStyleBackColor = true;
            this.getTokenButton.Click += new System.EventHandler(this.GetTokenButton_Click);
            // 
            // getCliCredTokenButton
            // 
            this.getCliCredTokenButton.Location = new System.Drawing.Point(252, 165);
            this.getCliCredTokenButton.Name = "getCliCredTokenButton";
            this.getCliCredTokenButton.Size = new System.Drawing.Size(121, 23);
            this.getCliCredTokenButton.TabIndex = 8;
            this.getCliCredTokenButton.Text = "Get Token";
            this.getCliCredTokenButton.UseVisualStyleBackColor = true;
            this.getCliCredTokenButton.Click += new System.EventHandler(this.GetCliCredTokenButton_Click);
            // 
            // queryTextBox
            // 
            this.queryTextBox.Location = new System.Drawing.Point(10, 287);
            this.queryTextBox.Name = "queryTextBox";
            this.queryTextBox.Size = new System.Drawing.Size(236, 23);
            this.queryTextBox.TabIndex = 9;
            // 
            // queryResultsTextBox
            // 
            this.queryResultsTextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.queryResultsTextBox.Location = new System.Drawing.Point(252, 287);
            this.queryResultsTextBox.MaxLength = 0;
            this.queryResultsTextBox.Multiline = true;
            this.queryResultsTextBox.Name = "queryResultsTextBox";
            this.queryResultsTextBox.ReadOnly = true;
            this.queryResultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.queryResultsTextBox.ShortcutsEnabled = false;
            this.queryResultsTextBox.Size = new System.Drawing.Size(339, 151);
            this.queryResultsTextBox.TabIndex = 0;
            this.queryResultsTextBox.TabStop = false;
            // 
            // sendRequestButton
            // 
            this.sendRequestButton.Location = new System.Drawing.Point(171, 316);
            this.sendRequestButton.Name = "sendRequestButton";
            this.sendRequestButton.Size = new System.Drawing.Size(75, 23);
            this.sendRequestButton.TabIndex = 10;
            this.sendRequestButton.Text = "Send";
            this.sendRequestButton.UseVisualStyleBackColor = true;
            this.sendRequestButton.Click += new System.EventHandler(this.SendRequestButton_Click);
            // 
            // queryLabel
            // 
            this.queryLabel.AutoSize = true;
            this.queryLabel.Location = new System.Drawing.Point(10, 269);
            this.queryLabel.Name = "queryLabel";
            this.queryLabel.Size = new System.Drawing.Size(42, 15);
            this.queryLabel.TabIndex = 11;
            this.queryLabel.Text = "Query:";
            // 
            // queryResultsLabel
            // 
            this.queryResultsLabel.AutoSize = true;
            this.queryResultsLabel.Location = new System.Drawing.Point(252, 269);
            this.queryResultsLabel.Name = "queryResultsLabel";
            this.queryResultsLabel.Size = new System.Drawing.Size(82, 15);
            this.queryResultsLabel.TabIndex = 12;
            this.queryResultsLabel.Text = "Query Results:";
            // 
            // DebugTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.queryResultsLabel);
            this.Controls.Add(this.queryLabel);
            this.Controls.Add(this.sendRequestButton);
            this.Controls.Add(this.queryResultsTextBox);
            this.Controls.Add(this.queryTextBox);
            this.Controls.Add(this.getCliCredTokenButton);
            this.Controls.Add(this.getTokenButton);
            this.Controls.Add(this.getAuthCodeButton);
            this.Controls.Add(this.clientCredTokenTextBox);
            this.Controls.Add(this.clientCredTokenLabel);
            this.Controls.Add(this.tokenTextBox);
            this.Controls.Add(this.tokenLabel);
            this.Controls.Add(this.authCodeTextBox);
            this.Controls.Add(this.authCodeLabel);
            this.Name = "DebugTools";
            this.Text = "DebugTools";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label authCodeLabel;
        private TextBox authCodeTextBox;
        private Label tokenLabel;
        private TextBox tokenTextBox;
        private Label clientCredTokenLabel;
        private TextBox clientCredTokenTextBox;
        private Button getAuthCodeButton;
        private Button getTokenButton;
        private Button getCliCredTokenButton;
        private TextBox queryTextBox;
        private TextBox queryResultsTextBox;
        private Button sendRequestButton;
        private Label queryLabel;
        private Label queryResultsLabel;
    }
}
namespace CCW.GoW
{
    partial class AddCommandDialog
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
            this.CommandLabel = new System.Windows.Forms.Label();
            this.CommandTextBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.CancelAddButton = new System.Windows.Forms.Button();
            this.CommandEmptyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CommandLabel
            // 
            this.CommandLabel.AutoSize = true;
            this.CommandLabel.Location = new System.Drawing.Point(12, 9);
            this.CommandLabel.Name = "CommandLabel";
            this.CommandLabel.Size = new System.Drawing.Size(67, 15);
            this.CommandLabel.TabIndex = 0;
            this.CommandLabel.Text = "Command:";
            // 
            // CommandTextBox
            // 
            this.CommandTextBox.Location = new System.Drawing.Point(12, 27);
            this.CommandTextBox.Name = "CommandTextBox";
            this.CommandTextBox.Size = new System.Drawing.Size(236, 23);
            this.CommandTextBox.TabIndex = 1;
            this.CommandTextBox.TextChanged += new System.EventHandler(this.CommandTextBox_TextChanged);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(92, 86);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // CancelAddButton
            // 
            this.CancelAddButton.Location = new System.Drawing.Point(173, 86);
            this.CancelAddButton.Name = "CancelAddButton";
            this.CancelAddButton.Size = new System.Drawing.Size(75, 23);
            this.CancelAddButton.TabIndex = 3;
            this.CancelAddButton.Text = "Cancel";
            this.CancelAddButton.UseVisualStyleBackColor = true;
            this.CancelAddButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CommandEmptyLabel
            // 
            this.CommandEmptyLabel.AutoSize = true;
            this.CommandEmptyLabel.ForeColor = System.Drawing.Color.Red;
            this.CommandEmptyLabel.Location = new System.Drawing.Point(10, 53);
            this.CommandEmptyLabel.Name = "CommandEmptyLabel";
            this.CommandEmptyLabel.Size = new System.Drawing.Size(157, 15);
            this.CommandEmptyLabel.TabIndex = 3;
            this.CommandEmptyLabel.Text = "Command cannot be empty";
            this.CommandEmptyLabel.Visible = false;
            // 
            // AddCommandDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 121);
            this.Controls.Add(this.CommandEmptyLabel);
            this.Controls.Add(this.CancelAddButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.CommandTextBox);
            this.Controls.Add(this.CommandLabel);
            this.Name = "AddCommandDialog";
            this.Text = "Add Command";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label CommandLabel;
        private TextBox CommandTextBox;
        private Button AddButton;
        private Button CancelAddButton;
        private Label CommandEmptyLabel;
    }
}
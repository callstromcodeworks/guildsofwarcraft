using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCW.GoW
{
    public partial class AddCommandDialog : Form
    {
        public string Command { get; private set; } = string.Empty;
        public AddCommandDialog()
        {
            InitializeComponent();
            CancelButton = CancelAddButton;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (CommandTextBox.Text == string.Empty) { CommandEmptyLabel.Visible = true; return; }
            Command = CommandTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CommandTextBox_TextChanged(object sender, EventArgs e)
        {
            CommandEmptyLabel.Visible = false;
        }
    }
}

/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using System.Runtime.InteropServices;
using CCW.GoW.Services;

namespace CCW.GoW
{
    //TODO clean up window, maybe add more details
    //TODO Add BlizzApi test form
    public partial class MainWindow : Form
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        public MainWindow()
        {
            InitializeComponent();
            HideCaret(consoleBox.Handle);
            consoleBox.GotFocus += (sender, e) => HideCaret(consoleBox.Handle);
            Console.SetOut(new ConsoleWriter(consoleBox));
#if !DEBUG
            debugToolsMenuItem.Visible = false;
#endif
        }

        private void ConfigMenuItem_Click(object sender, EventArgs e)
        {
            //TODO add config form and serialization

        }
        private void DebugToolsMenuItem_Click(object sender, EventArgs e)
        {
#if DEBUG
            DebugTools debugTools = new()
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            debugTools.ShowDialog();
#endif
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            //TODO add confirmation (don't want accidental shutdown)
            Environment.Exit(0);
        }

    }
}

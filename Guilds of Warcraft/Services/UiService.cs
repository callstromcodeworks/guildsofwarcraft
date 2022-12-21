/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

namespace CCW.GoW.Services
{
    internal class UiService
    {
        internal ListBox serverList;
        internal TextBox consoleBox;
        internal ToolStripLabel discordStatus;
        public UiService(MainWindow window)
        {
            serverList = window.serverList;
            consoleBox = window.consoleBox;
            discordStatus = window.discordStatus;
        }
        private delegate Task SetServerListDataSourceDelegate(ref List<ServerConfig> dataSource);
        private SetServerListDataSourceDelegate GetDelegate()
        {
            return delegate(ref List<ServerConfig> dataSource)
                {
                    serverList.DataSource = dataSource;
                    serverList.DisplayMember = "Name";
                    return Task.CompletedTask;
                };
        }

    /// <summary>
    /// Sets the data source and display member for the server list
    /// </summary>
    /// <param name="dataSource">The datasource to use for the server list</param>
    /// <returns></returns>
    public Task SetServerListDataSource(ref List<ServerConfig> dataSource)
        {
            if (serverList.InvokeRequired)
            {
                serverList.Invoke(GetDelegate(), dataSource);
            }
            else
            {
                serverList.DataSource = dataSource;
                serverList.DisplayMember = "Name";
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates the tool strip Gateway connection status
        /// </summary>
        /// <param name="text">The new status to display.</param>
        /// <returns></returns>
        public Task UpdateDiscordStatus(string text)
        {
            if (discordStatus.GetCurrentParent().InvokeRequired) discordStatus.GetCurrentParent().Invoke(delegate { UpdateDiscordStatus(text); });
            else discordStatus.Text = text;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Write a line to the textbox used as console output
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns></returns>
        public Task WriteLine(string text)
        {
            if (consoleBox.InvokeRequired) consoleBox.Invoke(delegate { WriteLine(text); });
            else consoleBox.AppendText($"{text}{Environment.NewLine}");
            return Task.CompletedTask;
        }
    }
}

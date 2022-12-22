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

        /// <summary>
        /// Sets the data source and display member for the server list
        /// </summary>
        /// <param name="dataSource">The datasource to use for the server list</param>
        /// <returns></returns>
        public Task SetServerListDataSource(HashSet<ServerConfig> dataSource)
            {
                if (serverList.InvokeRequired) serverList.Invoke( delegate { SetServerListDataSource(dataSource);  });
                else
                {
                    serverList.DataSource = dataSource.ToList();
                    serverList.DisplayMember = "Name";
                }
                return Task.CompletedTask;
            }

        public Task AddServerListItem(string name)
        {
            if (serverList.InvokeRequired) serverList.Invoke( delegate { AddServerListItem(name); });
            else
            {
                serverList.Items.Add(name);
            }
            return Task.CompletedTask;
        }

        public Task RemoveServerListItem(string name)
        {
            if (serverList.InvokeRequired) serverList.Invoke(delegate { RemoveServerListItem(name); });
            else
            {
                serverList.Items.Remove(name);
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

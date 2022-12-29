/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */
using MessagePipe;

namespace CCW.GoW;

public class UiService
{
    internal ListBox serverList;
    internal TextBox consoleBox;
    internal ToolStripLabel discordStatus;
    private static UiService? _instance;
    private UiService(MainWindow window)
    {
        serverList = window.serverList;
        consoleBox = window.consoleBox;
        discordStatus = window.discordStatus;
    }
    internal static UiService CreateInstance(MainWindow window)
    {
        _instance = new UiService(window);
        return _instance;
    }

    public Task AddItem(string name)
    {
        if (serverList.InvokeRequired) serverList.Invoke( delegate { AddItem(name); });
        else serverList.Items.Add(name);
        return Task.CompletedTask;
    }

    public async ValueTask AddItemAsync(string name, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await AddItem(name);
            break;
        }
    }

    public Task RemoveItem(string name)
    {
        if (serverList.InvokeRequired) serverList.Invoke(delegate { RemoveItem(name); });
        else serverList.Items.Remove(name);
        return Task.CompletedTask;
    }

    public async ValueTask RemoveItemAsync(string name, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await RemoveItem(name);
            break;
        }
    }

    /// <summary>
    /// Updates the tool strip Gateway connection status
    /// </summary>
    /// <param name="text">The new status to display.</param>
    /// <returns></returns>
    public Task UpdateStatus(string text)
    {
        if (discordStatus.GetCurrentParent().InvokeRequired) discordStatus.GetCurrentParent().Invoke(delegate { UpdateStatus(text); });
        else discordStatus.Text = text;
        return Task.CompletedTask;
    }

    public async ValueTask UpdateStatusAsync(string text, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UpdateStatus(text);
            break;
        }
    }

    /// <summary>
    /// Write a line to the textbox used as console output
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <returns></returns>
    public Task WriteLine(string text)
    {
        if (!consoleBox.Created) consoleBox.HandleCreated += (sender, e) =>
        {
            if (consoleBox.InvokeRequired) consoleBox.Invoke(delegate { WriteLine(text); });
            else consoleBox.AppendText($"{text}{Environment.NewLine}");
        };
        if (consoleBox.InvokeRequired) consoleBox.Invoke(delegate { WriteLine(text); });
        else consoleBox.AppendText($"{text}{Environment.NewLine}");
        return Task.CompletedTask;
    }

    public async ValueTask WriteLineAsync(string text, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await WriteLine(text);
            break;
        }
    }
}

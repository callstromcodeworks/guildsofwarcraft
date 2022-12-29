/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */
using System.Runtime.InteropServices;
using CCW.GoW.Services;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;
using static CCW.GoW.DataService.Service.MessageService;

namespace CCW.GoW;

public partial class MainWindow : Form
{
    private readonly IDistributedSubscriber<int, string>? subscriber;
    private readonly IAsyncDisposable[] disposableList;

    [DllImport("user32.dll")]
    static extern bool HideCaret(IntPtr hWnd);
    public MainWindow(IServiceProvider services)
    {
        InitializeComponent();
        FormClosed += FormClose;
        disposableList = new IAsyncDisposable[4];
        subscriber = services.GetService<IDistributedSubscriber<int, string>>();
        SubscriptionHandler(subscriber).GetAwaiter().GetResult();

        HideCaret(consoleBox.Handle);
        consoleBox.GotFocus += (sender, e) => HideCaret(consoleBox.Handle);
        Console.SetOut(new ConsoleWriter(consoleBox));
#if !DEBUG
        debugToolsMenuItem.Visible = false;
#endif
    }

    public async Task SubscriptionHandler(IDistributedSubscriber<int, string>? subscriber)
    {
        if (subscriber == null) throw new ArgumentNullException();
        UiService ui = UiService.CreateInstance(this);
        var s1 = await subscriber.SubscribeAsync(_AddItem, ui.AddItemAsync);
        var s2 = await subscriber.SubscribeAsync(_RemoveItem, ui.RemoveItemAsync);
        var s3 = await subscriber.SubscribeAsync(_UpdateStatus, ui.UpdateStatusAsync);
        var s4 = await subscriber.SubscribeAsync(_WriteLine, ui.WriteLineAsync);
        disposableList[0] = s1;
        disposableList[1] = s2;
        disposableList[2] = s3;
        disposableList[3] = s4;
    }

    private async void FormClose(object? sender, FormClosedEventArgs e)
    {
        foreach (IAsyncDisposable a in disposableList)
        {
            await a.DisposeAsync();
        }
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
        Close();
    }

    private void InviteMenuItem_Click(object sender, EventArgs e)
    {
        Form form = new()
        {
            StartPosition = FormStartPosition.CenterScreen,
        };
        Label label = new()
        {
            Text = "Invite Link"
        };
        form.Controls.Add(label);
        TextBox inviteLink = new()
        {
            Text = "https://discord.com/api/oauth2/authorize?client_id=1045301189811650580&permissions=1108370147392&scope=bot"
        };
        inviteLink.Location = new (25, 25);
        form.Controls.Add(inviteLink);
        form.ShowDialog();
    }
}

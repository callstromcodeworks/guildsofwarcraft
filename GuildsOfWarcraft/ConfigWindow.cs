/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */
using CCW.GoW.DataService.Service;
using Discord.WebSocket;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;

namespace CCW.GoW;
public partial class ConfigWindow : Form
{
    private readonly IServiceProvider services = new ServiceCollection()
        .AddMessagePipe()
        .AddMessagePipeNamedPipeInterprocess("CCW-GoW-Tx")
        .BuildServiceProvider();
    private readonly IServiceProvider services2 = new ServiceCollection()
        .AddMessagePipe()
        .AddMessagePipeNamedPipeInterprocess("CCW-GoW-Rx")
        .BuildServiceProvider();

    private readonly IDistributedPublisher<int, string> publisher;
    private readonly IDistributedSubscriber<int, IReadOnlyCollection<SocketApplicationCommand>> cmdListSub;

    public ConfigWindow()
    {
        InitializeComponent();
        CancelButton = CancelConfigButton;
        publisher = services.GetRequiredService<IDistributedPublisher<int, string>>();
        cmdListSub = services2.GetRequiredService<IDistributedSubscriber<int, IReadOnlyCollection<SocketApplicationCommand>>>();
        Startup().GetAwaiter().GetResult();
    }

    private async Task Startup()
    {
        await cmdListSub.SubscribeAsync(CommandHandler._GetList, UpdateCommandList);
        await publisher.PublishAsync(CommandHandler._UpdList, string.Empty);
    }

    public ValueTask UpdateCommandList(IReadOnlyCollection<SocketApplicationCommand> commandList, CancellationToken token)
    {
        CommandListListBox.DataSource = commandList;
        return ValueTask.CompletedTask;
    }

    private void AddCommandButton_Click(object sender, EventArgs e)
    {
        using var form = new AddCommandDialog();
        if (form.ShowDialog() == DialogResult.OK)
        {
            if (form.Command == string.Empty) throw new InvalidOperationException();
            publisher.PublishAsync(CommandHandler._AddCmd, form.Command);
            CommandListListBox.Items.Add(form.Command);
        }

    }

    private void RemoveCommandButton_Click(object sender, EventArgs e)
    {
        var pos = CommandListListBox.SelectedIndex;
        if (pos == -1) throw new InvalidOperationException();
        var command = CommandListListBox.Items[pos].ToString();
        if (command == null || command == string.Empty) throw new InvalidOperationException();
        publisher.PublishAsync(CommandHandler._RemCmd, command);
        CommandListListBox.Items.Remove(command);
    }
}

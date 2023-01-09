/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */
using Discord;
using Discord.WebSocket;
using MessagePipe;

namespace CCW.GoW.DataService.Service;

public class CommandHandler
{
    private readonly IServiceProvider services;
    private readonly IDistributedPublisher<int, IReadOnlyCollection<SocketApplicationCommand>> publisher;
    private readonly IDistributedSubscriber<int, string> cmdSub;
    private readonly IDistributedSubscriber<int, int> updCmdSub;
    private readonly MessageService messageService;
    private readonly DiscordSocketClient discordClient;
    private DateTime starting;
    private IReadOnlyCollection<SocketApplicationCommand> commandList;
    public static readonly int _GetList = 109391726, _UpdList = 109391727, _AddCmd = 109391728, _RemCmd = 109391729;

    private static CommandHandler? _instance;
    public static CommandHandler GetInstance(IServiceProvider provider) 
    {
        _instance ??= new CommandHandler(provider);
        return _instance;
    }
    private CommandHandler(IServiceProvider _services)
    {
        starting = DateTime.UtcNow;
        services = _services;
        messageService = services.GetRequiredService<MessageService>();
        discordClient = services.GetRequiredService<DiscordSocketClient>();

        var svc = new ServiceCollection()
            .AddMessagePipe()
            .AddMessagePipeNamedPipeInterprocess("CCW-GoW-Tx")
            .BuildServiceProvider();
        publisher = svc.GetRequiredService<IDistributedPublisher<int, IReadOnlyCollection<SocketApplicationCommand>>>();
        var svc2 = new ServiceCollection()
            .AddMessagePipe()
            .AddMessagePipeNamedPipeInterprocess("CCW-GoW-Rx")
            .BuildServiceProvider();
        cmdSub = svc2.GetRequiredService<IDistributedSubscriber<int, string>>();
        discordClient.Ready += Startup;
        commandList ??= new List<SocketApplicationCommand>();
    }

    private async Task Startup()
    {
        await cmdSub.SubscribeAsync(_AddCmd, AddCommandAsync).AsTask();
        await cmdSub.SubscribeAsync(_RemCmd, RemoveCommandAsync).AsTask();
        await cmdSub.SubscribeAsync(_UpdList, UpdateGlobalAppCommandListAsync).AsTask();
        try
        {
            commandList = await discordClient.GetGlobalApplicationCommandsAsync();
        } catch (HttpRequestException ex)
        {
            await messageService.Log(new LogMessage(LogSeverity.Error, DiscordService.SourceCommands, $"Error retrieving command list: {ex.Message}"));
        }
    }

    private async ValueTask AddCommandAsync(string command, CancellationToken token)
    {
        var cmd = new SlashCommandBuilder()
            .WithName(command)
            .WithDefaultPermission(true)
            .WithDefaultMemberPermissions(GuildPermission.ManageEvents)
            .Build();
        try
        {
            var result = await discordClient.CreateGlobalApplicationCommandAsync(cmd);
        }
        catch (HttpRequestException ex)
        {
            await messageService.Log(new LogMessage(LogSeverity.Error, DiscordService.SourceCommands, $"Failed to add command {cmd.Name} Reason: {ex.Message}"));
        }
        await messageService.Log(new LogMessage(LogSeverity.Info, DiscordService.SourceCommands, $"Created command: {cmd.Name}"));
    }

    private async ValueTask RemoveCommandAsync(string command, CancellationToken token)
    {
        var cmd = commandList.First(c => c.Name == command);
        if (cmd != null) await cmd.DeleteAsync();
    }

    private async ValueTask UpdateCommandListAsync()
    {
        var t = new Task(async () => {
            starting = DateTime.UtcNow;
            commandList = await discordClient.GetGlobalApplicationCommandsAsync();
            await publisher.PublishAsync(_GetList, commandList);
        });
        while (true)
        {
            if (DateTime.UtcNow.CompareTo(starting.AddHours(1.0)) >= 0)
            {
                t.Start();
            }
            await Task.Delay(60000);
        }
    }

    private ValueTask UpdateGlobalAppCommandListAsync(string _, CancellationToken token) => UpdateCommandListAsync();

    public async Task HandleCommand(SocketSlashCommand command)
    {
        switch (command.CommandName)
        {
            case "setguild":
                await command.FollowupAsync($"Please authorize this application here: {BlizzardApi.BlizzApiHandler.GetAuthorizationUri()}", ephemeral: true);
                await messageService.Log(new LogMessage(LogSeverity.Info, "Commands", $"setguild request from {command.GuildId}"));
                //await CheckForAuthorizationCode(command.GuildId.ToString());
                await command.RespondAsync("Your Guild is now set", ephemeral: true);
                break;
            case "removeguild":
                await command.RespondAsync("Your Guild is now removed", ephemeral: true);
                break;
        }
    }


    //TODO link with blazor to get result from auth
    async Task CheckForAuthorizationCode(string? GuildId)
    {

    }
}

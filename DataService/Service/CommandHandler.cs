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
    private readonly MessageService messageService;
    private readonly DiscordSocketClient discordClient;
    private IReadOnlyCollection<SocketApplicationCommand> commandList;

    private static CommandHandler? _instance;
    public static CommandHandler GetInstance(IServiceProvider provider) 
    {
        _instance = new CommandHandler(provider);
        return _instance;
    }
    private CommandHandler(IServiceProvider _services)
    {
        services = _services;
        messageService = services.GetRequiredService<MessageService>();
        discordClient = services.GetRequiredService<DiscordSocketClient>();
        Startup().GetAwaiter().GetResult();
    }

    private async Task Startup()
    {
        var addCmdSub = services.GetRequiredService<IDistributedSubscriber<int, SlashCommandProperties>>();
        await addCmdSub.SubscribeAsync(0, AddCommandAsync).AsTask();
        var remCmdSub = services.GetRequiredService<IDistributedSubscriber<int, ulong>>();
        await remCmdSub.SubscribeAsync(1, RemoveCommandAsync).AsTask();
        try
        {
            commandList = await discordClient.GetGlobalApplicationCommandsAsync();
        } catch (HttpRequestException ex)
        {
            await messageService.Log(new LogMessage(LogSeverity.Error, DiscordService.SourceCommands, $"Error retrieving command list: {ex.Message}"));
            commandList = new List<SocketApplicationCommand>();
        }
    }

    private async ValueTask AddCommandAsync(SlashCommandProperties command, CancellationToken token)
    {
        try
        {
            var result = await discordClient.CreateGlobalApplicationCommandAsync(command);
        }
        catch (HttpRequestException ex)
        {
            await messageService.Log(new LogMessage(LogSeverity.Error, DiscordService.SourceCommands, $"Failed to add command {command.Name} Reason: {ex.Message}"));
        }
        await messageService.Log(new LogMessage(LogSeverity.Info, DiscordService.SourceCommands, $"Created command: {command.Name}"));
    }

    private async ValueTask RemoveCommandAsync(ulong commandId, CancellationToken token)
    {
        var cmdToRem = await discordClient.GetGlobalApplicationCommandAsync(commandId);
        await cmdToRem.DeleteAsync();
    }

    private async ValueTask UpdateGlobalAppCommandListAsync() => commandList = await discordClient.GetGlobalApplicationCommandsAsync();

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

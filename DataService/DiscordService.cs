/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Discord;
using System.Reflection;
using CCW.GoW.DataService.Service;
using MessagePipe;

namespace CCW.GoW.DataService;

public class DiscordService
{
    private readonly IServiceProvider services;
    private readonly MessageService message;
    private readonly DiscordSocketClient discordClient;
    private readonly CommandService commandService;
    private readonly DataHandler dataHandler;
    internal HashSet<ServerConfig> configSet;

    private static readonly string SourceInit = "Init";
    private static readonly string SourceGuilds = "Guilds";
    private static readonly string SourceGuildEvents = "Guild Events";
    private static readonly string SourceCommands = "Commands";
    private static readonly string SourceMessages = "Messages";

    public DiscordService(IServiceProvider _services)
    {
        services = _services;
        message = new MessageService(services.GetRequiredService<IDistributedPublisher<int, string>>());
        discordClient = services.GetRequiredService<DiscordSocketClient>();
        commandService = services.GetRequiredService<CommandService>();
        dataHandler = services.GetRequiredService<DataHandler>();
        configSet = dataHandler.LoadAllServers().GetAwaiter().GetResult();
    }

    public async Task RunAsync()
    {
        SetEventHandlers();
        await Log(new LogMessage(LogSeverity.Debug, SourceInit, "Logging into Discord"));
        await discordClient.LoginAsync(TokenType.Bot, Worker.AppConfig.DiscordToken);
        await Log(new LogMessage(LogSeverity.Debug, SourceInit, "Starting client"));
        await discordClient.StartAsync();
        await Task.Delay(-1);
    }

    private void SetEventHandlers()
    {
        commandService.Log += Log;
        discordClient.Log += Log;
        discordClient.Connected += Connected;
        discordClient.Disconnected += Disconnected;
        discordClient.Ready += Ready;
        discordClient.JoinedGuild += JoinedGuild;
        discordClient.LeftGuild += LeftGuild;
        discordClient.GuildAvailable += GuildAvailable;
        discordClient.GuildUnavailable += GuildUnavailable;
        discordClient.GuildScheduledEventCreated += GuildScheduledEventCreated;
        discordClient.GuildScheduledEventCancelled += GuildScheduledEventCancelled;
        discordClient.GuildScheduledEventUpdated += GuildScheduledEventUpdated;
        discordClient.GuildScheduledEventStarted += GuildScheduledEventStarted;
        discordClient.GuildScheduledEventCompleted += GuildScheduledEventCompleted;
        discordClient.InviteCreated += InviteCreated;
        discordClient.InviteDeleted += InviteDeleted;
        discordClient.ReactionAdded += ReactionAdded;
        discordClient.SlashCommandExecuted += SlashCommandExecuted;
    }

    #region Client callbacks
    async Task CreateGlobalCommands()
    {
        var cmdList = new List<SlashCommandBuilder>();
        var setGuildCommand = new SlashCommandBuilder()
            .WithName("setguild")
            .WithDescription("Set your WoW Guild for this server");
        cmdList.Add(setGuildCommand);

        var removeGuildCommand = new SlashCommandBuilder()
            .WithName("removeguild")
            .WithDescription("Removes the currently associated WoW Guild");
        cmdList.Add(removeGuildCommand);
        // Commands only need to be created once (stored on Discord's servers)
        try
        {
            var commands = await discordClient.GetGlobalApplicationCommandsAsync();
            foreach (var cmd in commands)
            {
                cmdList.Remove(cmdList.First(x => x.Name == cmd.Name));
            }
        }
        catch (InvalidOperationException ex)
        {
            await Log(new LogMessage(LogSeverity.Error, SourceCommands, ex.Message));
        }

        if (cmdList.Count > 0)
        try
        {
            for (int i = 0; i < cmdList.Count; i++)
            {
                await discordClient.CreateGlobalApplicationCommandAsync(cmdList[i].Build());
            }
        }
        catch (HttpException ex)
        {
            if (ex.HttpCode is not System.Net.HttpStatusCode.MethodNotAllowed)
                await Log(new LogMessage(LogSeverity.Error, SourceCommands, $"Exception: {ex.Reason}"));
            else await Log(new LogMessage(LogSeverity.Error, SourceCommands, $"405 MethodNotAllowed"));
        }
    }
    #region Connection events
    async Task Connected()
    {
        await message.UpdateStatus("Connected");
        await Log(new LogMessage(LogSeverity.Info, SourceInit, "Connected to gateway"));
    }
    async Task Disconnected(Exception ex)
    {
        await message.UpdateStatus("Disconnected");
        await Log(new LogMessage(LogSeverity.Error, SourceInit, "Failed to connect"));
        await Log(new LogMessage(LogSeverity.Debug, SourceInit, "Exception occured", ex));
    }
    async Task Ready()
    {
        await message.UpdateStatus("Ready");
        await Log(new LogMessage(LogSeverity.Info, SourceInit, "Ready"));
        await CreateGlobalCommands();
    }
    #endregion
    #region Guild events
    async Task GuildAvailable(SocketGuild guild)
    {
        var config = await dataHandler.LoadServerInfo(guild.Id.ToString());
        if (config.Empty)
        {
            config.Id = guild.Id.ToString();
            config.Name = guild.Name;
            if (!await dataHandler.AddDiscordServer(config)) await dataHandler.UpdateDiscordServer(config);
        }
        configSet.Add(config);
        await message.AddItem(config.Name);
        await Log(new LogMessage(LogSeverity.Info, SourceInit, $"Guild available: {guild.Name}"));
    }
    async Task GuildUnavailable(SocketGuild guild)
    {
        var config = configSet.First(x => x.Id == guild.Id.ToString());
        if (config is not null)
        {
            configSet.Remove(config);
            await message.RemoveItem(config.Name);
            await dataHandler.UpdateDiscordServer(config);
            await Log(new LogMessage(LogSeverity.Info, SourceInit, $"Guild unavailable: {guild.Name}"));
        }
        else await Log(new LogMessage(LogSeverity.Warning, SourceInit, "Guild not in configuration set"));
    }
    async Task JoinedGuild(SocketGuild guild)
    {
        var config = new ServerConfig()
        {
            Id = guild.Id.ToString(),
            Name = guild.Name
        };
        await dataHandler.AddDiscordServer(config);
        configSet.Add(config);
        await message.AddItem(config.Name);
        await Log(new LogMessage(LogSeverity.Info, SourceGuilds, $"Joined guild: {guild.Name}"));
    }
    async Task LeftGuild(SocketGuild guild)
    {
        await Log(new LogMessage(LogSeverity.Info, SourceGuilds, $"Left guild: {guild.Name}"));
        var config = configSet.First(c => c.Id == guild.Id.ToString());
        if (config is null) await Log(new LogMessage(LogSeverity.Warning, SourceGuilds, "Guild not in configuration set so not removed"));
        else
        {
            await dataHandler.RemoveDiscordServer(config);
            configSet.Remove(config);
            await message.RemoveItem(config.Name);
        }
    }
    Task InviteCreated(SocketInvite invite)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuilds, "Invites not currently implemented"));
    }
    Task InviteDeleted(SocketGuildChannel channel, string deletedCode)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuilds, "Invites not currently implemented"));
    }
    #endregion
    #region GuildEvents events
    Task GuildScheduledEventCreated(SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventCancelled(SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventUpdated(Cacheable<SocketGuildEvent, ulong> cachedEvent, SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventStarted(SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventCompleted(SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    #endregion
    async Task SlashCommandExecuted(SocketSlashCommand command)
    {
        switch (command.CommandName)
        {
            case "setguild":
                await command.FollowupAsync($"Please authorize this application here: {BlizzardApi.BlizzApiHandler.GetAuthorizationUri()}", ephemeral: true);
                //await CheckForAuthorizationCode(command.GuildId.ToString());
                await command.RespondAsync("Your Guild is now set", ephemeral: true);
                break;
            case "removeguild":
                await command.RespondAsync("Your Guild is now removed", ephemeral: true);
                break;
        }
    }
    Task ReactionAdded(Cacheable<IUserMessage, ulong> cachedMessage, Cacheable<IMessageChannel, ulong> cachedChannel, SocketReaction reaction)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceMessages, "Reactions not currently implemented"));
    }
    Task Log(LogMessage msg)
    {
        if (msg.Severity > Worker.LogSeverityLevel) return Task.CompletedTask;
        return message.WriteLine(msg.ToString());
    }

    #endregion

    //TODO link with blazor to get result from auth
    async Task CheckForAuthorizationCode(string? GuildId)
    {

    }
}
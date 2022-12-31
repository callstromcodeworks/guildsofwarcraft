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
using CCW.GoW.DataService.Database;
using CCW.GoW.DataService.Objects;
using CCW.GoW.DataService.Service;

namespace CCW.GoW.DataService;

public class DiscordService
{
    private readonly IServiceProvider services;
    private readonly MessageService messageService;
    private readonly DiscordSocketClient discordClient;
    private readonly CommandService commandService;
    private readonly CommandHandler commandHandler;
    private readonly DataHandler dataHandler;
    internal HashSet<ServerConfig> configSet;

    internal static readonly string SourceStartup = "Startup";
    internal static readonly string SourceGuilds = "Guilds";
    internal static readonly string SourceGuildEvents = "Guild Events";
    internal static readonly string SourceCommands = "Commands";
    internal static readonly string SourceMessages = "Messages";

    public DiscordService(IServiceProvider _services)
    {
        services = _services;
        messageService = services.GetRequiredService<MessageService>();
        discordClient = services.GetRequiredService<DiscordSocketClient>();
        commandService = services.GetRequiredService<CommandService>();
        dataHandler = services.GetRequiredService<DataHandler>();
        configSet = dataHandler.LoadAllServers().GetAwaiter().GetResult();
        commandHandler = new CommandHandler(services);
    }

    public async Task RunAsync()
    {
        SetEventHandlers();
        await messageService.Log(new LogMessage(LogSeverity.Debug, SourceStartup, "Logging into Discord"));
        await discordClient.LoginAsync(TokenType.Bot, Worker.AppConfig.DiscordToken);
        await messageService.Log(new LogMessage(LogSeverity.Debug, SourceStartup, "Starting client"));
        await discordClient.StartAsync();
        await Task.Delay(-1);
    }

    private void SetEventHandlers()
    {
        commandService.Log += messageService.Log;
        discordClient.Log += messageService.Log;
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
        discordClient.SlashCommandExecuted += commandHandler.HandleCommand;
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
            await messageService.Log(new LogMessage(LogSeverity.Error, SourceCommands, ex.Message));
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
            await messageService.Log(new LogMessage(LogSeverity.Error, SourceCommands, $"Exception: {ex.Reason}"));
        }
    }
    #region Connection events
    async Task Connected()
    {
        await messageService.UpdateStatus("Connected");
        await messageService.Log(new LogMessage(LogSeverity.Info, SourceStartup, "Connected to gateway"));
    }
    async Task Disconnected(Exception ex)
    {
        await messageService.UpdateStatus("Disconnected");
        await messageService.Log(new LogMessage(LogSeverity.Error, SourceStartup, "Failed to connect"));
        await messageService.Log(new LogMessage(LogSeverity.Debug, SourceStartup, "Exception occured", ex));
    }
    async Task Ready()
    {
        await messageService.UpdateStatus("Ready");
        await messageService.Log(new LogMessage(LogSeverity.Info, SourceStartup, "Ready"));
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
        await messageService.AddItem(config.Name);
        await messageService.Log(new LogMessage(LogSeverity.Info, SourceGuilds, $"Guild available: {guild.Name}"));
    }
    async Task GuildUnavailable(SocketGuild guild)
    {
        var config = configSet.First(x => x.Id == guild.Id.ToString());
        if (config is not null)
        {
            configSet.Remove(config);
            await messageService.RemoveItem(config.Name);
            await dataHandler.UpdateDiscordServer(config);
            await messageService.Log(new LogMessage(LogSeverity.Info, SourceGuilds, $"Guild unavailable: {guild.Name}"));
        }
        else await messageService.Log(new LogMessage(LogSeverity.Warning, SourceGuilds, "Guild not in configuration set"));
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
        await messageService.AddItem(config.Name);
        await messageService.Log(new LogMessage(LogSeverity.Info, SourceGuilds, $"Joined guild: {guild.Name}"));
    }
    async Task LeftGuild(SocketGuild guild)
    {
        await messageService.Log(new LogMessage(LogSeverity.Info, SourceGuilds, $"Left guild: {guild.Name}"));
        var config = configSet.First(c => c.Id == guild.Id.ToString());
        if (config is null) await messageService.Log(new LogMessage(LogSeverity.Warning, SourceGuilds, "Guild not in configuration set so not removed"));
        else
        {
            await dataHandler.RemoveDiscordServer(config);
            configSet.Remove(config);
            await messageService.RemoveItem(config.Name);
        }
    }
    Task InviteCreated(SocketInvite invite)
    {
        return messageService.Log(new LogMessage(LogSeverity.Info, SourceGuilds, "Invites not currently implemented"));
    }
    Task InviteDeleted(SocketGuildChannel channel, string deletedCode)
    {
        return messageService.Log(new LogMessage(LogSeverity.Info, SourceGuilds, "Invites not currently implemented"));
    }
    #endregion
    #region GuildEvents events
    Task GuildScheduledEventCreated(SocketGuildEvent eventParam)
    {
        return messageService.Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventCancelled(SocketGuildEvent eventParam)
    {
        return messageService.Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventUpdated(Cacheable<SocketGuildEvent, ulong> cachedEvent, SocketGuildEvent eventParam)
    {
        return messageService.Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventStarted(SocketGuildEvent eventParam)
    {
        return messageService.Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventCompleted(SocketGuildEvent eventParam)
    {
        return messageService.Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    #endregion
    Task ReactionAdded(Cacheable<IUserMessage, ulong> cachedMessage, Cacheable<IMessageChannel, ulong> cachedChannel, SocketReaction reaction)
    {
        return messageService.Log(new LogMessage(LogSeverity.Info, SourceMessages, "Reactions not currently implemented"));
    }

    #endregion
}
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
using Microsoft.Extensions.DependencyInjection;

namespace CCW.GoW.Services;

internal class DiscordService
{
    private readonly IServiceProvider services;
    private readonly UiService uiService;
    private readonly DiscordSocketClient discordClient;
    private readonly CommandService commandService;
    private readonly DataHandler dataHandler;
    private List<ServerConfig> configSet;

    private static readonly string SourceInit = "Init";
    private static readonly string SourceGuilds = "Guilds Handler";
    private static readonly string SourceGuildEvents = "Guild Event Handler";
    private static readonly string SourceGlobalCommands = "Global Command Handler";
    private static readonly string SourceCommands = "Command Handler";
    private static readonly string SourceMessages = "Message Handler";

    //TODO switch this over to a database column and modify stored procedure accordingly to not load these guilds
    private readonly HashSet<ServerConfig> ConfigsUnavailable = new();

    public DiscordService(IServiceProvider _services)
    {
        services = _services;
        uiService = services.GetRequiredService<UiService>();
        discordClient = services.GetRequiredService<DiscordSocketClient>();
        commandService = services.GetRequiredService<CommandService>();
        dataHandler = services.GetRequiredService<DataHandler>();
        configSet = dataHandler.LoadAllServers().GetAwaiter().GetResult();
        RunAsync().GetAwaiter().GetResult();
    }

    public async Task RunAsync()
    {
        await uiService.SetServerListDataSource(ref configSet);
        SetEventHandlers();
        await discordClient.LoginAsync(TokenType.Bot, Program.AppConfig.DiscordToken);
        await discordClient.StartAsync();

        await Log(new LogMessage(LogSeverity.Debug, SourceInit, "Adding Modules"));
        await commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: services);
        await CreateGlobalCommands();
    }

    private void SetEventHandlers()
    {
        commandService.Log += Log;
        discordClient.Log += Log;
        discordClient.Connected += ConnectedEvent;
        discordClient.Disconnected += DisconnectedEvent;
        discordClient.Ready += ReadyEvent;
        discordClient.JoinedGuild += JoinedGuildEvent;
        discordClient.LeftGuild += LeftGuildEvent;
        discordClient.GuildAvailable += GuildAvailableEvent;
        discordClient.GuildUnavailable += GuildUnavilableEvent;
        discordClient.GuildScheduledEventCreated += GuildScheduledEventCreatedEvent;
        discordClient.GuildScheduledEventCancelled += GuildScheduledEventCancelledEvent;
        discordClient.GuildScheduledEventUpdated += GuildScheduledEventUpdatedEvent;
        discordClient.GuildScheduledEventStarted += GuildScheduledEventStartedEvent;
        discordClient.GuildScheduledEventCompleted += GuildScheduledEventCompletedEvent;
        discordClient.InviteCreated += InviteCreatedEvent;
        discordClient.InviteDeleted += InviteDeletedEvent;
        discordClient.MessageReceived += HandleCommandAsync;
        discordClient.ReactionAdded += ReactionAddedEvent;
        discordClient.SlashCommandExecuted += HandleSlashCommmandAsync;
    }

    #region Client callbacks
    async Task CreateGlobalCommands()
    {
        var cmdList = new List<SlashCommandBuilder>();
        var setGuildCommand = new SlashCommandBuilder()
            .WithName("setguild")
            .WithDescription("Set your WoW Guild for this server")
            .WithDefaultMemberPermissions(GuildPermission.Administrator)
            .WithDMPermission(true);
        cmdList.Add(setGuildCommand);

        var removeGuildCommand = new SlashCommandBuilder()
            .WithName("removeguild")
            .WithDescription("Removes the currently associated WoW Guild")
            .WithDefaultMemberPermissions(GuildPermission.Administrator)
            .WithDMPermission(true);
        cmdList.Add(removeGuildCommand);
        try
        {
            for (int i = 0; i < cmdList.Count; i++)
            {
                await discordClient.CreateGlobalApplicationCommandAsync(cmdList[i].Build());
            }
        }
        catch (HttpException ex)
        {
            if (ex.HttpCode != System.Net.HttpStatusCode.MethodNotAllowed) 
                await Log(new LogMessage(LogSeverity.Error, SourceGlobalCommands, $"Exception: {ex.Reason}\nStack Trace: {ex.StackTrace}"));
        }
    }
    #region Connection events
    async Task ConnectedEvent()
    {
        await uiService.UpdateDiscordStatus("Connected");
        await Log(new LogMessage(LogSeverity.Info, SourceInit, "Connected to gateway"));
    }
    async Task DisconnectedEvent(Exception ex)
    {
        await uiService.UpdateDiscordStatus("Disconnected");
        await Log(new LogMessage(LogSeverity.Error, SourceInit, "Failed to connect"));
        await Log(new LogMessage(LogSeverity.Debug, SourceInit, "Exception occured", ex));
    }
    async Task ReadyEvent()
    {
        await uiService.UpdateDiscordStatus("Ready");
        await Log(new LogMessage(LogSeverity.Info, SourceInit, "Ready"));
    }
    #endregion
    #region Guild events
    async Task GuildAvailableEvent(SocketGuild guild)
    {
        var config = await dataHandler.LoadServerInfo(guild.Id.ToString());
        if (config.Empty)
        {
            config.Id = guild.Id.ToString();
            config.Name = guild.Name;
            if(!await dataHandler.AddDiscordServer(config)) await dataHandler.UpdateDiscordServer(config);
        }
        configSet.Add(config);
        await Log(new LogMessage(LogSeverity.Info, SourceInit, $"Guild available: {guild.Name}"));
    }
    async Task GuildUnavilableEvent(SocketGuild guild)
    {
        await dataHandler.UpdateDiscordServerAvailability(guild.Id.ToString(), true);
        var config = configSet.Find(x => x.Id == guild.Id.ToString());
        if (config != null) configSet.Remove(config);
        await Log(new LogMessage(LogSeverity.Warning, SourceInit, $"Guild unavailable: {guild.Name}"));
    }
    async Task JoinedGuildEvent(SocketGuild guild)
    {
        var config = new ServerConfig()
        {
            Id = guild.Id.ToString(),
            Name = guild.Name
        };
        await dataHandler.AddDiscordServer(config);
        configSet.Add(config);
        await Log(new LogMessage(LogSeverity.Info, SourceGuilds, $"Joined guild: {guild.Name}"));
    }
    Task LeftGuildEvent(SocketGuild guild)
    {
        ServerConfig config = configSet.First(c => c.Id == guild.Id.ToString());
        if (config == null) return Log(new LogMessage(LogSeverity.Warning, SourceGuilds, $"Guild not in configSet: {guild.Name}"));
        dataHandler.RemoveDiscordServer(config).GetAwaiter().GetResult();
        configSet.Remove(config);
        return Log(new LogMessage(LogSeverity.Info, SourceGuilds, $"Left guild: {guild.Name}"));
    }
    Task InviteCreatedEvent(SocketInvite invite)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuilds, "Invites not currently implemented"));
    }
    Task InviteDeletedEvent(SocketGuildChannel channel, string deletedCode)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuilds, "Invites not currently implemented"));
    }
    #endregion
    #region GuildEvents events
    Task GuildScheduledEventCreatedEvent(SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventCancelledEvent(SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventUpdatedEvent(Cacheable<SocketGuildEvent, ulong> cachedEvent, SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventStartedEvent(SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    Task GuildScheduledEventCompletedEvent(SocketGuildEvent eventParam)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceGuildEvents, "Not implemented"));
    }
    #endregion

    async Task HandleCommandAsync(SocketMessage messageParam)
    {
        if (messageParam is not SocketUserMessage message) return;
        if (message.Channel is not SocketGuildChannel c) return;
        ServerConfig config = configSet.First(s => s.Id == c.Guild.Id.ToString());
        int messagePosition = 0;
        if (!(message.HasCharPrefix(config.CommandPrefix, ref messagePosition) ||
            message.HasMentionPrefix(discordClient.CurrentUser, ref messagePosition)) ||
            message.Author.IsBot)
            return;
        var context = new SocketCommandContext(discordClient, message);
        IResult r = await commandService.ExecuteAsync(
            context: context,
            argPos: messagePosition,
            services: services);
        if (!r.IsSuccess && r.Error != null) await Log(new LogMessage(LogSeverity.Error, SourceCommands, r.ErrorReason));
    }
    async Task HandleSlashCommmandAsync(SocketSlashCommand command)
    {
        switch (command.CommandName)
        {
            case "setguild":
                await command.RespondAsync("Your Guild is now linked", ephemeral: true);
                break;
            case "removeguild":
                break;
        }
    }
    Task ReactionAddedEvent(Cacheable<IUserMessage, ulong> cachedMessage, Cacheable<IMessageChannel, ulong> cachedChannel, SocketReaction reaction)
    {
        return Log(new LogMessage(LogSeverity.Info, SourceMessages, "Reactions not currently implemented"));
    }
    Task Log(LogMessage msg)
    {
        if (msg.Severity > Program.LogSeverityLevel) return Task.CompletedTask;
        return uiService.WriteLine(msg.ToString());
    }

    #endregion

    string GetInviteLink()
    {
        return "https://discord.com/api/oauth2/authorize?client_id=1045301189811650580&permissions=1108370147392&scope=bot";
    }
}
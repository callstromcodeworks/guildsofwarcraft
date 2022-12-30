/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */
using Discord;
using Discord.WebSocket;

namespace CCW.GoW.DataService.Service;

public class CommandHandler
{
    private readonly IServiceProvider services;
    private readonly MessageService messageService; 
    public CommandHandler(IServiceProvider _services)
    {
        this.services = _services;
        messageService = services.GetRequiredService<MessageService>();
    }
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

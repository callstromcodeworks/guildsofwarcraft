/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using Discord.Commands;

namespace CCW.GoW.Modules;

[Group("players")]
public class PlayersModule : ModuleBase<SocketCommandContext>
{
    [Command("list")]
    [Summary("List all players in guild")]
    public Task ListAsync() =>
        ReplyAsync("");

    [Command("info")]
    [Summary("Get info about a player")]
    public Task InfoAsync([Summary("The player to get info about")] string playerName) =>
        ReplyAsync("");
}

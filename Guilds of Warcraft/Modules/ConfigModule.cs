/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

using CCW.GoW.Services;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CCW.GoW.Modules;

//TODO implement all per server configurations

[Group("config")]
[Summary("Used for getting/setting bot configuration options")]
public class ConfigModule : ModuleBase<SocketCommandContext>
{

    [Group("get")]
    [Summary("Get configuration option current values")]
    public class ConfigGetModule : ModuleBase<SocketCommandContext>
    {
        [Command("all")]
        [Summary("Lists all the configurable options")]
        public Task AllAsync()
        {
            Embed embed = new EmbedBuilder().WithTitle("All Configuration values")
                .WithColor(Discord.Color.DarkGrey).Build();
            return ReplyAsync(null, false, embed);
        }


    }
    [Group("set")]
    [Summary("Sets configuration option values")]
    public class ConfigSetModule : ModuleBase<SocketCommandContext>
    {
        readonly IServiceProvider services;
        public ConfigSetModule(IServiceProvider _services)
        {
            services = _services;
        }

        [Command("prefix")]
        [Summary("Set the command prefix")]
        public Task PrefixAsync(char prefix)
        {
            var set = services.GetRequiredService<HashSet<ServerConfig>>();
            ServerConfig config = set.First(s => s.Id == Context.Guild.Id.ToString());
            config.CommandPrefix = prefix;
            return ReplyAsync($"Set prefix to {prefix}");
        }
    }
}

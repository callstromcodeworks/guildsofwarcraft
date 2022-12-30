/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

namespace CCW.GoW.DataService.Objects;

public class ServerConfig
{
    public bool Empty { get; set; } = false;
    public string Id { get; set; } = string.Empty;
    public char CommandPrefix { get; set; } = '!';
    public string WowId { get; set; } = string.Empty;
    public bool Unavailable { get; set; } = false;
    public string GuildName { get; set; } = string.Empty;
    public string Faction { get; set; } = string.Empty;
    public string AchievementPoints { get; set; } = string.Empty;
    public string MemberCount { get; set; } = string.Empty;
    public string Realm { get; set; } = string.Empty;
    public string CreationTimestamp { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ServerConfig() { }
    public ServerConfig(bool empty)
    {
        Empty = empty;
    }
}

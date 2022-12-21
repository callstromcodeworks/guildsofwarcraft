/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

namespace CCW.GoW.BlizzardApi;
//TODO fix this, not filling in character/achievement sections

public class GuildActivityApiResponse : ApiResponseBase
{
    public __links _links { get; set; }
    public struct __guild
    {
        public __key Key { get; set; }
        public string Id { get; set; }
        public __realm Realm { get; set; }
        public __faction Faction { get; set; }
    }
    public __guild Guild { get; set; }
    public List<__character_achievement> Activities { get; set; } = new();
}

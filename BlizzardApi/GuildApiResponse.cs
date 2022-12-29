/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

namespace CCW.GoW.BlizzardApi;

public class GuildApiResponse : ApiResponseBase
{
    public __links _links { get; set; }
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public __faction Faction { get; set; }
    public string Achievement_Points { get; set; } = string.Empty; 
    public string Member_Count { get; set; } = string.Empty;
    public __realm Realm { get; set; }
    public struct __crest
    {
        public struct __emblem
        {
            public string Id { get; set; }
            public __media Media { get; set; }
            public __color Color { get; set; }
        }
        public __emblem Emblem { get; set; }
        public struct __border
        {
            public string Id { get; set; }
            public __media Media { get; set; }
            public __color Color { get; set; }
        }
        public __border Border { get; set; }
        public __background Background { get; set; }
    }
    public struct __roster
    {
        public string href { get; set; }
    }
    public __roster Roster { get; set; }
    public struct __achievements
    {
        public string href { get; set; }
    }
    public __achievements Achievements { get; set; }
    public string Created_Timestamp { get; set; } = string.Empty; 
    public __activity Activity { get; set; }

}

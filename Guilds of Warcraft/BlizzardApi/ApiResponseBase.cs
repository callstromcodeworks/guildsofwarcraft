/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

namespace CCW.GoW.BlizzardApi;

public abstract class ApiResponseBase
{
    public string Raw { get; set; } = string.Empty;
    public struct __links
    {
        public struct __self
        {
            public string Href { get; set; }
        }
        public __self Self { get; set; }
    }
    public struct __key
    {
        public string Href { get; set; }
    }
    public struct __realm
    {
        public __key Key { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Slug { get; set; }
    }
    public struct __faction
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }
    public struct __media
    {
        public __key Key { get; set; }
        public string Id { get; set; }
    }
    public struct __RGBA
    {
        public string R { get; set; }
        public string G { get; set; }
        public string B { get; set; }
        public string A { get; set; }
    }
    public struct __color
    {
        public string Id { get; set; }
        public __RGBA RGBA { get; set; }
    }
    public struct __background
    {
        public __color Color { get; set; }
    }
    public struct __character
    {
        public __key Key { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public __realm Realm { get; set; }
    }
    public struct __achievement
    {
        public __key Key { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }
    public struct __activity
    {
        public string Type { get; set; }
    }
    public struct __character_achievement
    {
        public __character Character { get; set; }
        public __achievement Achievement { get; set; }
        public __activity Activity { get; set; }
        public string Timestamp { get; set; }
    }
}

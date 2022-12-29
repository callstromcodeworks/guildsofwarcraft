/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */

namespace CCW.GoW.DataService;
public class DataContracts
{
    public readonly struct TableNames
    {
        public readonly static string Owner = "dbo";
        public readonly static string DiscordGuilds = "DiscordGuilds";
        public readonly static string DiscordEvents = "DiscordEvents";
        public readonly static string WowGuilds = "WoWGuilds";
        public readonly static string WowCrests = "WoWCrests";
    }
    public readonly struct ColumnNames
    {
        public readonly static string Id = "Id";
        public readonly static string DiscordId = "discord_id";
        public readonly static string WowId = "wow_id";

        public readonly static string Prefix = "command_prefix";

        public readonly static string Name = "name";
        public readonly static string Faction = "faction";
        public readonly static string AchPoints = "ach_points";
        public readonly static string MemberCount = "member_count";
        public readonly static string Realm = "realm";
        public readonly static string CreationTimestamp = "creation_timestamp";


    }

    public readonly struct SpParams
    {
        public readonly struct AddDiscordServer
        {
            public readonly static string _SpName = "AddDiscordServer";
            public readonly static string Id = "id";
            public readonly static string Name = "name";
            public readonly static string Prefix = "prefix";
            public readonly static string WowId = "wow_id";
        }

        public readonly struct RemoveDiscordServer
        {
            public readonly static string _SpName = "RemoveDiscordServer";
            public readonly static string Id = "id";
            public readonly static string WowId = "wow_id";
        }

        public readonly struct AssociateGuild
        {
            public readonly static string _SpName = "";
            public readonly static string Id = "id";
            public readonly static string WowId = "wow_id";
            public readonly static string Name = "name";
            public readonly static string Faction = "faction";
            public readonly static string AchievementPoints = "ach_points";
            public readonly static string MemberCount = "member_count";
            public readonly static string Realm = "realm";
            public readonly static string CreationTimestamp = "creation_timestamp";
        }
        public readonly struct DisassociateGuild
        {
            public readonly static string _SpName = "DisassociateGuild";
            public readonly static string Id = "id";
            public readonly static string WowId = "wow_id";
            public readonly static string Name = "name";
            public readonly static string Faction = "faction";
            public readonly static string AchievementPoints = "ach_points";
            public readonly static string MemberCount = "member_count";
            public readonly static string Realm = "realm";
            public readonly static string CreationTimestamp = "creation_timestamp";
        }
        public readonly struct GetServerInfo
        {
            public readonly static string _SpNameSingle = "GetServerInfo";
            public readonly static string _SpNameAll = "GetAllServers";
            public readonly static string Id = "id";
            public readonly struct Indexes
            {
                public readonly static int Id = 0;
                public readonly static int Prefix = 1;
                public readonly static int WowId = 2;
                public readonly static int Name = 3;
                public readonly static int Faction = 4;
                public readonly static int AchPoints = 5;
                public readonly static int MemberCount = 6;
                public readonly static int Realm = 7;
                public readonly static int CreationTimestamp = 8;
            }
        }

        public readonly struct UpdateDiscordServer
        {
            public readonly static string _SpName = "UpdateDiscordServer";
            public readonly static string Id = "id";
            public static readonly string Name = "name";
            public readonly static string Prefix = "prefix";
            public readonly static string Unavailable = "unavailable";
        }
    }
}

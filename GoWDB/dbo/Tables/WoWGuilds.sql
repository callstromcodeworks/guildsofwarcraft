CREATE TABLE [dbo].[WoWGuilds]
(
    [Id] NVARCHAR(50) NOT NULL PRIMARY KEY,
    [DiscordId] NVARCHAR(50) NOT NULL UNIQUE, 
    [GuildName] NVARCHAR(50) NOT NULL, 
    [Faction] NVARCHAR(50) NOT NULL, 
    [AchievementPoints] NVARCHAR(50) NOT NULL, 
    [MemberCount] NVARCHAR(50) NOT NULL, 
    [Realm] NVARCHAR(50) NOT NULL, 
    [CreationTimestamp] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_WoWGuilds_ToDiscordGuilds] FOREIGN KEY ([DiscordId]) REFERENCES [DiscordGuilds]([Id]) 
)

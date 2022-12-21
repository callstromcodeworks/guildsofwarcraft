CREATE TABLE [dbo].[WoWCrests]
(
	[Id] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [DiscordId] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_WoWCrests_WoWGuilds] FOREIGN KEY ([DiscordId]) REFERENCES [WoWGuilds]([DiscordId])
)

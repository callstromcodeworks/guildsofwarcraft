CREATE TABLE [dbo].[DiscordGuilds]
(
    [Id] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL,
    [CommandPrefix] NCHAR(1) NOT NULL DEFAULT '!',
    [WowId] NVARCHAR(50) NOT NULL DEFAULT 0, 
    [Unavailable] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [AK_DiscordGuilds_guild_id] UNIQUE ([WowId])
)

CREATE PROCEDURE [dbo].[GetServerInfo]
	@id nvarchar(50)
AS
BEGIN
	SELECT [DiscordGuilds].Id, [DiscordGuilds].Name, [DiscordGuilds].CommandPrefix, [DiscordGuilds].Unavailable, [WoWGuilds].Id, [WoWGuilds].GuildName, [WoWGuilds].Faction,
	[WoWGuilds].AchievementPoints, [WoWGuilds].MemberCount, [WoWGuilds].Realm, [WoWGuilds].CreationTimestamp
	FROM [DiscordGuilds]
	LEFT JOIN [WoWGuilds]
	ON [WoWGuilds].[Id] = [DiscordGuilds].[WowId]
	WHERE dbo.DiscordGuilds.Id = @id
END

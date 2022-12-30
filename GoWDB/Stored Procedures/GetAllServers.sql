CREATE PROCEDURE [dbo].[GetAllServers]
AS
BEGIN
	SELECT [DiscordGuilds].Id, [DiscordGuilds].Name, [DiscordGuilds].CommandPrefix, [DiscordGuilds].WowId, [WoWGuilds].GuildName, [WoWGuilds].Faction,
	[WoWGuilds].AchievementPoints, [WoWGuilds].MemberCount, [WoWGuilds].Realm, [WoWGuilds].CreationTimestamp
	FROM [DiscordGuilds]
	LEFT JOIN [WoWGuilds]
	ON [WoWGuilds].[Id] = [DiscordGuilds].[WowId]
	WHERE [DiscordGuilds].Unavailable = 0
END

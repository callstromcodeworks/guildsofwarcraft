CREATE PROCEDURE [dbo].[DisassociateGuild]
	@id nvarchar(50),
	@wow_id nvarchar(50)
AS
BEGIN
	UPDATE dbo.DiscordGuilds
	SET dbo.DiscordGuilds.WowId = 0
	WHERE dbo.DiscordGuilds.Id = @id;

	DELETE dbo.WoWGuilds
	WHERE dbo.WoWGuilds.Id = @wow_id;
END
RETURN @@ROWCOUNT
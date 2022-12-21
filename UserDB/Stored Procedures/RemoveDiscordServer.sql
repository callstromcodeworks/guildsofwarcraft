CREATE PROCEDURE [dbo].[RemoveDiscordServer]
	@id nvarchar(50),
	@wow_id nvarchar(50)
AS
BEGIN
	DELETE dbo.DiscordGuilds
	WHERE dbo.DiscordGuilds.Id = @id;

	IF @wow_id != '-1'
	DELETE dbo.WoWGuilds
	WHERE dbo.WoWGuilds.Id = @wow_id;
END
RETURN @@ROWCOUNT
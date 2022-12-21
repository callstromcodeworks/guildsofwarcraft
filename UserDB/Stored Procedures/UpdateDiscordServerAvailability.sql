CREATE PROCEDURE [dbo].[UpdateDiscordServerAvailability]
	@id nvarchar(50),
	@unavailable bit
AS
BEGIN
	UPDATE dbo.DiscordGuilds
	SET dbo.DiscordGuilds.Unavailable = @unavailable
	WHERE dbo.DiscordGuilds.Id = @id;
END
RETURN @@ROWCOUNT

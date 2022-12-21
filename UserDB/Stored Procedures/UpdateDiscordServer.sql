CREATE PROCEDURE [dbo].[UpdateDiscordServer]
	@id nvarchar(50),
	@name nvarchar(50),
	@prefix nchar(1)
AS
BEGIN
	UPDATE dbo.DiscordGuilds
	SET dbo.DiscordGuilds.Name = @name, dbo.DiscordGuilds.CommandPrefix = @prefix
	WHERE dbo.DiscordGuilds.Id = @id;
END
RETURN @@ROWCOUNT

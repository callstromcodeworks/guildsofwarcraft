CREATE PROCEDURE [dbo].[AddDiscordServer]
	@id nvarchar(50),
	@name nvarchar(50),
	@prefix nchar(1),
	@wow_id nvarchar(50) = 0
AS
BEGIN
	INSERT INTO dbo.DiscordGuilds
	VALUES (@id, @name, @prefix, @wow_id, 0);
END
RETURN @@ROWCOUNT

CREATE PROCEDURE [dbo].[AssociateGuild]
	@id nvarchar(50),
	@wow_id nvarchar(50),
	@name nvarchar(50),
	@faction nvarchar(50),
	@ach_points nvarchar(50),
	@member_count nvarchar(50),
	@realm nvarchar(50),
	@creation_timestamp nvarchar(50)
AS
BEGIN
	UPDATE dbo.DiscordGuilds
	SET dbo.DiscordGuilds.WowId = @wow_id
	WHERE dbo.DiscordGuilds.Id = @id;
	
	INSERT INTO dbo.WoWGuilds
	VALUES (@wow_id, @id, @name, @faction, @ach_points, @member_count, @realm, @creation_timestamp);
END
RETURN @@ROWCOUNT

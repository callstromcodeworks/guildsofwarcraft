CREATE TABLE [dbo].[DiscordEvents]
(
	[Id] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [DiscordId] NVARCHAR(50) NOT NULL, 
    [StartDate] DATETIME NULL, 
    [Duration] TIME NOT NULL
)

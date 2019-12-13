
DROP TABLE Sauna
DROP TABLE Valot
DROP TABLE Termostaatti
DROP TABLE Loki
DROP TABLE Ominaisuudet
-- proseduuri joka lis‰‰ ne toisiin tauluihin ja asettaa niille jonkin oletus arvon? 


CREATE TABLE [dbo].[Sauna]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[Kaynnissa] BIT NULL,
	[Lampo] INT NULL, 
)
CREATE TABLE [dbo].[Valot]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[Kaynnissa] BIT NULL,
	[Teho] INT NULL, 
)
CREATE TABLE [dbo].[Termostaatti]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[Tavoite] INT NULL,
	[Lampo] INT NULL, 

)
CREATE TABLE [dbo].[Ominaisuudet] 
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[OminaisuusId] INT NOT NULL,
	[Nimi] NVARCHAR(15) NULL,
)
CREATE TABLE [dbo].[Loki]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[OminaisuusId] INT NOT NULL,
	[Tapahtuma] NVARCHAR(100) NULL,
	[Ajakohta] DATETIME NULL,
	
)





---------------------------------------
-----------------------------------------------------------------------------------------------------------
--CREATE TABLE [dbo].[AssetLocations] (
--    [Id]         INT      IDENTITY (1, 1) NOT NULL,
--    [LocationId] INT      NULL,
--    [AssetId]    INT      NULL,
--    [LastSeen]   DATETIME NULL,
--    PRIMARY KEY CLUSTERED ([Id] ASC), 
--    CONSTRAINT [FK_AssetLocations_Locations] FOREIGN KEY ([LocationId]) REFERENCES [AssetLocation]([Id]),
--    CONSTRAINT [FK_AssetLocations_Assets] FOREIGN KEY ([AssetId]) REFERENCES [Assets]([Id])
--);

--CREATE TABLE [dbo].[AssetLocation]
--(
--	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
--	[Code] NVARCHAR(100),
--	[Name] NVARCHAR(200),
--	[Address] NVARCHAR(500)
--)

--CREATE TABLE [dbo].[Assets]
--(
--	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
--	[Code] NVARCHAR(100),
--	[Type] NVARCHAR(200),
--	[Model] NVARCHAR(500)
--)


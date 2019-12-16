
DROP TABLE Sauna
DROP TABLE Valot
DROP TABLE Termostaatti
DROP TABLE Loki
DROP TABLE Ominaisuudet
-- proseduuri joka lisää ne toisiin tauluihin ja asettaa niille jonkin oletus arvon? 

CREATE TABLE [dbo].[Ominaisuudet] 
(
	[OminaisuusId] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[Nimi] NVARCHAR(15) NULL,
)

CREATE TABLE [dbo].[Sauna]
(
	[SaunaId] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[OminaisuusId] INT NULL,
	[Kaynnissa] BIT NULL,
	[Lampo] INT NULL, 
	CONSTRAINT [FK_Sauna_Ominaisuudet] FOREIGN KEY ([OminaisuusId]) REFERENCES [Ominaisuudet]([OminaisuusId]),
)
CREATE TABLE [dbo].[Valot]
(
	[ValotId] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[OminaisuusId] INT NULL,
	[Kaynnissa] BIT NULL,
	[Teho] INT NULL, 
		CONSTRAINT [FK_Valot_Ominaisuudet] FOREIGN KEY ([OminaisuusId]) REFERENCES [Ominaisuudet]([OminaisuusId]),
)
CREATE TABLE [dbo].[Termostaatti]
(
	[TermoId] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[OminaisuusId] INT NULL,
	[Tavoite] INT NULL,
	[Lampo] INT NULL, 
		CONSTRAINT [FK_Termostaatti_Ominaisuudet] FOREIGN KEY ([OminaisuusId]) REFERENCES [Ominaisuudet]([OminaisuusId]),

)

CREATE TABLE [dbo].[Loki]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1000, 1), 
	[OminaisuusId] INT NOT NULL,
	[Tapahtuma] NVARCHAR(100) NULL,
	[Ajakohta] DATETIME NULL,
	CONSTRAINT [FK_Loki_Ominaisuudet] FOREIGN KEY ([OminaisuusId]) REFERENCES [Ominaisuudet]([OminaisuusId]),
)




-- tauluihin FK:ksi Ominaisuus taulun ID nro 
--triggeri vastakkaiseksi kuin nyt? 
-- Eli se triggaa kun viedään ominaisuus tauluun ja if nimi on X, Y Z niin se sijoittaa sen myös oikeaan alitauluun? 
-- INSTEAD OF 

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


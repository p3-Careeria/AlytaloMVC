
DROP TABLE Sauna
DROP TABLE Valot
DROP TABLE Termostaatti
DROP TABLE Loki
DROP TABLE Ominaisuudet


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

)

----------------------------DEL/RESEED------------------------------------

DELETE FROM Sauna
DELETE FROM Valot
DELETE FROM Termostaatti
DELETE FROM Ominaisuudet
DELETE FROM Loki

DBCC CHECKIDENT ('Ominaisuudet', RESEED, 1000)  
DBCC CHECKIDENT ('Loki', RESEED, 1000)
DBCC CHECKIDENT ('Valot', RESEED, 1000)  
DBCC CHECKIDENT ('Termostaatti', RESEED, 1000)
DBCC CHECKIDENT ('Sauna', RESEED, 1000) 
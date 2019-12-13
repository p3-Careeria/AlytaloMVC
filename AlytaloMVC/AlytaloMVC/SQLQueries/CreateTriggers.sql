ALTER TRIGGER Tr_SaunaInsert ON Sauna 
AFTER INSERT 
AS
	DECLARE @OminaisuudenID int;
	DECLARE @Nimi NVARCHAR(15); 

	SELECT @OminaisuudenID = Id FROM inserted

	INSERT INTO [dbo].[Ominaisuudet] (OminaisuusId, Nimi) VALUES (@OminaisuudenID, 'Sauna') 
	INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuudenID, 'Saunan luonti', GETDATE())

GO

CREATE TRIGGER Tr_TermostaattiInsert ON Termostaatti
AFTER INSERT 
AS
	DECLARE @OminaisuudenID int;

	SELECT @OminaisuudenID = Id FROM inserted

	INSERT INTO [dbo].[Ominaisuudet] (OminaisuusId, Nimi) VALUES (@OminaisuudenID, 'Termostaatti') 
	INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuudenID, 'Termostaatin luonti', GETDATE())

GO

CREATE TRIGGER Tr_ValoInsert ON Valot
AFTER INSERT 
AS
	DECLARE @OminaisuudenID int;

	SELECT @OminaisuudenID = Id FROM inserted

	INSERT INTO [dbo].[Ominaisuudet] (OminaisuusId, Nimi) VALUES (@OminaisuudenID, 'Valo') 
	INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuudenID, 'Valon luonti', GETDATE())

GO
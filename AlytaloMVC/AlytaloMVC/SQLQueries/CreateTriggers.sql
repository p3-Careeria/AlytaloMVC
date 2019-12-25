----------------------- INSERT -----------------------
------------------------------------------------------

CREATE TRIGGER Tr_OminaisuusInsert ON Ominaisuudet
AFTER INSERT 
AS 
	SET NOCOUNT ON 

	DECLARE @OminaisuusId int; 
	DECLARE @Kaynnissa Bit;
	DECLARE @Lampo int;
	DECLARE @Teho int;
	DECLARE @Tavoite int;
	DECLARE @Tapahtuma NVARCHAR(100); 
	DECLARE @Nimi NVARCHAR(15);

	SELECT @Nimi = Nimi FROM inserted
	SELECT @OminaisuusId = OminaisuusId FROM inserted

	IF @Nimi = 'Sauna'	
	BEGIN
		INSERT INTO [dbo].[Sauna] (OminaisuusId, Kaynnissa, Lampo) VALUES (@OminaisuusId, 0, 20) 
		INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuusID, 'Sauna luotu', GETDATE())
	END 
	IF @Nimi = 'Termostaatti'	
	BEGIN
		INSERT INTO [dbo].[Termostaatti] (OminaisuusId, Tavoite, Lampo) VALUES (@OminaisuusId, 20, 20) 
		INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuusId, 'Termostaatti luotu', GETDATE())
	END
	IF @Nimi = 'Valot'	
	BEGIN
		INSERT INTO [dbo].[Valot] (OminaisuusId, Kaynnissa, Teho) VALUES (@OminaisuusId, 0, 0) 
		INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuusId, 'Valo luotu', GETDATE())
	END 
GO 


----------------------- DELETE -----------------------
------------------------------------------------------

CREATE TRIGGER Tr_OminaisuusDelete ON Ominaisuudet 
INSTEAD OF DELETE 
AS 
	DECLARE @Nimi NVARCHAR(15); 
	DECLARE @OminaisuusId int;

	SELECT @Nimi = Nimi FROM deleted 
	SELECT @OminaisuusId = OminaisuusId FROM deleted

	IF @Nimi = 'Sauna'	
	BEGIN
		DELETE FROM Sauna WHERE OminaisuusId = @OminaisuusId
		DELETE FROM Ominaisuudet WHERE OminaisuusId = @OminaisuusId 
		INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuusID, 'Sauna poistettu', GETDATE())
	END 
	IF @Nimi = 'Termostaatti'	
	BEGIN
		DELETE FROM Termostaatti WHERE OminaisuusId = @OminaisuusId
		DELETE FROM Ominaisuudet WHERE OminaisuusId = @OminaisuusId 
		INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuusID, 'Termostaatti poistettu', GETDATE())
	END
	IF @Nimi = 'Valot'	
	BEGIN
		DELETE FROM Valot WHERE OminaisuusId = @OminaisuusId
		DELETE FROM Ominaisuudet WHERE OminaisuusId = @OminaisuusId 
		INSERT INTO Loki(OminaisuusId, Tapahtuma, Ajakohta) VALUES (@OminaisuusID, 'Valo poistettu', GETDATE())
	END 
GO 


 
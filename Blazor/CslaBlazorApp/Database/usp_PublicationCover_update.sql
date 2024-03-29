USE [Csla]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_PublicationCover_update] (
    @PublicationId AS int,
	@Cover AS varbinary(max) = null
) AS 
-- Author: CCREK\GucciardiD  
-- Created: 03 Apr 2023  
-- Function: update Cover into dbo.Publication 
-- Modifications:     
BEGIN TRANSACTION 
BEGIN TRY 
-- update 
UPDATE
    [dbo].[Publication] WITH (ROWLOCK)
SET
	Cover = coalesce(@Cover, Cover)
WHERE
    Id = @PublicationId

SELECT @PublicationId

COMMIT TRANSACTION
END TRY 

BEGIN CATCH 

declare @ErrorMessage NVARCHAR(4000);
declare @ErrorSeverity INT;
declare @ErrorState INT;

SELECT
    @ErrorMessage = ERROR_MESSAGE(),
    @ErrorSeverity = ERROR_SEVERITY(),
    @ErrorState = ERROR_STATE();

RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);

ROLLBACK TRANSACTION
END CATCH;
USE [Csla]
GO
IF EXISTS (
    SELECT
        *
    FROM
        sys.objects
    WHERE
        object_id = OBJECT_ID(N'[dbo].[usp_Document_delete]')
        AND TYPE IN (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_Document_delete] 
GO

CREATE PROCEDURE [dbo].[usp_Document_delete] (@Id AS int) AS 
-- Author: Auto
-- Created: 29 Mar 2023
-- Function: DELETE a Document
-- Modifications:
BEGIN TRANSACTION 
BEGIN TRY 

DELETE [dbo].[Document] WITH (ROWLOCK) WHERE Id = @Id

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
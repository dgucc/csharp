USE [Csla]
GO
IF EXISTS (
    SELECT
        *
    FROM
        sys.objects
    WHERE
        object_id = OBJECT_ID(N'[dbo].[usp_Publication_delete]')
        AND TYPE IN (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_Publication_delete] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Publication_delete] (@Id AS int) AS 
-- Author: Auto
-- Created: 20 Mar 2023
-- Function: DELETE a Publication an all related records
-- Modifications:
BEGIN TRANSACTION 
BEGIN TRY 

-- Cascading deletions
DELETE [dbo].[Document] WITH (ROWLOCK) WHERE PublicationId = @Id

DELETE [dbo].[PublicationTopic] WITH (ROWLOCK) WHERE PublicationId = @Id

DELETE [dbo].[Publication] WITH (ROWLOCK) WHERE Id = @Id 

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
GO

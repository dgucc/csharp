USE [Csla]
GO
IF EXISTS (
SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_PublicationTopic_insert]') AND type in (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_PublicationTopic_insert]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_PublicationTopic_insert] (
    @TopicId AS nvarchar(255),
    @PublicationId AS int
) AS -- Author: CCREK\GucciardiD  
-- Created: 16 Mar 2023  
-- Function: Inserts a dbo.PublicationTopic table record      
-- Modifications:     
BEGIN TRANSACTION 
BEGIN TRY 

-- insert  
INSERT
    [dbo].[PublicationTopic] (TopicId, PublicationId)
    VALUES(
        @TopicId,
        @PublicationId
    ) 
    
-- Return the new ID   
SELECT SCOPE_IDENTITY();

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



USE [Csla]
GO
IF EXISTS (
SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Document_insert]') AND type in (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_Document_insert]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Document_insert] (
        @FileName AS nvarchar(255) = NULL,
        @MimeType AS nvarchar(100) = NULL,
        @Extension AS nvarchar(10) = NULL,
        @CreatedOn AS datetime = NULL,
        @Description AS nvarchar(255) = NULL,
        @UploadedByUser AS nvarchar(255) = NULL,
        @File AS varbinary(max) = NULL,
        @Thumbnail AS varbinary(max) = NULL,
        @DocumentType AS nvarchar(100) = NULL,
        @Language AS nvarchar(25) = NULL,
        @PublicationId AS int = NULL
    ) AS 
    -- Author: CCREK\GucciardiD  
    -- Created: 16 Mar 2023  
    -- Function: Inserts a dbo.Document table record      
    -- Modifications:     
BEGIN TRANSACTION 

BEGIN TRY 
-- insert  
INSERT
    [dbo].[Document] (
        FileName,
        MimeType,
        Extension,
        CreatedOn,
        Description,
        UploadedByUser,
        [File],
        Thumbnail,
        DocumentType,
        [Language],
        PublicationId
    ) VALUES (
        @FileName,
        @MimeType,
        @Extension,
        (SELECT CURRENT_TIMESTAMP), --@CreatedOn,
        @Description,
        (SELECT SUSER_NAME()), --@UploadedByUser,		
        @File,
        @Thumbnail,
        @DocumentType,
        @Language,
        @PublicationId
    ) 
    
-- Update Publication.Cover if Thumbnail <> NULL
IF NOT(@Thumbnail IS NULL) AND @DocumentType = 'Report' 
BEGIN 
    EXEC [dbo].[usp_Publication_update] @Id = @PublicationId, @Cover = @Thumbnail
END 

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
USE [Csla]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_Document_insert] (
    @FileName AS nvarchar(255) = Null,
    @MimeType AS nvarchar(100) = Null,
    @Extension AS nvarchar(10) = Null,
    @CreatedOn AS datetime = Null,
    @Description AS nvarchar(255) = Null,
    @UploadedByUser AS nvarchar(255) = Null,
    @File AS varbinary(max) = Null,
    @Thumbnail AS varbinary(max) = Null,
    @DocumentType AS nvarchar(100) = Null,
    @Language AS nvarchar(20) = Null,
    @PublicationId AS int = Null
) AS -- Author: CCREK\GucciardiD  
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
        Language,
        PublicationId
    ) VALUES (
        @FileName,
        @MimeType,
        @Extension,
        (SELECT CURRENT_TIMESTAMP),--@CreatedOn,
        @Description,
        (SELECT SUSER_NAME()),--@UploadedByUser,		
        @File,
        @Thumbnail,
        @DocumentType,
        @Language,
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

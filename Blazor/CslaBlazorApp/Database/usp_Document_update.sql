USE [Csla]
GO
IF EXISTS (
    SELECT
        *
    FROM
        sys.objects
    WHERE
        object_id = OBJECT_ID(N'[dbo].[usp_Document_update]')
        AND TYPE IN (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_Document_update]
GO

CREATE PROCEDURE [dbo].[usp_Document_update] (
    @Id AS int,
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
) AS -- Author: GucciardiD
    -- Created: 16 Mar 2023 
    -- Function: Create or update a dbo.Document table record  
    -- Modifications:  
BEGIN TRANSACTION 
BEGIN TRY 

-- update 
UPDATE
    [dbo].[Document] WITH (ROWLOCK)
SET
    FileName = coalesce(@FileName, FileName),
    MimeType = coalesce(@MimeType, MimeType),
    Extension = coalesce(@Extension, Extension),
    CreatedOn = coalesce(@CreatedOn, CreatedOn),
    Description = coalesce(@Description, Description),
    UploadedByUser = coalesce(@UploadedByUser, UploadedByUser),
    [File] = coalesce(@File, [File]),
    Thumbnail = coalesce(@Thumbnail, Thumbnail),
    DocumentType = coalesce(@DocumentType, DocumentType),
    [Language] = coalesce(@Language, [Language]),
    PublicationId = coalesce(@PublicationId, PublicationId)
WHERE
    Id = @Id

-- Update Publication.Cover if Thumbnail <> NULL
IF NOT(@Thumbnail IS NULL) AND @DocumentType = 'Report' 
BEGIN 
	EXEC [dbo].[usp_Publication_update] @Id = @PublicationId, @Cover = @Thumbnail
END

SELECT @Id 
    
COMMIT TRANSACTION;

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
USE [Csla]
GO
SET
    ANSI_NULLS ON
GO
SET
    QUOTED_IDENTIFIER ON
GO
    CREATE PROCEDURE [dbo].[usp_Document_select] (@Id AS int) AS
SET
    NOCOUNT ON 
    -- Author: GucciardiD 
    -- Created: 21 Mar 2023 
    -- Function: Get a dbo.Document 
    -- Modifications:  
SELECT
    [Id],
    [FileName],
    [MimeType],
    [Extension],
    [CreatedOn],
    [Description],
    [UploadedByUser],
    [File],
    [Thumbnail],
    [DocumentType],
    [Language],
    [PublicationId]
FROM
    [dbo].[Document]
WHERE
    [Id] = @Id RETURN
USE [Csla]
GO
IF EXISTS (
    SELECT
        *
    FROM
        sys.objects
    WHERE
        object_id = OBJECT_ID(N'[dbo].[usp_Document_select]')
        AND TYPE IN (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_Document_select] 
GO
SET
    ANSI_NULLS ON
GO
SET
    QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Document_select] AS
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
    [dbo].[Document] WITH (UPDLOCK)
GO

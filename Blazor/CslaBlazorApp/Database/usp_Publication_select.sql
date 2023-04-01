USE [Csla]
GO

SET
    ANSI_NULLS ON
GO
SET
    QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[usp_Publication_select] (@Id AS int) AS 

SET NOCOUNT ON
-- Author: Auto 
-- Created: 20 Mar 2023 
-- Function: Get a dbo.Publication 
-- Modifications:  
SELECT
    [Id],
    [ApprovalDate],
    [PublishDate],
    [RequestorEmail],
    [TitleNl],
    [TitleFr],
    [TitleDe],
    [TitleEn],
    [Cover]
FROM
    [dbo].[Publication] WITH (UPDLOCK)
WHERE
    [Id] = @Id

RETURN
GO
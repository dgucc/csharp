USE [Csla]
GO
IF EXISTS (
SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Publications_select]') AND type in (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_Publications_select]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Publications_select] AS 

SET NOCOUNT ON

-- Author: Auto 
-- Created: 20 Mar 2023 
-- Function: Get List of dbo.Publication
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

RETURN
GO
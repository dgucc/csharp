USE [Csla]
GO
IF EXISTS (
SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Publication_insert]') AND type in (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_Publication_insert]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Publication_insert] (
        @LegislativeLevel AS nvarchar(255) = Null,
    @PublicationType AS nvarchar(255) = Null,
    @ApprovedBy AS nvarchar(255) = Null,
    @ApprovalDate AS datetime = Null,
    @PublishDate AS datetime = Null,
    @RequestorEmail AS nvarchar(255) = Null,
    @TitleNl AS nvarchar(255) = Null,
    @TitleFr AS nvarchar(255) = Null,
    @TitleDe AS nvarchar(255) = Null,
    @TitleEn AS nvarchar(255) = Null,
    @HeaderNl AS nvarchar(max) = Null,
    @HeaderFr AS nvarchar(max) = Null,
    @HeaderDe AS nvarchar(max) = Null,
    @HeaderEn AS nvarchar(max) = Null,
	@Cover AS varbinary(max) = Null
) AS 
-- Author: CCREK\GucciardiD  
-- Created: 16 Mar 2023  
-- Function: Inserts a dbo.Publication table record      
-- Modifications:     
BEGIN TRANSACTION BEGIN TRY -- insert  
INSERT
    [dbo].[Publication] (
        LegislativeLevel,
        PublicationType,
        ApprovedBy,
        ApprovalDate,
        PublishDate,
        RequestorEmail,
        TitleNl,
        TitleFr,
        TitleDe,
        TitleEn,
        HeaderNl,
        HeaderFr,
        HeaderDe,
        HeaderEn,
		[Cover]
    )
VALUES
    (
        @LegislativeLevel,
        @PublicationType,
        @ApprovedBy,
        @ApprovalDate,
        @PublishDate,
        @RequestorEmail,
        @TitleNl,
        @TitleFr,
        @TitleDe,
        @TitleEn,
        @HeaderNl,
        @HeaderFr,
        @HeaderDe,
        @HeaderEn,
		@Cover
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
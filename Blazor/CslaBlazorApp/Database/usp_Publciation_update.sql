IF EXISTS (
SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Publication_update]') AND type in (N'P', N'PC')
) DROP PROCEDURE [dbo].[usp_Publication_update]
GO

CREATE PROCEDURE [dbo].[usp_Publication_update] (
        @Id as int,
        @LegislativeLevel as nvarchar(255) = Null,
        @PublicationType as nvarchar(255) = Null,
        @ApprovedBy as nvarchar(255) = Null,
        @ApprovalDate as datetime = Null,
        @PublishDate as datetime = Null,
        @RequestorEmail as nvarchar(255) = Null,
        @TitleNl as nvarchar(255) = Null,
        @TitleFr as nvarchar(255) = Null,
        @TitleDe as nvarchar(255) = Null,
        @TitleEn as nvarchar(255) = Null,
        @HeaderNl as nvarchar(max) = Null,
        @HeaderFr as nvarchar(max) = Null,
        @HeaderDe as nvarchar(max) = Null,
        @HeaderEn as nvarchar(max) = Null,
		@Cover as varbinary(max) = Null
    ) AS -- Author: Auto 
    -- Created: 16 Mar 2023 
    -- Function: Create or update a dbo.Publication table record  
    -- Modifications:  
BEGIN TRANSACTION 
BEGIN TRY 

-- update 
UPDATE
    [dbo].[Publication]
SET
    LegislativeLevel = coalesce(@LegislativeLevel, LegislativeLevel),
    PublicationType = coalesce(@PublicationType, PublicationType),
    ApprovedBy = coalesce(@ApprovedBy, ApprovedBy),
    ApprovalDate = coalesce(@ApprovalDate, ApprovalDate),
    PublishDate = coalesce(@PublishDate, PublishDate),
    RequestorEmail = coalesce(@RequestorEmail, RequestorEmail),
    TitleNl = coalesce(@TitleNl, TitleNl),
    TitleFr = coalesce(@TitleFr, TitleFr),
    TitleDe = coalesce(@TitleDe, TitleDe),
    TitleEn = coalesce(@TitleEn, TitleEn),
    HeaderNl = coalesce(@HeaderNl, HeaderNl),
    HeaderFr = coalesce(@HeaderFr, HeaderFr),
    HeaderDe = coalesce(@HeaderDe, HeaderDe),
    HeaderEn = coalesce(@HeaderEn, HeaderEn),
	Cover = coalesce(@Cover, Cover)
WHERE
    Id = @Id

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
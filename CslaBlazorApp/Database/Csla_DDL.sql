USE CSLA
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Document]
DROP TABLE [dbo].[PublicationTopic]
DROP TABLE [dbo].[Publication]
GO

CREATE TABLE [dbo].[Publication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LegislativeLevel] [nvarchar](255) NULL,
	[PublicationType] [nvarchar](255) NULL,
	[ApprovedBy] [nvarchar](255) NULL,
	[ApprovalDate] [datetime] NULL,
	[PublishDate] [datetime] NULL,
	[RequestorEmail] [nvarchar](255) NULL,
	[TitleNl] [nvarchar](255) NULL,
	[TitleFr] [nvarchar](255) NULL,
	[TitleDe] [nvarchar](255) NULL,
	[TitleEn] [nvarchar](255) NULL,
	[HeaderNl] [nvarchar](max) NULL,
	[HeaderFr] [nvarchar](max) NULL,
	[HeaderDe] [nvarchar](max) NULL,
	[HeaderEn] [nvarchar](max) NULL,
	[Cover] [varbinary](max) NULL,
 CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED ([Id])
 )
 GO
 
 CREATE TABLE [dbo].[Document](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](255) NULL,
	[MimeType] [nvarchar](100) NULL,
	[Extension] [nvarchar](10) NULL,
	[CreatedOn] [datetime] NULL,
	[Description] [nvarchar](255) NULL,
	[UploadedByUser] [nvarchar](255) NULL,
	[File] [varbinary](max) NULL,
	[Thumbnail] [varbinary](max) NULL,
	[DocumentType] [nvarchar](100) NULL,
	[IsFR] [bit] NULL,
	[IsNL] [bit] NULL,
	[IsDE] [bit] NULL,
	[IsEN] [bit] NULL,
	[PublicationId] [int] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([Id])
 )
 GO
 
 CREATE TABLE [dbo].[PublicationTopic](
	[TopicId] [nvarchar](255) NOT NULL,
	[PublicationId] [int] NOT NULL,
 CONSTRAINT [PK_PublicationTopic] PRIMARY KEY CLUSTERED ([TopicId],[PublicationId])
 )
 GO

-- Foreign Keys
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_PublicationID] FOREIGN KEY([PublicationId])
REFERENCES [dbo].[Publication] ([Id])
GO

ALTER TABLE [dbo].[PublicationTopic]  WITH CHECK ADD  CONSTRAINT [FK_PublicationTopic_PublicationID] FOREIGN KEY([PublicationId])
REFERENCES [dbo].[Publication] ([Id])
GO

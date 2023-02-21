CREATE SCHEMA sandbox
GO

CREATE TABLE sandbox.Composite(
	Id int NOT NULL IDENTITY (1, 1),
	Parent int NOT NULL,
	Child int NOT NULL,
	Description nvarchar(100) NULL
)
GO
ALTER TABLE test.Composite ADD CONSTRAINT PK_Composite PRIMARY KEY CLUSTERED (Id)
GO
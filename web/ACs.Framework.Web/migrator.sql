/* Using Database sqlserver2008 and Connection String Server=.\sqlexpress;Database=Foo;Trusted_Connection=True;MultipleActiveResultSets=true */
/* 1: Migration1 migrating =================================================== */

/* Beginning Transaction */
/* DeleteColumn Foo Url */
DECLARE @default sysname, @sql nvarchar(max);

-- get name of default constraint
SELECT @default = name
FROM sys.default_constraints
WHERE parent_object_id = object_id('[dbo].[Foo]')
AND type = 'D'
AND parent_column_id = (
SELECT column_id
FROM sys.columns
WHERE object_id = object_id('[dbo].[Foo]')
AND name = 'Url'
);

-- create alter table command to drop constraint as string and run it
SET @sql = N'ALTER TABLE [dbo].[Foo] DROP CONSTRAINT ' + @default;
EXEC sp_executesql @sql;

-- now we can finally drop column
ALTER TABLE [dbo].[Foo] DROP COLUMN [Url];

GO

/* AlterTable Foo */
/* No SQL statement executed. */

/* CreateColumn Foo Email String */
ALTER TABLE [dbo].[Foo] ADD [Email] NVARCHAR(255) NOT NULL
GO

INSERT INTO [dbo].[Foo] ([Name], [Email]) VALUES ('Something 1', 'user1@domain.com.br')
GO
INSERT INTO [dbo].[Foo] ([Name], [Email]) VALUES ('Something 2', 'user2@domain.com.br')
GO
/* -> 2 Insert operations completed in 00:00:00.0050019 taking an average of 00:00:00.0025009 */
INSERT INTO [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (1, '2015-12-24T17:26:57', 'Migration1')
GO
/* Committing Transaction */
/* 1: Migration1 migrated */

/* Task completed. */

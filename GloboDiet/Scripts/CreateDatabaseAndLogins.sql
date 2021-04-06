
/*

-- login
CREATE LOGIN [UserManagerUser] WITH PASSWORD='tsM3PhbtZWn91'
GO

-- db
DROP DATABASE [UserManager] 
GO

CREATE DATABASE [UserManager]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 30 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO

-- switch to the new before, "use" command is not implemented
CREATE USER [UserManagerUser]
	FOR LOGIN [UserManagerUser]
	WITH DEFAULT_SCHEMA = dbo
GO
EXEC sp_addrolemember N'db_owner', N'UserManagerUser'
GO

*/
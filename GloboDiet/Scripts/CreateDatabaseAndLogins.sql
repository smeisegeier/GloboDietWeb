/*

-- login
CREATE LOGIN [GloboDietWebUser] WITH PASSWORD='tsM3PhbtZWn91'
GO

-- db CAREFUL! THIS REPLACES THE UI CREATION PROCESS IN AZURE PORTAL !!
DROP DATABASE [GloboDietWeb] 
GO

CREATE DATABASE [GloboDietWeb]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 30 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO

-- switch to the new before, "use" command is not implemented
CREATE USER [GloboDietWebUser]
	FOR LOGIN [GloboDietWebUser]
	WITH DEFAULT_SCHEMA = dbo
GO
EXEC sp_addrolemember N'db_owner', N'GloboDietWebUser'
GO

*/
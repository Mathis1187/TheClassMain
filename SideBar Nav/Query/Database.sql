-- 1. Crée la base de données (si elle n'existe pas déjà)
CREATE DATABASE TheClassMainDB;
GO

-- 2. Crée un login SQL Server (au niveau du serveur)
CREATE LOGIN root WITH PASSWORD = '1234';
GO

-- 3. Associe ce login à un utilisateur dans la base TheClassMainDB
USE TheClassMainDB;
GO
CREATE USER root FOR LOGIN root;
GO

-- 4. Donne à l'utilisateur les droits de propriétaire de la base
ALTER ROLE db_owner ADD MEMBER root;
GO

-- Script: 03_CreateTables_Users. sql
-- Description: Create User Tables (Customers, Admins)
USE SchoolSuppliesDB;
GO

-- Table: Customers
CREATE TABLE shop.Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Address NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_Customers_Email 
    ON shop.Customers(Email);
GO

-- Table: Admins
CREATE TABLE shop.Admins (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'STAFF' CHECK (Role IN ('SUPER_ADMIN', 'ADMIN', 'STAFF')),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_Admins_Username 
    ON shop.Admins(Username);
GO

CREATE NONCLUSTERED INDEX IX_Admins_Email 
    ON shop.Admins(Email);
GO

PRINT 'User tables (Customers, Admins) created successfully!';
GO
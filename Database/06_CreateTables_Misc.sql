-- Script: 06_CreateTables_Misc.sql
-- Description: Create Miscellaneous Tables (ContactMessages, Banners)
-- =============================================

USE SchoolSuppliesDB;
GO

-- Table: ContactMessages
CREATE TABLE shop.ContactMessages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NULL,
    Subject NVARCHAR(200) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'NEW' CHECK (Status IN ('NEW', 'READ', 'REPLIED')),
    AdminReply NVARCHAR(MAX) NULL,
    IpAddress NVARCHAR(45) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    RepliedAt DATETIME2 NULL
);
GO

CREATE NONCLUSTERED INDEX IX_ContactMessages_CustomerId 
    ON shop.ContactMessages(CustomerId);
GO

CREATE NONCLUSTERED INDEX IX_ContactMessages_Status 
    ON shop.ContactMessages(Status);
GO

-- Table: Banners
CREATE TABLE shop. Banners (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    ImageUrl NVARCHAR(500) NOT NULL,
    Status BIT NOT NULL DEFAULT 1
);
GO

CREATE NONCLUSTERED INDEX IX_Banners_Status 
    ON shop.Banners(Status);
GO

PRINT 'Miscellaneous tables (ContactMessages, Banners) created successfully!';
GO
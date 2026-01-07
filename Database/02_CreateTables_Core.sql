-- Script:  02_CreateTables_Core.sql
-- Description: Create Core Tables (Categories, Brands, Products, ProductImages)

USE SchoolSuppliesDB;
GO

-- Table: Categories
CREATE TABLE shop.Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    ImageUrl NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_Categories_CategoryName 
    ON shop.Categories(CategoryName);
GO

-- Table: Brands
CREATE TABLE shop. Brands (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BrandName NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_Brands_BrandName 
    ON shop. Brands(BrandName);
GO

-- Table: Products
CREATE TABLE shop.Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CategoryId INT NOT NULL,
    BrandId INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    SalePrice DECIMAL(10, 2) NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    SoldCount INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_Products_CategoryId 
    ON shop.Products(CategoryId);
GO

CREATE NONCLUSTERED INDEX IX_Products_BrandId 
    ON shop.Products(BrandId);
GO

CREATE NONCLUSTERED INDEX IX_Products_Price 
    ON shop.Products(Price);
GO

-- Table: ProductImages
CREATE TABLE shop.ProductImages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    ImageUrl NVARCHAR(500) NOT NULL,
    IsPrimary BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_ProductImages_ProductId 
    ON shop.ProductImages(ProductId);
GO

PRINT '4 Core tables created successfully!';
GO
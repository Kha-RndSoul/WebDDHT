-- Script: 05_CreateTables_Marketing.sql
-- Description: Create Marketing Tables (Coupons, ProductReviews, PaymentTransactions)
-- =============================================

USE SchoolSuppliesDB;
GO

-- Table: Coupons
CREATE TABLE shop. Coupons (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CouponCode NVARCHAR(50) NOT NULL UNIQUE,
    DiscountType NVARCHAR(20) NOT NULL CHECK (DiscountType IN ('PERCENTAGE', 'FIXED_AMOUNT')),
    DiscountValue DECIMAL(10, 2) NOT NULL,
    MinOrderAmount DECIMAL(10, 2) NOT NULL DEFAULT 0,
    MaxUses INT NOT NULL DEFAULT 0,
    UsedCount INT NOT NULL DEFAULT 0,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_Coupons_CouponCode 
    ON shop.Coupons(CouponCode);
GO

CREATE NONCLUSTERED INDEX IX_Coupons_IsActive 
    ON shop. Coupons(IsActive);
GO

-- Table: ProductReviews
CREATE TABLE shop.ProductReviews (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    CustomerId INT NOT NULL,
    Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
    Comment NVARCHAR(MAX) NULL,
    Status BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_ProductReviews_ProductId 
    ON shop.ProductReviews(ProductId);
GO

CREATE NONCLUSTERED INDEX IX_ProductReviews_CustomerId 
    ON shop.ProductReviews(CustomerId);
GO

-- Table: PaymentTransactions
CREATE TABLE shop.PaymentTransactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    PaymentMethod NVARCHAR(20) NOT NULL CHECK (PaymentMethod IN ('COD', 'BANK_TRANSFER', 'MOMO', 'VNPAY', 'ZALOPAY')),
    PaymentStatus NVARCHAR(20) NOT NULL DEFAULT 'PENDING' CHECK (PaymentStatus IN ('PENDING', 'PAID', 'FAILED', 'REFUNDED')),
    Amount DECIMAL(10, 2) NOT NULL,
    TransactionNote NVARCHAR(MAX) NULL,
    PaidAt DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_PaymentTransactions_OrderId 
    ON shop.PaymentTransactions(OrderId);
GO

CREATE NONCLUSTERED INDEX IX_PaymentTransactions_PaymentStatus 
    ON shop.PaymentTransactions(PaymentStatus);
GO

PRINT '3 Marketing tables created successfully!';
GO
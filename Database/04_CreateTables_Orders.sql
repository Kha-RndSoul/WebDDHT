-- Script: 04_CreateTables_Orders.sql
-- Description: Create Order Tables (Orders, OrderDetails, CartItems, OrderCoupons)

USE SchoolSuppliesDB;
GO

-- Table: Orders
CREATE TABLE shop.Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    OrderCode NVARCHAR(50) NOT NULL UNIQUE,
    OrderStatus NVARCHAR(20) NOT NULL DEFAULT 'PENDING',
    PaymentMethod NVARCHAR(20) NULL,
    PaymentStatus NVARCHAR(20) NULL,
    TotalAmount DECIMAL(15, 2) NOT NULL DEFAULT 0,
    ShippingName NVARCHAR(100) NULL,
    ShippingPhone NVARCHAR(20) NULL,
    ShippingAddress NVARCHAR(500) NULL,
    Note NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_Orders_CustomerId 
    ON shop.Orders(CustomerId);
GO

CREATE NONCLUSTERED INDEX IX_Orders_OrderCode 
    ON shop.Orders(OrderCode);
GO

-- Table: OrderDetails
CREATE TABLE shop.OrderDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    Price DECIMAL(15, 2) NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    Subtotal DECIMAL(15, 2) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_OrderDetails_OrderId 
    ON shop.OrderDetails(OrderId);
GO

CREATE NONCLUSTERED INDEX IX_OrderDetails_ProductId 
    ON shop.OrderDetails(ProductId);
GO

-- Table: CartItems
CREATE TABLE shop.CartItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    AddedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_CartItems_CustomerProduct UNIQUE (CustomerId, ProductId)
);
GO

CREATE NONCLUSTERED INDEX IX_CartItems_CustomerId 
    ON shop.CartItems(CustomerId);
GO

CREATE NONCLUSTERED INDEX IX_CartItems_ProductId 
    ON shop.CartItems(ProductId);
GO

-- Table: OrderCoupons
CREATE TABLE shop.OrderCoupons (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    CouponId INT NOT NULL,
    DiscountAmount DECIMAL(15, 2) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE NONCLUSTERED INDEX IX_OrderCoupons_OrderId 
    ON shop. OrderCoupons(OrderId);
GO

CREATE NONCLUSTERED INDEX IX_OrderCoupons_CouponId 
    ON shop.OrderCoupons(CouponId);
GO

PRINT '4 Order tables created successfully!';
GO
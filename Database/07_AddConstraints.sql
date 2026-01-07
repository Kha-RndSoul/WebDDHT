-- =============================================
-- Script: 07_AddConstraints.sql
-- Description: Add Foreign Key Constraints
-- =============================================

USE SchoolSuppliesDB;
GO

-- ========================================
-- FOREIGN KEYS FOR CORE TABLES
-- ========================================

-- Products -> Categories
ALTER TABLE shop.Products
ADD CONSTRAINT FK_Products_Categories
    FOREIGN KEY (CategoryId) REFERENCES shop.Categories(Id)
    ON DELETE NO ACTION;
GO

-- Products -> Brands
ALTER TABLE shop.Products
ADD CONSTRAINT FK_Products_Brands
    FOREIGN KEY (BrandId) REFERENCES shop.Brands(Id)
    ON DELETE NO ACTION;
GO

-- ProductImages -> Products
ALTER TABLE shop. ProductImages
ADD CONSTRAINT FK_ProductImages_Products
    FOREIGN KEY (ProductId) REFERENCES shop.Products(Id)
    ON DELETE CASCADE;
GO

-- ========================================
-- FOREIGN KEYS FOR ORDER TABLES
-- ========================================

-- Orders -> Customers
ALTER TABLE shop.Orders
ADD CONSTRAINT FK_Orders_Customers
    FOREIGN KEY (CustomerId) REFERENCES shop.Customers(Id)
    ON DELETE NO ACTION;
GO

-- OrderDetails -> Orders
ALTER TABLE shop.OrderDetails
ADD CONSTRAINT FK_OrderDetails_Orders
    FOREIGN KEY (OrderId) REFERENCES shop.Orders(Id)
    ON DELETE CASCADE;
GO

-- OrderDetails -> Products
ALTER TABLE shop.OrderDetails
ADD CONSTRAINT FK_OrderDetails_Products
    FOREIGN KEY (ProductId) REFERENCES shop.Products(Id)
    ON DELETE NO ACTION;
GO

-- CartItems -> Customers
ALTER TABLE shop.CartItems
ADD CONSTRAINT FK_CartItems_Customers
    FOREIGN KEY (CustomerId) REFERENCES shop.Customers(Id)
    ON DELETE CASCADE;
GO

-- CartItems -> Products
ALTER TABLE shop.CartItems
ADD CONSTRAINT FK_CartItems_Products
    FOREIGN KEY (ProductId) REFERENCES shop.Products(Id)
    ON DELETE CASCADE;
GO

-- OrderCoupons -> Orders
ALTER TABLE shop. OrderCoupons
ADD CONSTRAINT FK_OrderCoupons_Orders
    FOREIGN KEY (OrderId) REFERENCES shop.Orders(Id)
    ON DELETE CASCADE;
GO

-- OrderCoupons -> Coupons
ALTER TABLE shop.OrderCoupons
ADD CONSTRAINT FK_OrderCoupons_Coupons
    FOREIGN KEY (CouponId) REFERENCES shop.Coupons(Id)
    ON DELETE NO ACTION;
GO

-- ========================================
-- FOREIGN KEYS FOR MARKETING TABLES
-- ========================================

-- ProductReviews -> Products
ALTER TABLE shop.ProductReviews
ADD CONSTRAINT FK_ProductReviews_Products
    FOREIGN KEY (ProductId) REFERENCES shop.Products(Id)
    ON DELETE CASCADE;
GO

-- ProductReviews -> Customers
ALTER TABLE shop.ProductReviews
ADD CONSTRAINT FK_ProductReviews_Customers
    FOREIGN KEY (CustomerId) REFERENCES shop.Customers(Id)
    ON DELETE CASCADE;
GO

-- PaymentTransactions -> Orders
ALTER TABLE shop.PaymentTransactions
ADD CONSTRAINT FK_PaymentTransactions_Orders
    FOREIGN KEY (OrderId) REFERENCES shop.Orders(Id)
    ON DELETE CASCADE;
GO

-- ========================================
-- FOREIGN KEYS FOR MISC TABLES
-- ========================================

-- ContactMessages -> Customers
ALTER TABLE shop.ContactMessages
ADD CONSTRAINT FK_ContactMessages_Customers
    FOREIGN KEY (CustomerId) REFERENCES shop.Customers(Id)
    ON DELETE SET NULL;
GO

PRINT 'All foreign key constraints added successfully!';
GO
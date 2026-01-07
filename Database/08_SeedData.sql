-- Script: 08_SeedData.sql
-- Description: Insert Sample Data
-- =============================================

USE SchoolSuppliesDB;
GO

-- ========================================
-- INSERT BANNERS
-- ========================================
SET IDENTITY_INSERT shop.Banners ON;
GO

INSERT INTO shop.Banners (Id, Title, ImageUrl, Status)
VALUES
    (1, N'Cùng sáng tạo với WONDERLINE', N'assets/images/banners/banner1.png', 1),
    (2, N'Tự tin học tốt cùng SCHOOLLINE', N'assets/images/banners/banner2.png', 1),
    (3, N'Giấy in cao cấp, giá hời quá xịn', N'assets/images/banners/banner3.png', 1),
    (4, N'Học cụ xinh-Rinh Deal xịn', N'assets/images/banners/banner4.png', 1),
    (5, N'VIP AVAKIDS', N'assets/images/banners/banner5.png', 1),
    (6, N'Trao quà tặng-Gửi yêu thương', N'assets/images/banners/banner6.png', 1),
    (7, N'Tết Việt', N'assets/images/banners/banner7.png', 1);
GO

SET IDENTITY_INSERT shop.Banners OFF;
GO

-- ========================================
-- INSERT CATEGORIES
-- ========================================
SET IDENTITY_INSERT shop.Categories ON;
GO

INSERT INTO shop.Categories (Id, CategoryName, ImageUrl)
VALUES
    (1, N'Bút viết', N'assets/images/categories/Bút viết.png'),
    (2, N'Sổ vở', N'assets/images/categories/Sổ vở.png'),
    (3, N'Đồ dùng vẽ', N'assets/images/categories/Đồ dùng vẽ.png'),
    (4, N'Balo & cặp', N'assets/images/categories/Balo & cặp.png'),
    (5, N'Máy tính', N'assets/images/categories/Máy tính.png'),
    (6, N'Đèn học', N'assets/images/categories/Đèn học.png'),
    (7, N'Giấy', N'assets/images/categories/Giấy in.png'),
    (8, N'Thước, compa & tẩy', N'assets/images/categories/Giấy in.png');
GO

SET IDENTITY_INSERT shop.Categories OFF;
GO

-- ========================================
-- INSERT BRANDS
-- ========================================
SET IDENTITY_INSERT shop.Brands ON;
GO

INSERT INTO shop.Brands (Id, BrandName)
VALUES
    (1, N'Thiên Long'),
    (2, N'Flexoffice'),
    (3, N'Điểm 10'),
    (4, N'Campus'),
    (5, N'Hồng Hà'),
    (6, N'Hải Tiến'),
    (7, N'Colokit'),
    (8, N'Deli'),
    (9, N'Miti'),
    (10, N'Mr. Vui'),
    (11, N'Flexio'),
    (12, N'Casio'),
    (13, N'Rạng Đông'),
    (14, N'Panasonic'),
    (15, N'Double A'),
    (16, N'IK Plus'),
    (17, N'Jamlos'),
    (18, N'King Jim');
GO

SET IDENTITY_INSERT shop.Brands OFF;
GO

-- ========================================
-- INSERT PRODUCTS (Sample - First 30 products)
-- ========================================
SET IDENTITY_INSERT shop.Products ON;
GO
INSERT INTO shop. Products (Id, ProductName, Description, CategoryId, BrandId, Price, SalePrice, StockQuantity, SoldCount, IsActive)
VALUES
    (1, N'Bút gel Doraemon TL', N'Bút có thiết kế đơn giản nhưng khoa học, thân tròn, nhỏ rất phù hợp với tay cầm của học sinh tiểu học. Thân bút bằng nhựa trong đẹp', 1, 1, 12000. 00, 10000.00, 100, 150, 1),
    (2, N'Bút gel B TL', N'Kiểu dáng hiện đại, dắt bút bằng kim loại sáng bóng sang trọng rất phù hợp với khách hàng là nhân viên văn phòng. ', 1, 1, 13000.00, 11000.00, 80, 120, 1),
    (3, N'Bút gel Fasgel TL', N'Nét viết êm tru, mực ra đều, liên tục.  Ngòi bút cao cấp, sang trọng.  Thiết Kế tinh vi, nghệ thuật', 1, 1, 7000.00, 6000.00, 200, 200, 1),
    (4, N'Bút gel Yoyee TL', N'Bút viết mượt, nét đều, mực khô nhanh — lý tưởng cho học sinh, sinh viên và nhân viên văn phòng muốn chữ rõ ràng, không lem. ', 1, 1, 6000.00, 5000.00, 150, 180, 1),
    (5, N'Bút gel Demon Slayer TL', N'Đầu bút bền, viết êm, phù hợp học sinh đam mê truyện tranh demon slayer', 1, 1, 12000.00, 10000.00, 50, 80, 1),
    (6, N'Bút gel 028 TL', N'Bút có thiết kế đơn giản nhưng khoa học, thân tròn, nhỏ rất phù hợp với tay cầm của học sinh tiểu học.  Thân bút bằng nhựa trong đẹp', 1, 1, 5000.00, 4000.00, 180, 220, 1),
    (7, N'Bút chì Gỗ 2B TL', N'Thiết kế nhỏ gọn, cầm vừa tay, chất liệu gỗ bền đẹp, ruột chì khó gãy, dễ dàng gọt. ', 1, 1, 4000.00, 3000.00, 120, 160, 1),
    (8, N'Bút chì gỗ điểm 10', N'Khi sử dụng, ngòi không bị gãy vụn, ít hao, dễ xóa sạch bằng gôm, đặc biệt hạn chế làm bẩn tay và quần áo.', 1, 3, 3500.00, 3000.00, 140, 170, 1),
    (9, N'Bút chì gỗ Neon CLK', N'Nét đậm, độ lướt siêu êm phù hợp với việc tô trắc nghiệm hay vẽ phác thảo. ', 1, 7, 3000.00, 2000.00, 100, 50, 1),
    (10, N'Bút chì gỗ HB Huvellou', N'Nét đậm, lướt rất nhẹ nhàng trên bề mặt giấy dùng để đánh bóng các bức vẽ, đạt đến nhiều mức độ sáng tối khác nhau', 1, 1, 4500.00, 4000.00, 90, 110, 1),
    (11, N'Vở 4 ô ly 48 trang Hồng Hà', N'Sản phẩm được sản xuất theo công nghệ mới, độ trắng tự nhiên, không gây lóa mắt, mỏi mắt khi đọc viết.  Bìa vở đẹp', 2, 5, 8000.00, 7000.00, 300, 250, 1),
    (12, N'Vở 4 ô ly 80 trang Hồng Hà', N'Sản phẩm được sản xuất theo công nghệ mới, độ trắng tự nhiên, không gây lóa mắt, mỏi mắt khi đọc viết. Bìa vở đẹp', 2, 5, 12000.00, 10000.00, 250, 200, 1),
    (13, N'Vở 4 ô ly 48 trang Campus', N'Giấy mịn, viết êm tay, không thấm mực. Bìa vở được thiết kế trẻ trung, bắt mắt. ', 2, 4, 10000.00, 9000.00, 200, 120, 1),
(14, N'Vở 4 ô ly 80 trang Campus', N'Giấy mịn, viết êm tay, không thấm mực. Bìa vở được thiết kế trẻ trung, bắt mắt.', 2, 4, 15000.00, 13000.00, 180, 90, 1),
    (15, N'Vở 4 ô ly 48 trang Hải Tiến', N'Chất lượng giấy cao cấp, dòng kẻ rõ nét, giúp học sinh viết chữ đẹp hơn. ', 2, 6, 7000.00, 6000.00, 120, 60, 1),
    (16, N'Vở 4 ô ly 80 trang Hải Tiến', N'Chất lượng giấy cao cấp, dòng kẻ rõ nét, giúp học sinh viết chữ đẹp hơn.', 2, 6, 11000.00, 9000.00, 160, 80, 1),
    (17, N'Sổ tay lò xo A5 Hồng Hà', N'Sổ tay tiện lợi, bìa cứng cáp, giấy trắng mịn, thích hợp ghi chép cá nhân hoặc công việc. ', 2, 5, 20000.00, 18000.00, 50, 30, 1),
    (18, N'Sổ tay lò xo A5 Campus', N'Thiết kế lò xo kép chắc chắn, giấy chất lượng cao, dễ dàng lật trang. ', 2, 4, 25000.00, 22000.00, 60, 40, 1),
    (19, N'Sổ tay lò xo A5 Hải Tiến', N'Sổ tay nhỏ gọn, bìa in hình đẹp mắt, giấy viết không lem. ', 2, 6, 18000.00, 16000.00, 40, 20, 1),
    (20, N'Bút Sáp màu 12 màu Doraemon CLK', N'Gồm có các màu thông dụng, mang đến sự tiện dụng cho các bé khi tô màu. Độ mịn cao, màu sắc tươi sáng, sinh động', 3, 7, 35000.00, 32000.00, 70, 45, 1),
    (21, N'Bút Sáp màu 24 màu Doraemon CLK', N'Bộ sáp màu đa dạng, giúp bé thỏa sức sáng tạo.  Màu sắc chuẩn, bền màu theo thời gian. ', 3, 7, 65000.00, 60000.00, 40, 25, 1),
    (22, N'Bút Sáp màu 12 màu Điểm 10', N'Màu sắc tươi sáng, tô êm, độ phủ màu tốt.  An toàn cho sức khỏe người sử dụng. ', 3, 3, 30000.00, 28000.00, 60, 40, 1),
    (23, N'Bút Sáp màu 24 màu Điểm 10', N'Hộp sáp màu tiện dụng, màu sắc rực rỡ, giúp các bức tranh thêm phần sinh động. ', 3, 3, 58000.00, 54000.00, 45, 35, 1),
    (24, N'Màu nước 12 màu Thiên Long', N'Màu nước dạng hũ, dễ pha màu, độ loang màu tốt, thích hợp cho học sinh học vẽ.', 3, 1, 45000.00, 40000.00, 30, 15, 1),
    (25, N'Màu nước 12 màu Colokit', N'Màu nước chất lượng cao, màu sắc tươi tắn, không độc hại. Kèm cọ vẽ tiện lợi.', 3, 7, 50000.00, 46000.00, 35, 20, 1),
    (26, N'Balo học sinh Miti', N'Balo siêu nhẹ, chống gù lưng, chất liệu bền bỉ, nhiều ngăn chứa đồ tiện dụng.', 4, 9, 350000.00, 320000.00, 20, 10, 1),
    (27, N'Balo học sinh Mr. Vui', N'Thiết kế thời trang, đệm lưng êm ái, quai đeo chắc chắn, phù hợp cho học sinh các cấp.', 4, 10, 380000.00, 350000.00, 15, 8, 1),
    (28, N'Balo chống gù Miti', N'Công nghệ chống gù hiện đại, bảo vệ cột sống của trẻ.  Họa tiết hoạt hình ngộ nghĩnh.', 4, 9, 450000.00, 410000.00, 12, 5, 1),
    (29, N'Balo chống gù Mr. Vui', N'Chất liệu trượt nước, dễ dàng vệ sinh.  Khóa kéo cao cấp, bền bỉ.', 4, 10, 480000.00, 440000.00, 10, 4, 1),
(30, N'Cặp táp học sinh Miti', N'Cặp táp truyền thống, form cứng cáp, bảo vệ sách vở không bị quăn góc.', 4, 9, 250000.00, 230000.00, 18, 9, 1);
GO

SET IDENTITY_INSERT shop.Products OFF;
GO

-- ========================================
-- INSERT SAMPLE CUSTOMERS
-- ========================================
SET IDENTITY_INSERT shop.Customers ON;
GO

INSERT INTO shop. Customers (Id, Email, PasswordHash, FullName, Phone, Address)
VALUES
    (1, N'customer1@email.com', N'$2a$10$hashed_password_1', N'Nguyễn Văn A', N'0901234567', N'123 Đường ABC, Quận 1, TP.HCM'),
    (2, N'customer2@email.com', N'$2a$10$hashed_password_2', N'Trần Thị B', N'0912345678', N'456 Đường XYZ, Quận 2, TP. HCM'),
    (3, N'customer3@email. com', N'$2a$10$hashed_password_3', N'Lê Văn C', N'0923456789', N'789 Đường DEF, Quận 3, TP.HCM');
GO

SET IDENTITY_INSERT shop.Customers OFF;
GO

-- ========================================
-- INSERT SAMPLE CONTACT MESSAGES
-- ========================================
SET IDENTITY_INSERT shop.ContactMessages ON;
GO

INSERT INTO shop. ContactMessages (Id, CustomerId, FullName, Email, Phone, Subject, Message, Status, AdminReply, IpAddress)
VALUES
    (1, 1, N'Nguyễn Văn A', N'customer1@email.com', N'0901234567', N'Hỏi về sản phẩm', N'Sản phẩm balo có màu xanh không?', N'NEW', NULL, N'192.168.1.1'),
    (2, 2, N'Trần Thị B', N'customer2@email. com', N'0912345678', N'Vấn đề giao hàng', N'Đơn hàng của tôi chưa nhận được', N'READ', NULL, N'192.168.1.2'),
    (3, 3, N'Lê Văn C', N'guest@email.com', N'0923456789', N'Yêu cầu hợp tác', N'Tôi muốn trở thành đối tác', N'NEW', NULL, N'192.168.1.3');
GO

SET IDENTITY_INSERT shop.ContactMessages OFF;
GO

PRINT 'Sample data inserted successfully! ';
PRINT 'Banners:  7 records';
PRINT 'Categories: 8 records';
PRINT 'Brands: 18 records';
PRINT 'Products: 30 records';
PRINT 'Customers: 3 records';
PRINT 'Contact Messages: 3 records';
GO
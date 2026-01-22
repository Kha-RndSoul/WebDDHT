using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebDDHT.Models;

namespace WebDDHT.Data
{
	/// <summary>
	/// DbContext cho School Supplies Shop - Entity Framework 6
	/// Qu?n l? 15 tables: categories, brands, products, customers, orders, etc.
	/// </summary>
	public class SchoolSuppliesContext : DbContext
	{
		// Constructor:  S? d?ng connection string t? Web.config
		public SchoolSuppliesContext() : base("name=SchoolSuppliesConnection")
		{
			// T?t lazy loading ð? tránh N+1 query problem
			Configuration.LazyLoadingEnabled = false;

			// T?t proxy creation
			Configuration.ProxyCreationEnabled = false;

			// Enable migrations (n?u s? d?ng Code First Migrations)
			Database.SetInitializer<SchoolSuppliesContext>(null);
		}

		#region DbSets - 15 Tables

		// ========== CORE TABLES (S?n ph?m) ==========

		/// <summary>
		/// Danh m?c s?n ph?m (Bút vi?t, S? v?, Balo, etc.)
		/// </summary>
		public virtual DbSet<Category> Categories { get; set; }

		/// <summary>
		/// Thýõng hi?u (Thiên Long, Deli, Casio, etc.)
		/// </summary>
		public virtual DbSet<Brand> Brands { get; set; }

		/// <summary>
		/// S?n ph?m (200 s?n ph?m vãn ph?ng ph?m)
		/// </summary>
		public virtual DbSet<Product> Products { get; set; }

		/// <summary>
		/// ?nh s?n ph?m (1 product có nhi?u ?nh)
		/// </summary>
		public virtual DbSet<ProductImage> ProductImages { get; set; }

		// ========== USER TABLES (Ngý?i dùng) ==========

		/// <summary>
		/// Khách hàng (email, password, full_name, phone, address)
		/// </summary>
		public virtual DbSet<Customer> Customers { get; set; }

		/// <summary>
		/// Qu?n tr? viên (username, email, role:  SUPER_ADMIN/ADMIN/STAFF)
		/// </summary>
		public virtual DbSet<Admin> Admins { get; set; }

		// ========== ORDER TABLES (Ðõn hàng) ==========

		/// <summary>
		/// Ðõn hàng (order_code, order_status, total_amount)
		/// </summary>
		public virtual DbSet<Order> Orders { get; set; }

		/// <summary>
		/// Chi ti?t ðõn hàng (product_id, quantity, unit_price, subtotal)
		/// </summary>
		public virtual DbSet<OrderDetail> OrderDetails { get; set; }

		/// <summary>
		/// Gi? hàng (customer_id, product_id, quantity)
		/// </summary>
		public virtual DbSet<CartItem> CartItems { get; set; }

		/// <summary>
		/// Giao d?ch thanh toán (COD, BANK_TRANSFER, MOMO, VNPAY, ZALOPAY)
		/// </summary>
		public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

		// ========== MARKETING TABLES ==========

		/// <summary>
		/// M? gi?m giá (coupon_code, discount_type, discount_value)
		/// </summary>
		public virtual DbSet<Coupon> Coupons { get; set; }

		/// <summary>
		/// Coupon ð? áp d?ng cho ðõn hàng
		/// </summary>
		public virtual DbSet<OrderCoupon> OrderCoupons { get; set; }

		/// <summary>
		/// Ðánh giá s?n ph?m (rating 1-5 stars, comment)
		/// </summary>
		public virtual DbSet<ProductReview> ProductReviews { get; set; }

		// ========== MISC TABLES ==========

		/// <summary>
		/// Banner qu?ng cáo trang ch?
		/// </summary>
		public virtual DbSet<Banner> Banners { get; set; }

		/// <summary>
		/// Tin nh?n liên h? t? khách hàng
		/// </summary>
		public virtual DbSet<ContactMessage> ContactMessages { get; set; }

		#endregion

		#region OnModelCreating - Fluent API Configuration

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// T?t pluralizing table names (gi? nguyên tên DbSet)
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			// ========== PRODUCT RELATIONSHIPS ==========

			// Product -> Category (Many-to-One)
			modelBuilder.Entity<Product>()
				.HasRequired(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.WillCascadeOnDelete(false); // Không xóa cascade

			// Product -> Brand (Many-to-One)
			modelBuilder.Entity<Product>()
				.HasRequired(p => p.Brand)
				.WithMany(b => b.Products)
				.HasForeignKey(p => p.BrandId)
				.WillCascadeOnDelete(false);

			// Product -> ProductImages (One-to-Many)
			modelBuilder.Entity<Product>()
				.HasMany(p => p.ProductImages)
				.WithRequired(pi => pi.Product)
				.HasForeignKey(pi => pi.ProductId)
				.WillCascadeOnDelete(true); // Xóa product ? xóa ?nh

			// Product -> OrderDetails (One-to-Many)
			modelBuilder.Entity<Product>()
				.HasMany(p => p.OrderDetails)
				.WithRequired(od => od.Product)
				.HasForeignKey(od => od.ProductId)
				.WillCascadeOnDelete(false);

			// Product -> CartItems (One-to-Many)
			modelBuilder.Entity<Product>()
				.HasMany(p => p.CartItems)
				.WithRequired(ci => ci.Product)
				.HasForeignKey(ci => ci.ProductId)
				.WillCascadeOnDelete(true); // Xóa product ? xóa cart item

			// Product -> ProductReviews (One-to-Many)
			modelBuilder.Entity<Product>()
				.HasMany(p => p.ProductReviews)
				.WithRequired(pr => pr.Product)
				.HasForeignKey(pr => pr.ProductId)
				.WillCascadeOnDelete(true);

			// ========== CUSTOMER RELATIONSHIPS ==========

			// Customer -> Orders (One-to-Many)
			modelBuilder.Entity<Customer>()
				.HasMany(c => c.Orders)
				.WithRequired(o => o.Customer)
				.HasForeignKey(o => o.CustomerId)
				.WillCascadeOnDelete(false);

			// Customer -> CartItems (One-to-Many)
			modelBuilder.Entity<Customer>()
				.HasMany(c => c.CartItems)
				.WithRequired(ci => ci.Customer)
				.HasForeignKey(ci => ci.CustomerId)
				.WillCascadeOnDelete(true); // Xóa customer ? xóa cart

			// Customer -> ProductReviews (One-to-Many)
			modelBuilder.Entity<Customer>()
				.HasMany(c => c.ProductReviews)
				.WithRequired(pr => pr.Customer)
				.HasForeignKey(pr => pr.CustomerId)
				.WillCascadeOnDelete(true);

			// Customer -> ContactMessages (One-to-Many, Optional)
			modelBuilder.Entity<Customer>()
				.HasMany(c => c.ContactMessages)
				.WithOptional(cm => cm.Customer)
				.HasForeignKey(cm => cm.CustomerId)
				.WillCascadeOnDelete(false);

			// ========== ORDER RELATIONSHIPS ==========

			// Order -> OrderDetails (One-to-Many)
			modelBuilder.Entity<Order>()
				.HasMany(o => o.OrderDetails)
				.WithRequired(od => od.Order)
				.HasForeignKey(od => od.OrderId)
				.WillCascadeOnDelete(true); // Xóa order ? xóa details

			// Order -> PaymentTransactions (One-to-Many)
			modelBuilder.Entity<Order>()
				.HasMany(o => o.PaymentTransactions)
				.WithRequired(pt => pt.Order)
				.HasForeignKey(pt => pt.OrderId)
				.WillCascadeOnDelete(true);

			// Order -> OrderCoupons (One-to-Many)
			modelBuilder.Entity<Order>()
				.HasMany(o => o.OrderCoupons)
				.WithRequired(oc => oc.Order)
				.HasForeignKey(oc => oc.OrderId)
				.WillCascadeOnDelete(true);

			// ========== COUPON RELATIONSHIPS ==========

			// Coupon -> OrderCoupons (One-to-Many)
			modelBuilder.Entity<Coupon>()
				.HasMany(c => c.OrderCoupons)
				.WithRequired(oc => oc.Coupon)
				.HasForeignKey(oc => oc.CouponId)
				.WillCascadeOnDelete(false);

			// ========== INDEXES ==========

			// Category. CategoryName index
			modelBuilder.Entity<Category>()
				.Property(c => c.CategoryName)
				.HasColumnAnnotation("Index",
					new System.ComponentModel.DataAnnotations.Schema.IndexAnnotation(
						new System.ComponentModel.DataAnnotations.Schema.IndexAttribute("IX_CategoryName")));

			// Brand.BrandName index
			modelBuilder.Entity<Brand>()
				.Property(b => b.BrandName)
				.HasColumnAnnotation("Index",
					new System.ComponentModel.DataAnnotations.Schema.IndexAnnotation(
						new System.ComponentModel.DataAnnotations.Schema.IndexAttribute("IX_BrandName")));

			// Customer.Email unique index
			modelBuilder.Entity<Customer>()
				.HasIndex(c => c.Email)
				.IsUnique()
				.HasName("IX_Customer_Email");

			// Admin.Username unique index
			modelBuilder.Entity<Admin>()
				.HasIndex(a => a.Username)
				.IsUnique()
				.HasName("IX_Admin_Username");

			// Order.OrderCode unique index
			modelBuilder.Entity<Order>()
				.HasIndex(o => o.OrderCode)
				.IsUnique()
				.HasName("IX_Order_OrderCode");

			// Coupon.CouponCode unique index
			modelBuilder.Entity<Coupon>()
				.HasIndex(c => c.CouponCode)
				.IsUnique()
				.HasName("IX_Coupon_CouponCode");

			// CartItem unique constraint (customer_id + product_id)
			modelBuilder.Entity<CartItem>()
				.HasIndex(ci => new { ci.CustomerId, ci.ProductId })
				.IsUnique()
				.HasName("IX_CartItem_CustomerProduct");

			// ========== DECIMAL PRECISION ==========

			// Product prices
			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasPrecision(10, 2);

			modelBuilder.Entity<Product>()
				.Property(p => p.SalePrice)
				.HasPrecision(10, 2);

			// Order amounts
			modelBuilder.Entity<Order>()
				.Property(o => o.TotalAmount)
				.HasPrecision(15, 2);

			modelBuilder.Entity<OrderDetail>()
				.Property(od => od.UnitPrice)
				.HasPrecision(15, 2);

			modelBuilder.Entity<OrderDetail>()
				.Property(od => od.Subtotal)
				.HasPrecision(15, 2);

			// Payment amounts
			modelBuilder.Entity<PaymentTransaction>()
				.Property(pt => pt.Amount)
				.HasPrecision(10, 2);

			// Coupon discounts
			modelBuilder.Entity<Coupon>()
				.Property(c => c.DiscountValue)
				.HasPrecision(10, 2);

			modelBuilder.Entity<Coupon>()
				.Property(c => c.MinOrderAmount)
				.HasPrecision(10, 2);

			modelBuilder.Entity<OrderCoupon>()
				.Property(oc => oc.DiscountAmount)
				.HasPrecision(15, 2);

            // ========== STRING LENGTHS ==========
            // Coupon ImageUrl
            modelBuilder.Entity<Coupon>()
                .Property(c => c.ImageUrl)
                .HasMaxLength(500);
            
        }

		#endregion

		#region Static Factory Method

		/// <summary>
		/// T?o DbContext instance m?i
		/// </summary>
		public static SchoolSuppliesContext Create()
		{
			return new SchoolSuppliesContext();
		}

		#endregion
	}
}
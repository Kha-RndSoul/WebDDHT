using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebDDHT.Data;


namespace WebDDHT.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Trang chủ - Hiển thị sản phẩm nổi bật
        /// GET: /
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                ViewBag.Title = "Trang chủ - School Supplies Shop";

                // Get featured products
                ViewBag.NewProducts = ProductService.GetNewestProducts(8);
                ViewBag.BestSellers = ProductService.GetBestSellers(8);
                ViewBag.OnSaleProducts = ProductService.GetOnSaleProducts().Take(8).ToList();

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Trang giới thiệu
        /// GET: /Home/About
        /// </summary>
        public ActionResult About()
        {
            ViewBag.Title = "Giới thiệu";
            ViewBag.Message = "Về chúng tôi - School Supplies Shop";

            return View();
        }

        /// <summary>
        /// Trang liên hệ
        /// GET: /Home/Contact
        /// </summary>
        public ActionResult Contact()
        {
            ViewBag.Title = "Liên hệ";
            ViewBag.Message = "Thông tin liên hệ";

            return View();
        }

        /// <summary>
        /// Trang lỗi
        /// GET:  /Home/Error
        /// </summary>
        public ActionResult Error()
        {
            ViewBag.Title = "Lỗi";
            return View();
        }

        /// <summary>
        /// Trang 404 - Not Found
        /// GET: /Home/NotFound
        /// </summary>
        public ActionResult NotFound()
        {
            ViewBag.Title = "Không tìm thấy trang";
            Response.StatusCode = 404;
            return View();
        }

        // ========== TEST ACTIONS (GIỮ LẠI CHO DEBUG) ==========

        /// <summary>
        /// Test 1: Kiểm tra database connection
        /// URL: /Home/TestConnection
        /// </summary>
        public ActionResult TestConnection()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    bool canConnect = db.Database.Exists();

                    if (canConnect)
                    {
                        string dbName = db.Database.Connection.Database;
                        return Content($"✅ SUCCESS: Connected to database '{dbName}'");
                    }
                    else
                    {
                        return Content("❌ FAILED: Database does not exist!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content($"❌ ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Test 2: Query Coupons với ImageUrl
        /// URL: /Home/TestCoupons
        /// </summary>
        public ActionResult TestCoupons()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    var coupons = db.Coupons
                        .OrderBy(c => c.CouponId)
                        .Take(5)
                        .ToList();

                    if (!coupons.Any())
                    {
                        return Content("⚠️ WARNING: No coupons found.  Run seed data script first.");
                    }

                    string response = $"✅ Found {coupons.Count} coupons:\n\n";
                    foreach (var coupon in coupons)
                    {
                        response += $"ID: {coupon.CouponId}\n";
                        response += $"Code: {coupon.CouponCode}\n";
                        response += $"ImageUrl: {coupon.ImageUrl ?? "(null)"}\n";
                        response += $"Discount: {coupon.DiscountValue} ({coupon.DiscountType})\n";
                        response += $"Active: {coupon.IsActive}\n";
                        response += "---\n";
                    }

                    return Content(response);
                }
            }
            catch (Exception ex)
            {
                return Content($"❌ ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Test 3: Query với Navigation Properties
        /// URL: /Home/TestNavigationProperties
        /// </summary>
        public ActionResult TestNavigationProperties()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    // Test Product -> Category, Brand, Images
                    var product = db.Products
                        .Include(p => p.Category)
                        .Include(p => p.Brand)
                        .Include(p => p.ProductImages)
                        .FirstOrDefault();

                    if (product == null)
                    {
                        return Content("⚠️ No products found");
                    }

                    var response = "✅ Navigation Property Test:\n\n";
                    response += $"Product: {product.ProductName}\n";
                    response += $"Category:  {product.Category?.CategoryName ?? "null"}\n";
                    response += $"Brand: {product.Brand?.BrandName ?? "null"}\n";
                    response += $"Images Count: {product.ProductImages?.Count ?? 0}\n";

                    return Content(response);
                }
            }
            catch (Exception ex)
            {
                return Content($"❌ ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Test 4: Đếm số records trong 15 tables
        /// URL: /Home/TestAllDbSets
        /// </summary>
        public ActionResult TestAllDbSets()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    string response = "✅ DbSets Count:\n\n";

                    response += $"Categories: {db.Categories.Count()}\n";
                    response += $"Brands: {db.Brands.Count()}\n";
                    response += $"Products: {db.Products.Count()}\n";
                    response += $"ProductImages: {db.ProductImages.Count()}\n";
                    response += $"Customers: {db.Customers.Count()}\n";
                    response += $"Admins: {db.Admins.Count()}\n";
                    response += $"Orders: {db.Orders.Count()}\n";
                    response += $"OrderDetails: {db.OrderDetails.Count()}\n";
                    response += $"CartItems: {db.CartItems.Count()}\n";
                    response += $"Coupons: {db.Coupons.Count()}\n";
                    response += $"OrderCoupons: {db.OrderCoupons.Count()}\n";
                    response += $"ProductReviews: {db.ProductReviews.Count()}\n";
                    response += $"PaymentTransactions: {db.PaymentTransactions.Count()}\n";
                    response += $"Banners: {db.Banners.Count()}\n";
                    response += $"ContactMessages: {db.ContactMessages.Count()}\n";

                    return Content(response);
                }
            }
            catch (Exception ex)
            {
                return Content($"❌ ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Test 5: Test Services
        /// URL: /Home/TestServices
        /// </summary>
        public ActionResult TestServices()
        {
            try
            {
                string response = "✅ Services Test:\n\n";

                // Test ProductService
                var products = ProductService.GetAllProducts();
                response += $"ProductService. GetAllProducts(): {products.Count()} products\n";

                var bestSellers = ProductService.GetBestSellers(5);
                response += $"ProductService.GetBestSellers(5): {bestSellers.Count()} products\n";

                // Test CategoryService
                var categories = CategoryService.GetActiveCategories();
                response += $"CategoryService.GetActiveCategories(): {categories.Count()} categories\n";

                return Content(response);
            }
            catch (Exception ex)
            {
                return Content($"❌ ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }
    }
}
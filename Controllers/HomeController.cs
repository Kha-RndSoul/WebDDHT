using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_CK.Models;
using WebDDHT.Data;
using System.Linq;

namespace Project_CK.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // ⭐ THÊM TEST ACTIONS NÀY

        /// <summary>
        /// Test 1: Kiểm tra database connection
        /// URL: /Home/TestConnection
        /// </summary>
        public IActionResult TestConnection()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    // Test connection bằng cách query database
                    bool canConnect = db.Database.Exists();

                    if (canConnect)
                    {
                        return Content($"✅ SUCCESS: Connected to database '{db.Database.Connection.Database}'");
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
        public IActionResult TestCoupons()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    // Query top 5 coupons
                    var coupons = db.Coupons
                        .OrderBy(c => c.CouponId)
                        .Take(5)
                        .ToList();

                    if (!coupons.Any())
                    {
                        return Content("⚠️ WARNING: No coupons found in database.  Run seed data script first.");
                    }

                    // Build response
                    var response = $"✅ Found {coupons.Count} coupons:\n\n";
                    foreach (var coupon in coupons)
                    {
                        response += $"ID: {coupon.CouponId}\n";
                        response += $"Code: {coupon.CouponCode}\n";
                        response += $"ImageUrl: {coupon.ImageUrl ?? "(null)"}\n";  // ⭐ TEST IMAGEURL
                        response += $"Discount:  {coupon.DiscountValue} ({coupon.DiscountType})\n";
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
        public IActionResult TestNavigationProperties()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    // Test Coupon -> OrderCoupons navigation
                    var coupon = db.Coupons
                        .Include(c => c.OrderCoupons)  // Eager loading
                        .FirstOrDefault();

                    if (coupon == null)
                    {
                        return Content("⚠️ No coupons found");
                    }

                    var response = $"✅ Coupon Navigation Property Test:\n\n";
                    response += $"Coupon:  {coupon.CouponCode}\n";
                    response += $"ImageUrl: {coupon.ImageUrl ?? "(null)"}\n";
                    response += $"OrderCoupons Count: {coupon.OrderCoupons?.Count ?? 0}\n\n";

                    // Test Product -> Category navigation
                    var product = db.Products
                        .Include(p => p.Category)
                        .Include(p => p.Brand)
                        .FirstOrDefault();

                    if (product != null)
                    {
                        response += $"Product Navigation Property Test:\n";
                        response += $"Product: {product.ProductName}\n";
                        response += $"Category: {product.Category?.CategoryName ?? "(null)"}\n";
                        response += $"Brand: {product.Brand?.BrandName ?? "(null)"}\n";
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
        /// Test 4: Test tất cả DbSets
        /// URL: /Home/TestAllDbSets
        /// </summary>
        public IActionResult TestAllDbSets()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    var response = "✅ DbSets Test:\n\n";

                    response += $"Categories:  {db.Categories.Count()}\n";
                    response += $"Brands: {db.Brands.Count()}\n";
                    response += $"Products:  {db.Products.Count()}\n";
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
                    response += $"ContactMessages:  {db.ContactMessages.Count()}\n";

                    return Content(response);
                }
            }
            catch (Exception ex)
            {
                return Content($"❌ ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }
    }
}
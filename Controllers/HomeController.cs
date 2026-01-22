using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;              
using System.Data.Entity;         
using WebDDHT.Data;
using WebDDHT.Models;

namespace WebDDHT.Controllers {   
    public class HomeController : Controller  
    {
        public ActionResult Index()  
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        
        /// Test 1: Kiểm tra database connection
        
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
                        return Content($" SUCCESS: Connected to database '{dbName}'");
                    }
                    else
                    {
                        return Content(" FAILED: Database does not exist!");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content($" ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }

       
        /// Test 2: Query Coupons với ImageUrl
       
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
                        return Content(" WARNING: No coupons found.  Run seed data script first.");
                    }

                    string response = $" Found {coupons.Count} coupons:\n\n";
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
                return Content($" ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }

    
        /// Test 3: Navigation Properties
       
        public ActionResult TestNavigationProperties()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    var coupon = db.Coupons
                        .Include(c => c.OrderCoupons)
                        .FirstOrDefault();

                    if (coupon == null)
                    {
                        return Content(" No coupons found");
                    }

                    string response = " Coupon Navigation Property Test:\n\n";
                    response += $"Coupon:  {coupon.CouponCode}\n";
                    response += $"ImageUrl: {coupon.ImageUrl ?? "(null)"}\n";
                    response += $"OrderCoupons Count: {coupon.OrderCoupons?.Count ?? 0}\n\n";

                    var product = db.Products
                        .Include(p => p.Category)
                        .Include(p => p.Brand)
                        .FirstOrDefault();

                    if (product != null)
                    {
                        response += "Product Navigation Property Test:\n";
                        response += $"Product: {product.ProductName}\n";
                        response += $"Category: {product.Category?.CategoryName ?? "(null)"}\n";
                        response += $"Brand: {product.Brand?.BrandName ?? "(null)"}\n";
                    }

                    return Content(response);
                }
            }
            catch (Exception ex)
            {
                return Content($" ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }

        /// Test 4: All DbSets
       
        public ActionResult TestAllDbSets()
        {
            try
            {
                using (var db = new SchoolSuppliesContext())
                {
                    string response = " DbSets Test:\n\n";

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
                    response += $"ContactMessages: {db.ContactMessages.Count()}\n";

                    return Content(response);
                }
            }
            catch (Exception ex)
            {
                return Content($" ERROR: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }
    }
}
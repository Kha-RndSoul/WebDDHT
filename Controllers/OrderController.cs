using System;
using System.Linq;
using System.Web.Mvc;
using WebDDHT.Filters;
using WebDDHT.Helpers;
using WebDDHT.ViewModels;

namespace WebDDHT.Controllers
{
    [AuthorizeCustomer]
    public class OrderController : BaseController
    {
        /// <summary>
        /// Trang checkout
        /// GET: /Order/Checkout
        /// </summary>
        public ActionResult Checkout()
        {
            try
            {
                ViewBag.Title = "Thanh toán";

                int customerId = SessionHelper.GetCustomerId().Value;
                var customer = SessionHelper.GetCustomer();

                // Get cart items
                var cartItems = CartService.GetCartItems(customerId).ToList();

                if (!cartItems.Any())
                {
                    TempData["ErrorMessage"] = "Giỏ hàng trống. Vui lòng thêm sản phẩm trước khi thanh toán.";
                    return RedirectToAction("Index", "Cart");
                }

                // Validate cart
                string validationError;
                if (!CartService.ValidateCart(customerId, out validationError))
                {
                    TempData["ErrorMessage"] = validationError;
                    return RedirectToAction("Index", "Cart");
                }

                // Calculate totals
                decimal subtotal = CartService.GetCartTotal(customerId);

                var viewModel = new CheckoutViewModel
                {
                    CustomerId = customerId,
                    CustomerEmail = customer.Email,
                    ShippingName = customer.FullName,
                    ShippingPhone = customer.Phone,
                    ShippingAddress = customer.Address,
                    CartItems = cartItems,
                    Subtotal = subtotal,
                    Total = subtotal,
                    PaymentMethod = "COD"
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Xử lý đặt hàng
        /// POST: /Order/Checkout
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Reload cart data
                    int customerId = SessionHelper.GetCustomerId().Value;
                    model.CartItems = CartService.GetCartItems(customerId).ToList();
                    model.Subtotal = CartService.GetCartTotal(customerId);
                    model.Total = model.Subtotal;

                    return View(model);
                }

                // Create order
                int orderId;
                string errorMessage;
                bool success = OrderService.CreateOrderFromCart(
                    model.CustomerId,
                    model.ShippingName,
                    model.ShippingPhone,
                    model.ShippingAddress,
                    model.PaymentMethod,
                    model.CouponCode,
                    model.Note,
                    out orderId,
                    out errorMessage
                );

                if (success)
                {
                    // Clear cart count in session
                    SessionHelper.SetCartItemCount(0);

                    // Redirect to confirmation page
                    TempData["SuccessMessage"] = "Đặt hàng thành công! ";
                    return RedirectToAction("Confirm", new { id = orderId });
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);

                    // Reload cart data
                    model.CartItems = CartService.GetCartItems(model.CustomerId).ToList();
                    model.Subtotal = CartService.GetCartTotal(model.CustomerId);
                    model.Total = model.Subtotal;

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi:  {ex.Message}");

                // Reload cart data
                int customerId = SessionHelper.GetCustomerId().Value;
                model.CartItems = CartService.GetCartItems(customerId).ToList();
                model.Subtotal = CartService.GetCartTotal(customerId);
                model.Total = model.Subtotal;

                return View(model);
            }
        }

        /// <summary>
        /// Trang xác nhận đơn hàng
        /// GET: /Order/Confirm/5
        /// </summary>
        public ActionResult Confirm(int id)
        {
            try
            {
                ViewBag.Title = "Xác nhận đơn hàng";

                int customerId = SessionHelper.GetCustomerId().Value;

                var order = OrderService.GetOrderWithDetails(id);

                if (order == null || order.CustomerId != customerId)
                {
                    ViewBag.ErrorMessage = "Đơn hàng không tồn tại.";
                    return View("NotFound");
                }

                var viewModel = new OrderConfirmViewModel
                {
                    Order = order,
                    Success = true,
                    Message = "Đặt hàng thành công!  Cảm ơn bạn đã mua hàng."
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Danh sách đơn hàng của tôi
        /// GET: /Order/MyOrders
        /// </summary>
        public ActionResult MyOrders()
        {
            try
            {
                ViewBag.Title = "Đơn hàng của tôi";

                int customerId = SessionHelper.GetCustomerId().Value;
                var customer = SessionHelper.GetCustomer();

                var orders = OrderService.GetCustomerOrders(customerId);

                var viewModel = new MyOrdersViewModel
                {
                    Customer = customer,
                    Orders = orders
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Chi tiết đơn hàng
        /// GET: /Order/Details/5
        /// </summary>
        public ActionResult Details(int id)
        {
            try
            {
                ViewBag.Title = "Chi tiết đơn hàng";

                int customerId = SessionHelper.GetCustomerId().Value;

                var order = OrderService.GetOrderWithDetails(id);

                if (order == null || order.CustomerId != customerId)
                {
                    ViewBag.ErrorMessage = "Đơn hàng không tồn tại.";
                    return View("NotFound");
                }

                var viewModel = new OrderConfirmViewModel
                {
                    Order = order,
                    Success = true
                };

                return View("Confirm", viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Hủy đơn hàng
        /// POST: /Order/Cancel/5
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            try
            {
                int customerId = SessionHelper.GetCustomerId().Value;

                var order = OrderService.GetOrderById(id);

                if (order == null || order.CustomerId != customerId)
                {
                    TempData["ErrorMessage"] = "Đơn hàng không tồn tại.";
                    return RedirectToAction("MyOrders");
                }

                string errorMessage;
                bool success = OrderService.CancelOrder(id, out errorMessage);

                if (success)
                {
                    TempData["SuccessMessage"] = "Đã hủy đơn hàng thành công.";
                }
                else
                {
                    TempData["ErrorMessage"] = errorMessage;
                }

                return RedirectToAction("MyOrders");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi: {ex.Message}";
                return RedirectToAction("MyOrders");
            }
        }

        /// <summary>
        /// Apply coupon (AJAX)
        /// POST: /Order/ApplyCoupon
        /// </summary>
        [HttpPost]
        public JsonResult ApplyCoupon(string couponCode)
        {
            try
            {
                int customerId = SessionHelper.GetCustomerId().Value;

                decimal discount;
                decimal total = OrderService.CalculateOrderTotal(customerId, couponCode, out discount);
                decimal subtotal = CartService.GetCartTotal(customerId);

                if (discount > 0)
                {
                    return Json(new
                    {
                        success = true,
                        message = $"Đã áp dụng mã giảm giá. Giảm {discount: N0} VNĐ",
                        discount = discount,
                        subtotal = subtotal,
                        total = total
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Mã giảm giá không hợp lệ hoặc không áp dụng được cho đơn hàng này."
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Lỗi: {ex.Message}"
                });
            }
        }
    }
}
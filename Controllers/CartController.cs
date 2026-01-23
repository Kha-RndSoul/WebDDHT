using System;
using System.Linq;
using System.Web.Mvc;
using WebDDHT.Filters;
using WebDDHT.Helpers;
using WebDDHT.ViewModels;

namespace WebDDHT.Controllers
{
    [AuthorizeCustomer]
    public class CartController : BaseController
    {
        /// <summary>
        /// Hiển thị giỏ hàng
        /// GET: /Cart
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                ViewBag.Title = "Giỏ hàng";

                int customerId = SessionHelper.GetCustomerId().Value;
                var cartItems = CartService.GetCartItems(customerId).ToList();
                decimal subtotal = CartService.GetCartTotal(customerId);

                // Validate cart
                string validationMessage;
                bool isValid = CartService.ValidateCart(customerId, out validationMessage);

                var viewModel = new CartViewModel
                {
                    CartItems = cartItems,
                    Subtotal = subtotal,
                    Total = subtotal,
                    IsValid = isValid,
                    ValidationMessage = validationMessage
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
        /// Thêm sản phẩm vào giỏ hàng
        /// POST: /Cart/Add
        /// </summary>
        [HttpPost]
        public JsonResult Add(int productId, int quantity = 1)
        {
            try
            {
                if (!SessionHelper.IsLoggedIn())
                {
                    return Json(new
                    {
                        success = false,
                        message = "Vui lòng đăng nhập để thêm vào giỏ hàng",
                        redirectUrl = Url.Action("Login", "Account", new { returnUrl = Request.UrlReferrer?.PathAndQuery })
                    });
                }

                int customerId = SessionHelper.GetCustomerId().Value;

                string errorMessage;
                bool success = CartService.AddToCart(customerId, productId, quantity, out errorMessage);

                if (success)
                {
                    // Update cart count in session
                    int cartCount = CartService.GetCartItemCount(customerId);
                    SessionHelper.SetCartItemCount(cartCount);

                    return Json(new
                    {
                        success = true,
                        message = "Đã thêm vào giỏ hàng",
                        cartCount = cartCount
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = errorMessage
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

        /// <summary>
        /// Cập nhật số lượng
        /// POST: /Cart/UpdateQuantity
        /// </summary>
        [HttpPost]
        public JsonResult UpdateQuantity(int productId, int quantity)
        {
            try
            {
                int customerId = SessionHelper.GetCustomerId().Value;

                string errorMessage;
                bool success = CartService.UpdateCartItemQuantity(customerId, productId, quantity, out errorMessage);

                if (success)
                {
                    decimal newTotal = CartService.GetCartTotal(customerId);
                    var cartItem = CartService.GetCartItems(customerId)
                        .FirstOrDefault(c => c.ProductId == productId);

                    decimal itemTotal = 0;
                    if (cartItem != null)
                    {
                        itemTotal = cartItem.Product.GetCurrentPrice() * quantity;
                    }

                    return Json(new
                    {
                        success = true,
                        message = "Đã cập nhật số lượng",
                        itemTotal = itemTotal,
                        cartTotal = newTotal
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = errorMessage
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

        /// <summary>
        /// Xóa sản phẩm khỏi giỏ hàng
        /// POST: /Cart/Remove
        /// </summary>
        [HttpPost]
        public JsonResult Remove(int productId)
        {
            try
            {
                int customerId = SessionHelper.GetCustomerId().Value;

                string errorMessage;
                bool success = CartService.RemoveFromCart(customerId, productId, out errorMessage);

                if (success)
                {
                    int cartCount = CartService.GetCartItemCount(customerId);
                    decimal cartTotal = CartService.GetCartTotal(customerId);
                    SessionHelper.SetCartItemCount(cartCount);

                    return Json(new
                    {
                        success = true,
                        message = "Đã xóa khỏi giỏ hàng",
                        cartCount = cartCount,
                        cartTotal = cartTotal
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = errorMessage
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

        /// <summary>
        /// Xóa tất cả sản phẩm trong giỏ hàng
        /// POST: /Cart/Clear
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear()
        {
            try
            {
                int customerId = SessionHelper.GetCustomerId().Value;

                string errorMessage;
                bool success = CartService.ClearCart(customerId, out errorMessage);

                if (success)
                {
                    SessionHelper.SetCartItemCount(0);
                    TempData["SuccessMessage"] = "Đã xóa tất cả sản phẩm trong giỏ hàng.";
                }
                else
                {
                    TempData["ErrorMessage"] = errorMessage;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Lấy số lượng items trong cart (dùng cho AJAX)
        /// GET: /Cart/GetCartCount
        /// </summary>
        [AllowAnonymous]
        public JsonResult GetCartCount()
        {
            try
            {
                if (!SessionHelper.IsLoggedIn())
                {
                    return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
                }

                int customerId = SessionHelper.GetCustomerId().Value;
                int count = CartService.GetCartItemCount(customerId);

                return Json(new { count = count }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
using System;
using System.Web.Mvc;
using System.Web.Security;
using WebDDHT.Helpers;
using WebDDHT.Filters;
using WebDDHT.ViewModels;

namespace WebDDHT.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// Trang đăng nhập
        /// GET: /Account/Login
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // Nếu đã login, redirect về trang chủ
            if (SessionHelper.IsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Đăng nhập";
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        /// <summary>
        /// Xử lý đăng nhập
        /// POST: /Account/Login
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string errorMessage;
                var customer = AuthService.Login(model.Email, model.Password, out errorMessage);

                if (customer != null)
                {
                    // Save customer to session
                    SessionHelper.SetCustomer(customer);

                    // Load cart item count
                    int cartCount = CartService.GetCartItemCount(customer.Id);
                    SessionHelper.SetCartItemCount(cartCount);

                    // Set authentication cookie if RememberMe
                    if (model.RememberMe)
                    {
                        FormsAuthentication.SetAuthCookie(customer.Email, true);
                    }

                    // Redirect to return URL or home page
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    TempData["SuccessMessage"] = $"Chào mừng {customer.FullName}! ";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                return View(model);
            }
        }

        /// <summary>
        /// Trang đăng ký
        /// GET: /Account/Register
        /// </summary>
        [AllowAnonymous]
        public ActionResult Register()
        {
            // Nếu đã login, redirect về trang chủ
            if (SessionHelper.IsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Đăng ký";

            return View();
        }

        /// <summary>
        /// Xử lý đăng ký
        /// POST: /Account/Register
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                string errorMessage;
                bool success = AuthService.Register(
                    model.Email,
                    model.Password,
                    model.FullName,
                    model.Phone,
                    model.Address,
                    out errorMessage
                );

                if (success)
                {
                    TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                return View(model);
            }
        }

        /// <summary>
        /// Đăng xuất
        /// GET: /Account/Logout
        /// </summary>
        public ActionResult Logout()
        {
            // Clear session
            SessionHelper.ClearCustomer();

            // Clear authentication cookie
            FormsAuthentication.SignOut();

            TempData["SuccessMessage"] = "Đã đăng xuất thành công.";
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Trang thông tin tài khoản
        /// GET: /Account/Profile
        /// </summary>
        [AuthorizeCustomer]
        public ActionResult Profile()
        {
            try
            {
                ViewBag.Title = "Thông tin tài khoản";

                var customer = SessionHelper.GetCustomer();

                return View(customer);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Check email exists (AJAX) - Dùng cho validation trong form đăng ký
        /// GET: /Account/CheckEmailExists? email=...
        /// </summary>
        [AllowAnonymous]
        public JsonResult CheckEmailExists(string email)
        {
            try
            {
                bool exists = AuthService.EmailExists(email);

                return Json(new
                {
                    exists = exists
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    exists = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Access Denied page
        /// GET: /Account/AccessDenied
        /// </summary>
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            ViewBag.Title = "Truy cập bị từ chối";
            return View();
        }
    }
}
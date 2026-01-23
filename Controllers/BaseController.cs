using System.Web.Mvc;
using WebDDHT.Data;
using WebDDHT.Helpers;
using WebDDHT.Repositories;
using WebDDHT.Services.Implementations;
using WebDDHT.Services.Interfaces;

namespace WebDDHT.Controllers
{
    /// <summary>
    /// Base Controller - Shared logic cho tất cả controllers
    /// </summary>
    public class BaseController : Controller
    {
        protected SchoolSuppliesContext Context;
        protected IUnitOfWork UnitOfWork;

        // Services
        protected IProductService ProductService;
        protected ICategoryService CategoryService;
        protected ICartService CartService;
        protected IOrderService OrderService;
        protected IAuthService AuthService;

        public BaseController()
        {
            // Initialize context and unit of work
            Context = new SchoolSuppliesContext();
            UnitOfWork = new UnitOfWork(Context);

            // Initialize services
            ProductService = new ProductService(UnitOfWork);
            CategoryService = new CategoryService(UnitOfWork);
            CartService = new CartService(UnitOfWork);
            OrderService = new OrderService(UnitOfWork);
            AuthService = new AuthService(UnitOfWork);
        }

        /// <summary>
        /// OnActionExecuting - Chạy trước mỗi action
        /// Load data chung cho layout (cart count, categories menu, customer info)
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Load cart count for layout
            if (SessionHelper.IsLoggedIn())
            {
                int customerId = SessionHelper.GetCustomerId().Value;
                int cartCount = CartService.GetCartItemCount(customerId);
                ViewBag.CartItemCount = cartCount;

                // Load customer info for layout
                ViewBag.CurrentCustomer = SessionHelper.GetCustomer();
            }
            else
            {
                ViewBag.CartItemCount = 0;
                ViewBag.CurrentCustomer = null;
            }

            // Load categories for menu
            ViewBag.MenuCategories = CategoryService.GetActiveCategories();

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Dispose - Clean up resources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork?.Dispose();
                Context?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project_CK.Models;

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

        //Xử lí đăng kí
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Project_CK.Models.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

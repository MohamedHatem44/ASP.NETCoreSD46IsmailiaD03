using System.Diagnostics;
using ASP.NETCoreD03.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreD03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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

        //// 
        //// Model Binding => query string => Bind input params
        ////
        //public IActionResult ACreate1(int id,string name)
        //{
        //    // Create New Employee
        //}

        //// After Submit
        //// Model Binding => query string => Bind input params
        //// Before Action
        //public IActionResult ACreate1(Employee employee)
        //{
        //    // Create New Employee
        //}
    }
}

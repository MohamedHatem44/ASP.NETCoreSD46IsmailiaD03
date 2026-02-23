using ASP.NETCoreD03.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreD03.Controllers
{
    public class DepartmentController : Controller
    {
        /*------------------------------------------------------------------*/
        // Context => DB => Data Access
        private readonly AppDbContext db = new AppDbContext();
        /*------------------------------------------------------------------*/
        public IActionResult Index()
        {
            var departments = db.Departments.ToList();
            return View(departments);
        }
        /*------------------------------------------------------------------*/
    }
}

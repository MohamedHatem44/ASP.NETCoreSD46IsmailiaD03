using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreD03.Controllers
{
    public class DepartmentV01Controller : Controller
    {
        // Context => DB => Data Access
        // GET: DepartmentV01Controller
        public ActionResult Index()
        {
            return View();
        }

        // GET: DepartmentV01Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentV01Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentV01Controller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            //var name = collection["Name"];
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentV01Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentV01Controller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentV01Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentV01Controller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

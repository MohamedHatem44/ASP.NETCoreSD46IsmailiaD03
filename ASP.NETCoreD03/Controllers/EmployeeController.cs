using ASP.NETCoreD03.Data.Context;
using ASP.NETCoreD03.Models;
using ASP.NETCoreD03.ViewModels;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreD03.Controllers
{
    public class EmployeeController : Controller
    {
        /*------------------------------------------------------------------*/
        // Context => DB => Data Access
        private readonly AppDbContext db = new AppDbContext();
        /*------------------------------------------------------------------*/
        // V01
        // Index => List All => Main Action => Landing Page
        [HttpGet]
        public IActionResult IndexV01()
        {
            //var employees = db.Employees.ToList();
            var employees = db.Employees.Include(e => e.Department);
            return View(employees);
        }
        /*------------------------------------------------------------------*/
        // V02
        // Index => List All => Main Action => Landing Page
        [HttpGet]
        public IActionResult IndexV02()
        {
            // Get All Employees
            // Map From Domain Model To VM
            var employeesReadVM = db.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeReadVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    Department = e.Department!.Name
                }).ToList();

            return View(employeesReadVM);
        }
        /*------------------------------------------------------------------*/
        // V01
        // View Details
        [HttpGet]
        public IActionResult DetailsV01(int id)
        {
            var employee = db.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return RedirectToAction("IndexV01");
            }
            return View(employee);
        }
        /*------------------------------------------------------------------*/
        // V02
        // View Details
        [HttpGet]
        public IActionResult DetailsV02(int id)
        {
            var employee = db.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return RedirectToAction("IndexV01");
            }

            // Map FromDomain Model To View Model
            var employeeReadVM = new EmployeeReadVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Department = employee.Department!.Name
            };

            return View(employeeReadVM);
        }
        /*------------------------------------------------------------------*/
        // V01
        // Create Employee
        // Get => Show Form
        [HttpGet]
        public IActionResult CreateV01()
        {
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Name");
            return View();
        }
        /*------------------------------------------------------------------*/
        // V01
        [HttpPost]
        public IActionResult CreateV01(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("IndexV01");
        }
        /*------------------------------------------------------------------*/
        // V02
        // Create Employee
        // Get => Show Form
        // Get Departments => For DropdownList
        [HttpGet]
        public IActionResult CreateV02()
        {
            //ViewBag.Departments = new SelectList(db.Departments, "Id", "Name");
            var employeeCreateVM = new EmployeeCreateVM
            {
                Departments = GetDepartmentsForDropDown()
            };
            return View(employeeCreateVM);
        }
        /*------------------------------------------------------------------*/
        // V02
        [HttpPost]
        public IActionResult CreateV02(EmployeeCreateVM employeeCreateVM)
        {
            // employeeCreateVM => Don't Have Id
            // Map From VM To Domain Model
            var employee = new Employee
            {
                Name = employeeCreateVM.Name,
                Age = employeeCreateVM.Age,
                Salary = employeeCreateVM.Salary,
                DepartmentId = employeeCreateVM.DepartmentId
            };

            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("IndexV02");
        }
        /*------------------------------------------------------------------*/
        // V01 
        [HttpGet]
        public IActionResult EditV01(int id)
        {
            var employee = db.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return RedirectToAction("IndexV01");
            }
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Name");
            return View(employee);
        }
        /*------------------------------------------------------------------*/
        // V01
        [HttpPost]
        public IActionResult EditV01(Employee employee)
        {
            //if (id != employee.Id)
            //{
            //    return RedirectToAction("IndexV01");
            //}
            //db.Employees.Update(employee);
            var employeeInDb = db.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (employeeInDb == null)
            {
                return RedirectToAction("IndexV01");
            }
            employeeInDb.Name = employee.Name;
            employeeInDb.Age = employee.Age;
            employeeInDb.Salary = employee.Salary;
            employeeInDb.DepartmentId = employee.DepartmentId;
            db.SaveChanges();
            return RedirectToAction("IndexV01");
        }
        /*------------------------------------------------------------------*/
        // V02
        [HttpGet]
        public IActionResult EditV02(int id)
        {
            var employee = db.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return RedirectToAction("IndexV02");
            }

            // Map Domain Model To VM
            var employeeEditVM = new EmployeeEditVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department!.Name,
                Departments = GetDepartmentsForDropDown()
            };
            return View(employeeEditVM);
        }
        /*------------------------------------------------------------------*/
        // V02
        [HttpPost]
        public IActionResult EditV02(EmployeeEditVM employeeEditVM)
        {
            //if (id != employee.Id)
            //{
            //    return RedirectToAction("IndexV01");
            //}
            //db.Employees.Update(employee);
            var employeeInDb = db.Employees.FirstOrDefault(e => e.Id == employeeEditVM.Id);
            if (employeeInDb == null)
            {
                return RedirectToAction("IndexV01");
            }

            // Map From VM To Domain Model
            employeeInDb.Name = employeeEditVM.Name;
            employeeInDb.Age = employeeEditVM.Age;
            employeeInDb.Salary = employeeEditVM.Salary;
            employeeInDb.DepartmentId = employeeEditVM.DepartmentId;
            db.SaveChanges();
            return RedirectToAction("IndexV01");
        }
        /*------------------------------------------------------------------*/
        public IActionResult Delete(int id)
        {
            var employee = db.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return RedirectToAction("IndexV01");
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("IndexV01");
        }
        /*------------------------------------------------------------------*/
        // Helper Method
        // DRY => Reusable Code => Don't Repeat Yourself
        private List<SelectListItem> GetDepartmentsForDropDown()
        {
            return db.Departments
             .Select(d => new SelectListItem
             {
                 Value = d.Id.ToString(),
                 Text = d.Name
             }).ToList();
        }
        /*------------------------------------------------------------------*/
    }
}

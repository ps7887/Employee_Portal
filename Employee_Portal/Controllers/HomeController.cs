using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Employee_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmployeeDbContext _context;

        public HomeController(ILogger<HomeController> logger ,EmployeeDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region allEmployee
        public IActionResult GetAllEmployee()
        {
            List<Employee> employee = _context.Employees.ToList();

            return View(employee);
        }
        #endregion
        #region Create_Employee
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            TempData["Success"] = "new employee Add Successfully";
            return RedirectToAction("GetAllEmployee", "Home");
        }
        #endregion
        #region Detail_Id_Name
        public IActionResult Details( int? id, string name)
        {
            var employees = new List<Employee>();
            if (id != null)
            {
                Employee EmployeeDet = _context.Employees.FirstOrDefault(a => a.Id == id);

                if (EmployeeDet != null)
                {
                    return View(EmployeeDet);

                }
            }
             if (!string.IsNullOrEmpty(name))
            {
             //  List <Employee> Employees = _context.Employees.ToList();

                employees = _context.Employees
                    .Where(a => a.Name.Contains(name))
                    .ToList();

                if (employees != null && employees.Any())
                {
                    return View("EmployeeList",employees);
                }

            }

                TempData["msg"] = "enter the valid Employee Name Or Id";
            return RedirectToAction("GetAllEmployee");
        }
        #endregion
        #region Update_Employee
        public IActionResult Edit(int id )
        { 
            Employee employeeinfo=_context.Employees.Where(x=>x.Id.Equals(id)).FirstOrDefault();
            if (employeeinfo != null)
            {
                return View(employeeinfo);
            }
            else
            {
                TempData["msg"] = "enter valid Employee Id";
                return RedirectToAction("GetAllEmployee");
            }
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
                _context.Employees.Update(employee);
                _context.SaveChanges(); 
            TempData["SuccessS"] = " employee Update Successfully";
            return RedirectToAction("GetAllEmployee", "Home");
        }
        [HttpGet]
        public IActionResult MultipleEdit(int[] id)
        {
            if (id == null || id.Length == 0)
            {
                TempData["msg"] = "Please select employees";
                return RedirectToAction("GetAllEmployee");
            }

            var employees = _context.Employees.Where(e => id.Contains(e.Id)).ToList();
            return View(employees);
        }
        [HttpPost]
        public IActionResult MultipleEdit(List<Employee> employees)
        {
            foreach (var emp in employees)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.Id == emp.Id);

                if (employee != null)
                {
                    employee.Salary = emp.Salary;
                }
            }
            _context.SaveChanges();
            TempData["msg"] = "Salary updated successfully";
            return RedirectToAction("GetAllEmployee");

        }
        #endregion
        #region Delete
        public IActionResult Delete(int id)
        {
            Employee employeedelete = _context.Employees.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (employeedelete != null)
            {
                return View(employeedelete);
            }
            else
            {
                TempData["msg"] = "enter valid Employee Id";
                return RedirectToAction("GetAllEmployee");
            }

        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            TempData["SuccessS"] = "Student Delete Successfully";
            return RedirectToAction("GetAllEmployee");

        }

        #endregion
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

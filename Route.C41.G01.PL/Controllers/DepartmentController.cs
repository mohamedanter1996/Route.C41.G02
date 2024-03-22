using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;

namespace Route.C41.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepository _departmentrepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentrepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var department = _departmentrepo.GetAll();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var Count= _departmentrepo.Add(department);

                if (Count > 0) {
                    return RedirectToAction("Index");
                }
            }

            return View(department);
        }
    }
}

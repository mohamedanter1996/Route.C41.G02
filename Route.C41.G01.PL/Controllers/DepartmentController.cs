using Microsoft.AspNetCore.Mvc;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;

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
            var department=_departmentrepo.GetAll();    
            return View(department);
        }
    }
}

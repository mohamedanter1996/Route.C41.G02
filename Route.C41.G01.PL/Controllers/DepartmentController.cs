using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Route.C41.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private IDepartmentRepository _departmentrepo;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUnitOfWork unitOfWork,/*IDepartmentRepository departmentRepo,*/ IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            //_departmentrepo = departmentRepo;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var department = await _unitOfWork.Repository<Department>().GetAllAsync();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                 _unitOfWork.Repository<Department>().Add(department);
                var Count =await _unitOfWork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();//400
            }

            var department =await _unitOfWork.Repository<Department>().GetAsync(id.Value);

            if (department == null)
            {
                return NotFound();//404
            }

            return View(ViewName, department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id.Value, "Edit");
            ///if (!id.HasValue)
            ///{
            ///    return BadRequest();
            ///}
            ///
            ///var department = _departmentrepo.Get(id.Value);
            ///if (department == null)
            ///{
            ///    return NotFound();
            ///}
            ///return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            try
            {
                _unitOfWork.Repository<Department>().Update(department);
               await _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department");
                }

                return View(department);

            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Department department)
        {
            try
            {
                _unitOfWork.Repository<Department>().Delete(department);
              await  _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message

                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Department");
                }

                return View(department);


            }
        }
    }
}

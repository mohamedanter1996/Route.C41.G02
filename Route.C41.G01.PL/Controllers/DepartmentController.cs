using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.BLL.Repositories;
using Route.C41.G02.DAL.Models;
using System;

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
        public IActionResult Index()
        {
            var department = _unitOfWork.Repository<Department>().GetAll();
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
                 _unitOfWork.Repository<Department>().Add(department);
                var Count = _unitOfWork.Complete();
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();//400
            }

            var department = _unitOfWork.Repository<Department>().Get(id.Value);

            if (department == null)
            {
                return NotFound();//404
            }

            return View(ViewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id.Value, "Edit");
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
        public IActionResult Edit([FromRoute] int id, Department department)
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
                _unitOfWork.Complete();
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
        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.Repository<Department>().Delete(department);
                _unitOfWork.Complete();

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

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
using System;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment env)
        {
            _employeeRepository = employeeRepository;
            _env = env;
        }
        public IActionResult Index()
        {
            var employee = _employeeRepository.GetAll();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var Count = _employeeRepository.Add(employee);

                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();//400
            }

            var employee = _employeeRepository.Get(id.Value);

            if (employee == null)
            {
                return NotFound();//404
            }

            return View(ViewName, employee);
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
            ///var employee = _employeeRepository.Get(id.Value);
            ///if (employee == null)
            ///{
            ///    return NotFound();
            ///}
            ///return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                _employeeRepository.Update(employee);

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
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");
                }

                return View(employee);

            }

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepository.Delete(employee);

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
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Employee");
                }

                return View(employee);


            }
        }

    }
}

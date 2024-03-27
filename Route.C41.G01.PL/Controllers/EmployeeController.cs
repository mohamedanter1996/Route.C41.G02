﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
using System;
using System.Linq;
using System.Security.Policy;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment env /*,IDepartmentRepository departmentRepository*/)
        {
            _employeeRepository = employeeRepository;
            _env = env;
            //_departmentRepository = departmentRepository;
        }
        public IActionResult Index(string SearchInput)
        {
            /// ViewData["Message"] = "Hello ViewData";
            ///
            /// ViewBag.Message = "Hello ViewBag";
            ///
            /// var employee = _employeeRepository.GetAll();
            /// return View(employee);
            var employee = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(SearchInput))
                employee = _employeeRepository.GetAll();
            else
                employee = _employeeRepository.SearchByName(SearchInput.ToLower());

            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            //ViewBag.Departments = _departmentRepo.GetAll();
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
                    TempData["Message"] = "Department is Created Successfully";
                    return RedirectToAction("Index");
                }else
                {
                    TempData["Message"] = "An Error Has Occured, Department Not Created: (";
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

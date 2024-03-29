using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.G02.BLL.Interfaces;
using Route.C41.G02.DAL.Models;
using System;
using System.Linq;
using AutoMapper;
using System.Security.Policy;
using System.Collections.Generic;
using Route.C41.G02.PL.ViewModels;

namespace Route.C41.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment env/*, IDepartmentRepository departmentRepository*/)
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
            {
                employee = _employeeRepository.SearchByName(SearchInput.ToLower());
            }
                

            // return View(employee);
            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);


            return View(mappedEmp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"]=_departmentRepository.GetAll();
            return View();
        }


        [HttpPost]

        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                //var Count = _employeeRepository.Add(employee);

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                var Count = _employeeRepository.Add(mappedEmp);
                if (Count > 0)
                {
                    TempData["Message"] = "Department is Created Successfully";
                    return RedirectToAction("Index");
                }else
                {
                    TempData["Message"] = "An Error Has Occured, Department Not Created: (";
                }
            }

            return View(employeeVM);
        }

        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();//400
            }

            var employee = _employeeRepository.Get(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee == null)
            {
                return NotFound();//404
            }

            return View(ViewName, mappedEmp);
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _employeeRepository.Update(mappedEmp);

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

                return View(employeeVM);

            }

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _employeeRepository.Delete(mappedEmp);

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

                return View(employeeVM);


            }
        }

    }
}

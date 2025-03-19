﻿using AutoMapper;
using Company.ClientName.BLL.Interfaces;
using Company.ClientName.DAL.Models;
using Company.ClientName.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.ClientName.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeRepository EmployeeRepository, 
            //IDepartmentRepository departmentRepository,
            IMapper mapper
            )
        {
            _EmployeeRepository = EmployeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _EmployeeRepository.GetAll();
            }else
            {
                employees = _EmployeeRepository.GetByName(SearchInput);
            }

            //// Dictionary : 3 Property
            //// 1. ViewData : Transfer Extra Information From Controller (Action) To Veiw
            //ViewData["Message"] = "Hello From ViewData";

            //// 2. ViewBag  : Transfer Extra Information From Controller (Action) To Veiw
            //ViewBag.Message = new { Message = "Hello From ViewBag"};

            //// 3. TempData : Has One Requst LifeTime [ Send Or Save Value Per Request ]

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //var departments = _departmentRepository.GetAll();
            //ViewData["departments"] = departments;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) 
            {
                var employee = _mapper.Map<Employee>(model);

                var count = _EmployeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Created !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); // 400

            var employee = _EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id : {id} is not found" });

            var dto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(viewName, dto);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model, string viewName = "Edit")
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                var count = _EmployeeRepository.Update(employee);
                if (count > 0) return RedirectToAction(nameof(Index));
            }

            return View(viewName , model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                var count = _EmployeeRepository.Delete(employee);
                if (count > 0) return RedirectToAction(nameof(Index));
            }

        return View(model);
        }
    }
}

using AutoMapper;
using Company.ClientName.BLL.Interfaces;
using Company.ClientName.BLL.Repositories;
using Company.ClientName.DAL.Models;
using Company.ClientName.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.ClientName.PL.Controllers
{
    // MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        // ASK CLR Create Object From DepartmentRepository
        public DepartmentController(
            //IDepartmentRepository departmentRepository, 
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var department = _unitOfWork.DepartmentRepository.GetAll();


            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {

                var department = _mapper.Map<Department>(model);

                _unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "Department Is Created !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); // 400
            
            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id : {id} is not found" });

            var dto = _mapper.Map<CreateDepartmentDto>(department);

            return View(viewName, dto);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); // 400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id : {id} is not found" });

            var departmentDto = _mapper.Map<CreateDepartmentDto>(department);

            return View(departmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Edit([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);
                department.Id = id;

                if (id != department.Id) return BadRequest(); // 400

                _unitOfWork.DepartmentRepository.Update(department);
                var count = _unitOfWork.Complete();
                if (count > 0) return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken] // Prevent Any Request Out The WebApp
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id = id,
        //            Name = model.Name,
        //            code = model.Code,
        //            CreateAt = model.CreateAt
        //        };
        //        var count = _departmentRepository.Update(department);
        //        if (count > 0) return RedirectToAction(nameof(Index));
        //    }

        //    return View(model); // Need Casting The Return Data
        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); // 400

            //var department = _departmentRepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 404, message = $"Department With Id : {id} is not found" });

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);
                department.Id = id;

                if (id != department.Id) return BadRequest(); // 400

                _unitOfWork.DepartmentRepository.Delete(department);
                var count = _unitOfWork.Complete();
                if (count > 0) return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}

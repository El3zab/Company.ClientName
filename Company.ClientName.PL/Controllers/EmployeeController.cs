using AutoMapper;
using Company.ClientName.BLL.Interfaces;
using Company.ClientName.DAL.Models;
using Company.ClientName.PL.Dtos;
using Company.ClientName.PL.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.ClientName.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _EmployeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository EmployeeRepository, 
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            //_EmployeeRepository = EmployeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
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
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) 
            {
                if(model.Image is not null)
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");

                var employee = _mapper.Map<Employee>(model);

                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Created !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); // 400

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee With Id : {id} is not found" });

            var dto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model, string viewName = "Edit")
        {
            if (ModelState.IsValid)
            {
                if (model.ImageName is not null && model.Image is not null)
                    DocumentSetting.DeleteFile(model.ImageName, "images");
                if (model.Image is not null)
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0) return RedirectToAction(nameof(Index));
            }

            return View(viewName , model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    if (model.ImageName is not null)
                        DocumentSetting.DeleteFile(model.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
            }

        return View(model);
        }
    }
}

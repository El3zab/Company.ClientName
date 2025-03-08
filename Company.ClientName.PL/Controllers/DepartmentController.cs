using Company.ClientName.BLL.Interfaces;
using Company.ClientName.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.ClientName.PL.Controllers
{
    // MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        // ASK CLR Create Object From DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var department = _departmentRepository.GetAll();


            return View(department);
        }
    }
}

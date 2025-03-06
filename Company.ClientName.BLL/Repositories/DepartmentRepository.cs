using Company.ClientName.BLL.Interfaces;
using Company.ClientName.DAL.Data.Contexts;
using Company.ClientName.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.ClientName.BLL.Repositories
{
    class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context; // Null

        public DepartmentRepository()
        {
            _context = new CompanyDbContext();
        }
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department? Get(int id)
        {
            return _context.Departments.Find(id);
        }

        public int Add(Department model)
        {
            _context.Departments.Add(model);
            return _context.SaveChanges();
        }

        public int Update(Department model)
        {
            _context.Departments.Update(model);
            return _context.SaveChanges();
        }
        public int Delete(Department model)
        {
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }

    }
}

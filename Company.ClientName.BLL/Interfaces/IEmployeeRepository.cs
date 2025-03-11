using Company.ClientName.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.ClientName.BLL.Interfaces
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();

        Employee? Get(int id);

        int Add(Employee model);
        int Update(Employee model);
        int Delete(Employee model);
    }
}

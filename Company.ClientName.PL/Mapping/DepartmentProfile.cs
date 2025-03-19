using AutoMapper;
using Company.ClientName.DAL.Models;
using Company.ClientName.PL.Dtos;

namespace Company.ClientName.PL.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, CreateDepartmentDto>();
            CreateMap<CreateDepartmentDto,Department>();

        }

    }
}

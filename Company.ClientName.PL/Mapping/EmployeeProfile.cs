using AutoMapper;
using Company.ClientName.DAL.Models;
using Company.ClientName.PL.Dtos;

namespace Company.ClientName.PL.Mapping
{
    // CLR
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.EmpName))
                .ReverseMap();
            //CreateMap<Employee, CreateEmployeeDto>();
        }
    }
}

using AutoMapper;
using Company.ClientName.DAL.Models;
using Company.ClientName.PL.Dtos;
using Microsoft.CodeAnalysis.Options;

namespace Company.ClientName.PL.Mapping
{
    // CLR
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
                /*.ForMember(d => d.Name, O => O.MapFrom(S => $"{S.EmpName}"))*/;
            CreateMap<Employee, CreateEmployeeDto>()
                /*.ForMember(destination => destination.DepartmentName, 
                    option => option.MapFrom(Source => Source.Department.Name))*/;
        }
    }
}

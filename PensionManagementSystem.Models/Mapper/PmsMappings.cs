using PensionManagementSystem.Models;
using AutoMapper;
using PensionManagementSystem.Dtos;

namespace PensionManagementSystem.Mapper
{
    public class PmsMappings : Profile
    {
        public PmsMappings()
        {
            CreateMap<Authentication, AuthenticationDto>().ReverseMap();
            CreateMap<Bank, BankDto>().ReverseMap();
            CreateMap<PensionDetail, PensionDetailDto>().ReverseMap();
            CreateMap<PensionerDetail, PensionerDetailDto>().ReverseMap();
            CreateMap<ProcessPensionInput, ProcessPensionInputDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

        }
    }
}

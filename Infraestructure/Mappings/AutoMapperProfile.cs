using AutoMapper;
using Core.Models.Dtos;
using Core.Models.Entities;

namespace Infraestructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UsersDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<AssignmentVisits, AssignmentVisitsDto>().ReverseMap();
        }
    }
}

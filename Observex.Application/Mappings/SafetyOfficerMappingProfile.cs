using AutoMapper;
using Observex.Application.DTOs.SafetyOfficers;
using Observex.Core.Entities;

namespace Observex.Application.Mappings
{
    internal class SafetyOfficerMappingProfile : Profile
    {
        public SafetyOfficerMappingProfile()
        {
            CreateMap<SafetyOfficerDto, SafetyOfficer>().ReverseMap();
            CreateMap<PostSafetyOfficerDto, SafetyOfficer>();
        }
    }
}

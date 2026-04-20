using AutoMapper;
using Observex.Application.DTOs.Violations;
using Observex.Core.Entities;

namespace Observex.Application.Mappings
{
    internal class ViolationMappingProfile : Profile
    {
        public ViolationMappingProfile()
        {
            CreateMap<Violation, ViolationDto>().ReverseMap();
            CreateMap<PostAddViolationDto, Violation>();
            CreateMap<PostUpdateViolationDto, Violation>();
        }
    }
}

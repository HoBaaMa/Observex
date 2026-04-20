using AutoMapper;
using Observex.Application.DTOs.Workers;
using Observex.Core.Entities;

namespace Observex.Application.Mappings
{
    internal class WorkerMappingProfile : Profile
    {
        public WorkerMappingProfile()
        {
            CreateMap<WorkerDto, Worker>().ReverseMap();
            CreateMap<PostWorkerDto, Worker>();
        }
    }
}

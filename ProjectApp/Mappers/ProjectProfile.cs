using AutoMapper;
using ProjectApp.Core;
using ProjectApp.Persistence;

namespace ProjectApp.Mappers;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectDb, Project>().ReverseMap();
    }
}
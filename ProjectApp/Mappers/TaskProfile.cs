using AutoMapper;
using ProjectApp.Persistence;

namespace ProjectApp.Mappers;

public class TaskProfile : Profile
{
  public TaskProfile()
  { 
    CreateMap<TaskDb, Core.Task>().ReverseMap();
  }

}
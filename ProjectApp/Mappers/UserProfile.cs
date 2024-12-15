using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectApp.Areas.Identity.Data;
using ProjectApp.Models.Auctions;

namespace ProjectApp.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserVm, AppIdentityUser>().ReverseMap();
    }
}
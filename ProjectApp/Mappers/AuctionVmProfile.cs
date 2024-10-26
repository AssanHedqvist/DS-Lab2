using AutoMapper;
using ProjectApp.Core;
using ProjectApp.Models.Auctions;

namespace ProjectApp.Mappers;

public class AuctionVmProfile : Profile
{
    public AuctionVmProfile()
    {
        CreateMap<Auction, AuctionVm>().ReverseMap();
    }
}
using ProjectApp.Core;
using ProjectApp.Core.Interfaces;

namespace ProjectApp.Persistence;

public class AuctionRepository : GenericRepository<AuctionDb>, IAuctionRepository
{
    public AuctionRepository(AuctionDbContext context) : base(context)
    {
    }
}
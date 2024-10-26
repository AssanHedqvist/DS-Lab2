using ProjectApp.Core;
using ProjectApp.Core.Interfaces;

namespace ProjectApp.Persistence;

public class BidRepository : GenericRepository<BidDb>, IBidRepository
{
    public BidRepository(AuctionDbContext context) : base(context)
    {
    }

    public List<BidDb> GetBidsByAuctionId(int auctionId)
    {
        return _context.BidsDbs.Where(b => b.AuctionId == auctionId).ToList();
    }
}
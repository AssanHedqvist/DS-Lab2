using ProjectApp.Persistence;

namespace ProjectApp.Core.Interfaces;

public interface IBidRepository : IGenericRepository<BidDb>
{
    List<BidDb> GetBidsByAuctionId(int auctionId);
}
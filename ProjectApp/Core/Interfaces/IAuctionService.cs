using ProjectApp.Models.Auctions;

namespace ProjectApp.Core.Interfaces;

public interface IAuctionService
{
    void AddAuction(AuctionVm auctionVm);
    
    void UpdateAuction(int id, string username, string newDescription);

    Auction GetById(int id);

    void AddBid(int id, Bid bid);
    
    List<Auction> GetAllAuctions();
    
    List<Auction> GetOngoingAuctions();
    
    List<Auction> GetBidActive(string username);
    
    List<Auction> GetWonAuctions(string username);

    void DeleteAuction(int id, string username);
    
    List<Auction> GetAuctionsByUser(string id);
}
namespace ProjectApp.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllAuctions();
    
    void AddAuction(Auction newAuction);
    
    void EditAuction(int id, string username, string newDescription);
    
    Auction GetById(int id, string username);
    
    void AddBid(int id, Bid bid);
    
    List<Auction> GetBidActive(string username);
    
    List<Auction> GetWonAuctions(string username);
}
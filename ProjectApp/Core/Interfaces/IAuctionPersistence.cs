namespace ProjectApp.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllAuctions();
    
    void AddAuction(Auction newAuction);
    
    Auction GetById(int id, string username);
    
    void UpdateAuction(Auction auction);
    
    void AddBid(int id, Bid bid);
    
}
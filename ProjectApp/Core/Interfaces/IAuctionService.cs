namespace ProjectApp.Core.Interfaces;

public interface IAuctionService
{
    void AddAuction(
        string name, 
        string description, 
        DateTime expirationDate,
        double startingPrice, 
        string username);

    void EditAuction(int id, string username, string newDescription);

    Auction GetById(int id, string username);

    void AddBid(int id, Bid bid);
    
    List<Auction> GetAllAuctions();
    
    List<Auction> GetOngoingAuctions();
    
    List<Auction> GetBidActive(string username);
    
    List<Auction> GetWonAuctions(string username);

}
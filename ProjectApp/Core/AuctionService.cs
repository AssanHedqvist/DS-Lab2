using ProjectApp.Core.Interfaces;

namespace ProjectApp.Core;

public class AuctionService : IAuctionService
{
    private readonly IAuctionPersistence _auctionPersistence;

    public AuctionService(IAuctionPersistence auctionPersistence)
    {
        _auctionPersistence = auctionPersistence;
    }

    public void AddAuction(string name,
        string description,
        DateTime expirationDate,
        double startingPrice,
        string username)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(description) ||
            string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Name, description, and username can neither be empty or null.");
        }

        if (DateTime.Compare(expirationDate, DateTime.Now) <= 0)
            throw new ArgumentException("Expiration date invalid");
        if (startingPrice < 0)
            throw new ArgumentException("Price cannot be negative.");
        _auctionPersistence.AddAuction(new Auction(name, description, username, startingPrice, DateTime.Now));
    }

    public void EditAuction(int id, string username, string newDescription)
    {
        
    }

    public Auction GetById(int id, string username)
    {
        Auction auction = _auctionPersistence.GetById(id, username);
        auction.sortBids();
        return auction;

    }

    public void AddBid(int id, Bid bid)
    {
        _auctionPersistence.AddBid(id, bid);
    }

    public List<Auction> GetAllAuctions()
    {
        return _auctionPersistence.GetAllAuctions();
    }

    public List<Auction> GetOngoingAuctions()
    {
        List<Auction> activeAuctions = _auctionPersistence.GetAllAuctions();
        foreach (Auction auction in activeAuctions.ToList())
        {
            if(auction.expirationDate < DateTime.Now)
            {
                activeAuctions.Remove(auction);
            }
            //add sort by date
        }
        return activeAuctions;
    }

    public List<Auction> GetBidActive(string username)
    {
        return _auctionPersistence.GetBidActive(username);
    }

    public List<Auction> GetWonAuctions(string username)
    {
        return null;
    }
}
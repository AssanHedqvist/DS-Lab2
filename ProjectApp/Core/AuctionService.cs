using ProjectApp.Core.Interfaces;

namespace ProjectApp.Core;

public class  AuctionService : IAuctionService
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
        _auctionPersistence.AddAuction(new Auction(name, description, username, startingPrice, expirationDate));
    }

    public void UpdateAuction(int id, string username, string newDescription)
    {
        Auction auction = _auctionPersistence.GetById(id, username);
        if (auction.username != username)
            throw new ArgumentException("You are not the owner of this auction.");
        auction.description = newDescription;
        _auctionPersistence.UpdateAuction(auction);
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
        activeAuctions = activeAuctions
            .Where(a => a.expirationDate >= DateTime.Now)
            .OrderBy(a => a.expirationDate)
            .ToList();
        return activeAuctions;
    }
    
    public List<Auction> GetBidActive(string username)
    {
        return _auctionPersistence.GetBidActive(username);
    }

    public List<Auction> GetWonAuctions(string username)
    {
        return _auctionPersistence.GetWonAuctions(username);
    }
}
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
        Auction auction = _auctionPersistence.GetById(id, bid.username);
        if(bid.username.Equals(auction.username))
            throw new ArgumentException("You are the owner of this auction.");
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
        List<Auction> list = _auctionPersistence.GetAllAuctions();
        List<Auction> activeBidAuctions = new List<Auction>();
        foreach (Auction auction in list)
        {
            if (!auction.Bids.Any() || auction.expirationDate < DateTime.Now)
                continue;
            foreach (Bid bid in auction.Bids)
            {
                if (bid.username.Equals(username))
                    activeBidAuctions.Add(auction);
            }
        }
        return activeBidAuctions;
    }

    public List<Auction> GetWonAuctions(string username)
    {
        List<Auction> list = _auctionPersistence.GetAllAuctions();
        List<Auction> wonAndExpiredAuctions = new List<Auction>();
        foreach (Auction auction in list)
        {
            if (!auction.Bids.Any()) 
                continue;
            auction.sortBids();
            if(auction.Bids.First().username.Equals(username) && auction.expirationDate < DateTime.Now)
                wonAndExpiredAuctions.Add(auction);
        }
        return wonAndExpiredAuctions;
    }
}
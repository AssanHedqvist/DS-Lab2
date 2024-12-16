using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ProjectApp.Core.Interfaces;
using ProjectApp.Models.Auctions;
using ProjectApp.Persistence;

namespace ProjectApp.Core;

public class  AuctionService : IAuctionService
{

    private readonly IAuctionRepository _auctionRepository;
    private readonly IBidRepository _bidRepository;
    private readonly IMapper _mapper;

    public AuctionService(IAuctionRepository auctionRepository, IBidRepository bidRepository, IMapper mapper)
    {
        _mapper = mapper;
        _bidRepository = bidRepository;
        _auctionRepository = auctionRepository;
    }
    
    public void AddAuction(AuctionVm auctionVm)
    {
        
        Auction auction = _mapper.Map<Auction>(auctionVm);
        
        if (DateTime.Compare(auction.expirationDate, DateTime.Now) <= 0)
            throw new ArgumentException("Expiration date invalid");
        
        if (auction.startPrice < 0)
            throw new ArgumentException("Price cannot be negative.");
        
        AuctionDb auctionDb = _mapper.Map<AuctionDb>(auction);
        _auctionRepository.Add(auctionDb);
    }

    public void UpdateAuction(int id, string username, string newDescription)
    {
        AuctionDb auctionDb = _auctionRepository.GetById(id);
        if (auctionDb.username != username)
            throw new ArgumentException("You are not the owner of this auction.");
        auctionDb.description = newDescription;
        _auctionRepository.Update(auctionDb);
    }

    public Auction GetById(int id)
    {
        AuctionDb auctionDb = _auctionRepository.GetById(id);
        Auction auction = _mapper.Map<Auction>(auctionDb);
        List<BidDb> bidDbs = _bidRepository.GetBidsByAuctionId(id);
        foreach(BidDb bidDb in bidDbs)
        {
            auction.addBid(_mapper.Map<Bid>(bidDb));
        }
        auction.sortBids();
        return auction;
    }

    public void AddBid(int id, Bid bid)
    {
        //finns det krav för minimum bud?
        AuctionDb auctionDb = _auctionRepository.GetById(id);
        List<BidDb> bidDbs = _bidRepository.GetBidsByAuctionId(auctionDb.Id);
        if (bid.bidSize < auctionDb.startPrice || !bidDbs.IsNullOrEmpty() && bid.bidSize <= bidDbs.Last().bidSize)
            throw new ArgumentException("Invalid bid size.");
        if(bid.username.Equals(auctionDb.username))
            throw new ArgumentException("You are the owner of this auction.");
        BidDb bidDb = _mapper.Map<BidDb>(bid);
        auctionDb.BidDbs.Add(bidDb);
        _bidRepository.Add(bidDb);
    }

    public List<Auction> GetAllAuctions()
    {
        List<AuctionDb> auctionDbs = _auctionRepository.GetAll();
        List<Auction> auctions = new List<Auction>();
        foreach (AuctionDb auctionDb in auctionDbs)
        {
            auctions.Add(_mapper.Map<Auction>(auctionDb));
        }

        return auctions;
    }

    public List<Auction> GetOngoingAuctions()
    {
        List<AuctionDb> auctionDbs = _auctionRepository.GetAll();
        List<Auction> ongoingAuctions = new List<Auction>();
        foreach (AuctionDb auctionDb in auctionDbs)
        {
            if (auctionDb.expirationDate > DateTime.Now)
            {
                ongoingAuctions.Add(_mapper.Map<Auction>(auctionDb));
            }
        }
        return ongoingAuctions;
    }
    
    public List<Auction> GetBidActive(string username)
    {
            var activeBidAuctions = _auctionRepository
                .GetAll()
                .Where(auction => auction.expirationDate >= DateTime.Now) // Keep only active auctions
                .Select(auction => new
                {
                    Auction = auction,
                    Bids = _bidRepository.GetBidsByAuctionId(auction.Id)
                })
                .Where(x => x.Bids.Any()) // Keep only auctions with bids
                .Where(x => x.Bids.Any(bid => bid.username == username)) // Keep only auctions with bids by the user
                .Select(x => _mapper.Map<Auction>(x.Auction)) // Map AuctionDb to Auction
                .ToList();

            return activeBidAuctions;
        
    }

    public List<Auction> GetWonAuctions(string username)
    {
        var wonAuctions = _auctionRepository
            .GetAll()
            .Where(auction => auction.expirationDate < DateTime.Now) // Keep only expired auctions
            .Select(auction => new
            {
                Auction = auction,
                Bids = _bidRepository.GetBidsByAuctionId(auction.Id)
            })
            .Where(x => x.Bids.Any()) // Keep only auctions with bids
            .Where(x => x.Bids.Last().username == username) // Keep only auctions where the user has the highest bid
            .Select(x => _mapper.Map<Auction>(x.Auction)) // Map AuctionDb to Auction
            .ToList();

        return wonAuctions;
    }

    public void DeleteAuction(int id, string username)
    {
        AuctionDb auctionDb = _auctionRepository.GetById(id);
        if (auctionDb.username != username)
            throw new ArgumentException("You are not the owner of this auction.");
        _auctionRepository.Remove(auctionDb);
    }
    
    public List<Auction> GetAuctionsByUser(string username)
    {
        List<AuctionDb> auctionDbs = _auctionRepository.GetAll().Where(a => a.username == username).ToList();
        List<Auction> auctions = auctionDbs.Select(a => _mapper.Map<Auction>(a)).ToList();
        return auctions;
    }
}
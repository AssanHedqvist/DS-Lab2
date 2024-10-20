using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Core;
using ProjectApp.Core.Interfaces;

namespace ProjectApp.Persistence;

public class MySqlAuctionPersistence : IAuctionPersistence
{
    private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public MySqlAuctionPersistence(AuctionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<Auction> GetAllAuctions()
    {
        List<AuctionDb> auctionDblist = _dbContext.AuctionDbs
            .Include(a => a.BidDbs)
            .ToList();
        List<Auction> result = new List<Auction>();
        foreach (AuctionDb auctionDb in auctionDblist)
        {
            Auction tempAuction = _mapper.Map<Auction>(auctionDb);
            foreach (BidDb bidDb in auctionDb.BidDbs)
            {
                tempAuction.addBid(_mapper.Map<Bid>(bidDb));
            }
            result.Add(tempAuction);
        }

        return result;
    }
    
    
    public void AddAuction(Auction newAuction)
    {
        AuctionDb auctionDb = _mapper.Map<AuctionDb>(newAuction);
        _dbContext.AuctionDbs.Add(auctionDb);
        _dbContext.SaveChanges();
    }

    public Auction GetById(int id, string username)
    {
        
            AuctionDb auctionDb = _dbContext.AuctionDbs
            .Where(a => a.Id == id)
            .Include(a => a.BidDbs)
            .FirstOrDefault();
            
            Auction auction = _mapper.Map<Auction>(auctionDb);
        
            foreach (BidDb bidDb in auctionDb.BidDbs)
            {
                auction.addBid(_mapper.Map<Bid>(bidDb));
            }
            return auction;
        
    }

    public void UpdateAuction(Auction auction)
    {
        var auctionDb = _mapper.Map<AuctionDb>(auction);
        var existingAuction = _dbContext.AuctionDbs.Find(auction.id);
        if (existingAuction != null)
        {
            _dbContext.Entry(existingAuction).CurrentValues.SetValues(auctionDb);
            _dbContext.SaveChanges();
        }

    }

    public void AddBid(int id, Bid bid)
    {
        AuctionDb auctionDb = _mapper.Map<AuctionDb>(GetById(id, null));
        BidDb bidDb = _mapper.Map<BidDb>(bid);

        bidDb.AuctionId = auctionDb.Id;
        _dbContext.BidsDbs.Add(bidDb);
        auctionDb.BidDbs.Add(bidDb);
        _dbContext.SaveChanges();
    }
    
    public List<Auction> GetOngoingAuctions()
    {
        //Vet inte om vi ska ska göra såhär
        // vet inte heller, men implementerar ändå
        List<Auction> allAuctions = GetAllAuctions();
        List<Auction> ongoingAuctions = new List<Auction>();
        foreach (Auction adb in allAuctions)
        {
            if (adb.expirationDate < DateTime.Now)
                ongoingAuctions.Add(adb);
        }
        return ongoingAuctions;
    }
}
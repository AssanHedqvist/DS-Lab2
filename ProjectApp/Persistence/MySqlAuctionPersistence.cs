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
        List<AuctionDb> auctionDb = _dbContext.AuctionDbs
            .ToList();
        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDb)
        {
            result.Add(_mapper.Map<Auction>(adb));
        }
        return result;
    }
    
    
    public void AddAuction(Auction newAuction)
    {
        throw new NotImplementedException();;
    }

    public void EditAuction(int id, string username, string newDescription)
    {
        throw new NotImplementedException();
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
         _dbContext.AuctionDbs.Update(auctionDb);
         _dbContext.SaveChanges();
            
    }


    public void AddBid(int id, Bid bid)
    {
        throw new NotImplementedException();
    }

    public List<Auction> GetOngoingAuctions()
    {
        //Vet inte om vi ska ska göra såhär
        return GetAllAuctions();
    }

    public List<Auction> GetBidActive(string username)
    {
        throw new NotImplementedException();
    }

    public List<Auction> GetWonAuctions(string username)
    {
        throw new NotImplementedException();
    }
}
using System.ComponentModel.DataAnnotations;
using ProjectApp.Core;

namespace ProjectApp.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    
    public string name { get; set; }
    
    public string description { get; set; }
    
    public string username { get; set; }
    
    public double startPrice { get; set; }
    
    
    [Display(Name = "Expiration date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime expirationDate { get; set; }
    
    public List<BidVm> bidVms { get; set; } = new();
    
    public static AuctionDetailsVm FromAuction(Auction auction)
    {
        var detailsVm = new AuctionDetailsVm()
        {
            Id = auction.id,
            name = auction.name,
            startPrice = auction.startPrice,
            description = auction.description,
            username = auction.username,
            expirationDate = auction.expirationDate
        };
        foreach(var bid in auction.Bids)
        {
            detailsVm.bidVms.Add(BidVm.FromBid(bid));
        }
        
        return detailsVm;
    }
    
    
    
    
}
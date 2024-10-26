using System.ComponentModel.DataAnnotations;
using ProjectApp.Core;

namespace ProjectApp.Models.Auctions;

public class BidVm
{
    [ScaffoldColumn(false)] public int Id { get; set; }
    
    public string username { get; set; }
    
    public double bidSize { get; set; }
    
    [Display(Name = "Bid time")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime bidTime { get; set; }
    
    public static BidVm FromBid(Bid bid)
    {
        return new BidVm()
        {
            Id = bid.Id,
            username = bid.username,
            bidSize = bid.bidSize,
            bidTime = bid.bidTime
        };
    }
    
    
    
}
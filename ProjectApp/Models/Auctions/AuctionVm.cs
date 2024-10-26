using System.ComponentModel.DataAnnotations;
using ProjectApp.Core;

namespace ProjectApp.Models.Auctions;

public class AuctionVm
{
 
    
    [ScaffoldColumn(false)] public int Id { get; set; }
    
    public string name { get; set; }
    
    public string description { get; set; }
    
    public double startPrice { get; set; }
    
    public string username { get; set; }
    
    [Display(Name = "Expiration Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime expirationDate { get; set; }
    


    public AuctionVm(string name, string description, double startPrice, string username, DateTime expirationDate)
    {
        this.name = name;
        this.description = description;
        this.startPrice = startPrice;
        this.username = username;
        this.expirationDate = expirationDate;
    }

    public AuctionVm()
    {
        
    }
    
    

    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm
        {
            Id = auction.Id,
            name = auction.name,
            description = auction.description,
            startPrice = auction.startPrice,
            username = auction.username,
            expirationDate = auction.expirationDate,
        };
    }
    
}
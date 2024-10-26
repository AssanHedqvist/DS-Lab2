using System.ComponentModel.DataAnnotations;
using ProjectApp.Core;

namespace ProjectApp.Persistence;

public class AuctionDb : BaseEntityDb
{
    
    [Required]
    [MaxLength(128)]
    public string name { get; set; }
    
    [Required]
    public string description { get; set; }
    
    [Required]
    public string username { get; set; }
    
    [Required]
    public double startPrice { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime expirationDate { get; set; }
    
    public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
    
}
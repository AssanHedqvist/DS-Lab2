using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectApp.Core;

namespace ProjectApp.Persistence;

public class BidDb : BaseEntityDb
{
    [Required]
    public string username { get; set; }
    
    [Required]
    public double bidSize { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime bidTime { get; set; }
    
    public int AuctionId { get; set; }
    
    [ForeignKey("AuctionId")]
    public AuctionDb AuctionDb { get; set; }
    
}
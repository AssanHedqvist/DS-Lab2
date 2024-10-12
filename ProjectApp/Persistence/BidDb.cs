using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApp.Persistence;

public class BidDb
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string username { get; set; }
    
    [Required]
    public double bidSize { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime bidTime { get; set; }
    
    [ForeignKey("AuctionId")]
    public AuctionDb AuctionDb { get; set; }
    
}
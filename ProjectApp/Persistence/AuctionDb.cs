using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Persistence;

public class AuctionDb
{
    [Key]
    public int Id { get; set; }
    
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
    
    [Required]
    public bool isExpired { get; set; }
    
    public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
    
    
    
}
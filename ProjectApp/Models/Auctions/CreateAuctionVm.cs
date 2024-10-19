namespace ProjectApp.Models.Auctions;

public class CreateAuctionVm
{
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime ExpirationDate { get; set; }
    
    public double StartingPrice { get; set; }
    
    public string Username { get; set; }
    
    public CreateAuctionVm(string name, string description, DateTime expirationDate, double startingPrice, string username)
    {
        Name = name;
        Description = description;
        ExpirationDate = expirationDate;
        StartingPrice = startingPrice;
        Username = username;
    }
    
    public CreateAuctionVm()
    {
        
    }
}
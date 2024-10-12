namespace ProjectApp.Core;

public class Auction
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string username { get; set; }
    public double startPrice { get; set; }
    public DateTime expirationDate { get; set; }
    private List<Bid> _bids = new List<Bid>(); 
    public IEnumerable<Bid> Bids => _bids;
    
    
    public bool isExpired { get; set; }
    public Auction(int id, string name, string description, string username, double startPrice, DateTime expirationDate)
    {   
        this.id = id;
        this.name = name;
        this.description = description;
        this.username = username;
        this.startPrice = startPrice;
        this.expirationDate = expirationDate;
        this.isExpired = false;
    }
    
    
    public Auction(string name, string description, string username, double startPrice, DateTime expirationDate)
    {
        this.name = name;
        this.description = description;
        this.username = username;
        this.startPrice = startPrice;
        this.expirationDate = expirationDate;
        this.isExpired = false;
    }
    
    public void addBid(Bid newBid)
    {
        _bids.Add(newBid);
    }
    public void sortBids()
    {
        _bids = _bids.OrderByDescending(b => b.bidSize).ToList();
    }
    
    public override string ToString()
    {
        return $"{id}: {name} - {description} - {username} - {startPrice} - {expirationDate} - {isExpired}";
    }
    
}
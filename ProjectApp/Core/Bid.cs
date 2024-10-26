using ProjectApp.Persistence;

namespace ProjectApp.Core;

public class Bid
{
    
    public int Id { get; set; }
    public string username { get; set; }
    public double bidSize { get; set; }
    public DateTime bidTime { get; set; }
    
    public Bid(string username, double bidSize, DateTime bidTime)
    {
        this.username = username;
        this.bidSize = bidSize;
        this.bidTime = bidTime;
    }
    public Bid(int Id, string username, double bidSize, DateTime bidTime)
    {
        this.Id = Id;
        this.username = username;
        this.bidSize = bidSize;
        this.bidTime = bidTime;
    }
}
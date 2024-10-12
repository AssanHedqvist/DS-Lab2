namespace ProjectApp.Core;

public class Bid
{
    public int id { get; set; }
    public string username { get; set; }
    public double bidSize { get; set; }
    public DateTime bidTime { get; set; }

    public Bid(string username, double bidSize, DateTime bidTime)
    {
        this.username = username;
        this.bidSize = bidSize;
        this.bidTime = bidTime;
    }
    public Bid(int id, string username, double bidSize, DateTime bidTime)
    {
        this.id = id;
        this.username = username;
        this.bidSize = bidSize;
        this.bidTime = bidTime;
    }
    
    

}
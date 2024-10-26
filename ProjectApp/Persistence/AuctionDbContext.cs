using Microsoft.EntityFrameworkCore;
using ProjectApp.Core;

namespace ProjectApp.Persistence;

public class AuctionDbContext : DbContext
{
    public DbSet<AuctionDb> AuctionDbs { get; set; }
    public DbSet<BidDb> BidsDbs { get; set; }
    
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
    }
}
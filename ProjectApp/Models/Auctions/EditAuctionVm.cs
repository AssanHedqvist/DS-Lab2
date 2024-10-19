using ProjectApp.Core;

namespace ProjectApp.Models.Auctions;

public class EditAuctionVm
{ 
   public int id { get; set; }
   public string description { get; set; }
   
   public static EditAuctionVm FromAuction(Auction auction)
   {
       return new EditAuctionVm
       {
           id = auction.id,
           description = auction.description
       };
   }
}
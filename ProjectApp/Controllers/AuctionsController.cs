using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using ProjectApp.Core;
using ProjectApp.Core.Interfaces;
using ProjectApp.Models.Auctions;

namespace ProjectApp.Controllers
{
    [Authorize]
    public class AuctionsController : Controller
    {
        
        private IAuctionService _auctionService;
        
        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
 
        public ActionResult Index()
        {
            List<Auction> auctions = _auctionService.GetOngoingAuctions();
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            foreach (var auction in auctions)
            {
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionVms);
        }
        
        public ActionResult Details(int id)
        {
            Auction auction = _auctionService.GetById(id);
            AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction);
            return View(detailsVm);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
               
                AuctionVm auctionVm = new AuctionVm(
                    collection["name"], 
                    collection["description"], 
                    double.Parse(collection["startPrice"]),
                    User.Identity.Name,
                    DateTime.Parse(collection["expirationDate"]));
                _auctionService.AddAuction(auctionVm);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            Auction auction = _auctionService.GetById(id);
            AuctionVm auctionVm = AuctionVm.FromAuction(auction);
            return View(auctionVm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string description)
        {
            try
            {   
                
                _auctionService.UpdateAuction(id, User.Identity.Name, description);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //redirect back to edit page
                return RedirectToAction(nameof(Edit), new { id });
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _auctionService.DeleteAuction(id, User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //error page
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceBid(int auctionId, double bidSize)
        {
            try
            {
                //kanske skicka in en BidVM istället för att skapa en bid här
                _auctionService.AddBid(auctionId, new Bid(User.Identity.Name, 
                    bidSize, 
                    DateTime.Now));
                return RedirectToAction(nameof(Details), new { id = auctionId });
            }
            catch (Exception ex)
            {
                //redirect to error page
                return View();
            }
        }
        
        public ActionResult ActiveBidAuctions()
        {
            List<Auction> auctions = _auctionService.GetBidActive(User.Identity.Name);
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            foreach (var auction in auctions)
            {
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionVms);
        }
        
        public ActionResult WonAuctions()
        {
            List<Auction> auctions = _auctionService.GetWonAuctions(User.Identity.Name);
            List<AuctionVm> auctionVms = new List<AuctionVm>();
            foreach (var auction in auctions)
            {
                auctionVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionVms);
        }
    }
}

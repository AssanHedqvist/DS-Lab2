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
        // GET: AuctionController
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

        // GET: AuctionController/Details/5
        public ActionResult Details(int id)
        {
            Auction auction = _auctionService.GetById(id, User.Identity.Name);
            AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction);
            return View(detailsVm);
        }

        // GET: AuctionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                _auctionService.AddAuction(
                    collection["name"],
                    collection["description"],
                    DateTime.Parse(collection["expirationDate"]),
                    double.Parse(collection["startPrice"]),
                    User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuctionController/Edit/5
        public ActionResult Edit(int id)
        {
            Auction auction = _auctionService.GetById(id, User.Identity.Name);
            EditAuctionVm editAuctionVm = EditAuctionVm.FromAuction(auction);
            return View(editAuctionVm);
        }

        // POST: AuctionController/Edit/5
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

        // GET: AuctionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceBid(int auctionId, double bidSize)
        {
            try
            {
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

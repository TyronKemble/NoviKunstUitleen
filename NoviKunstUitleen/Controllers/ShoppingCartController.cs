using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NoviKunstUitleen.Models;
using NoviKunstUitleen.ViewModel;

namespace NoviKunstUitleen.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context;

        public ShoppingCartController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = "Student")]
        // GET: /ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

 
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }


        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(string endOfLoan, int id)
        {
            // Retrieve the album from the database
            var addedArt = _context.Arts.Single(a => a.ArtsId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedArt, endOfLoan);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index", "ShoppingCart");
        }
        [HttpPost]
        public ActionResult LoanPeriod(string endOfLoan)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var getCarts = cart.GetCartItems();

            foreach (var item in getCarts)
            {
                ArtLendPeriod artLendPeriod = new ArtLendPeriod();
                artLendPeriod.EndOfLend = endOfLoan;
                artLendPeriod.ArtsId = item.ArtsId;

                _context.LendPeriod.Add(artLendPeriod);
            }

            _context.SaveChanges();
            return RedirectToAction("AddressAndPayment", "Checkout");
        }

        // /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {

            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string artNm = _context.Carts.FirstOrDefault(a => a.RecordId == id).Arts.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id, artNm);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(artNm) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
               
            };
            return Json(results);
        }

        
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
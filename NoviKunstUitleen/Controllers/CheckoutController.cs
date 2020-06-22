using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using NoviKunstUitleen.Models;
using NoviKunstUitleen.ViewModel;

namespace NoviKunstUitleen.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        private readonly string privateETHSender = "54b95bf755be54758e9617d636bc745215f29f14e08fa6bbc0ab307b652a6c3a";
        private readonly string web3Url = "https://ropsten.infura.io/v3/cc7d9b90b1964ada9f24467dc0f0f780";
        private readonly string studentETHAddress = "0x28C5524Cb44062eDcf7fCb5d78F39B8B80aeF4d4";
        private readonly string staffMemberETHAddress = "0xc3202910De95C5B34c43B39E8178b35D4b45c8bB";

        public CheckoutController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            
            return View();
        }



        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            Session["order"] = order;

            if (!ModelState.IsValid)
            {
                return View(order);
            }
            else
            {
                return RedirectToAction("PaymentIndex");
            }
        }

        public ActionResult PaymentIndex()
        {

            // Get total price
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var cartTotal = cart.GetTotal();

            // Get total Cart Items
            var cartItemsWithID = cart.GetCartItems().ToList();

            // Get current Art creator from cart
            var artsCreator = "";
            foreach (var item in cartItemsWithID)
            {
                artsCreator = item.Arts.Creator;
            }

            // Get eth adres of artCreator
            var userCreator = _context.Users.FirstOrDefault(a => a.UserName == artsCreator)
                .CryptoWallet;

            var result = new CryptoViewModel
            {
                TotalAmount = cartTotal,
                SendEthToAdress = userCreator,
            };

            return View(result);

        }

        public ActionResult PaymentValid()
        {
            var order = (Order)Session["order"];
            
            order.Username = User.Identity.Name;
            order.OrderDate = DateTime.Now;
            _context.Orders.Add(order);

            //Process the order / Creator order
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            return RedirectToAction("Complete",
                new { id = order.OrderId });
        }

        // Function checks of payment 
        public async Task<ActionResult> CheckPayment()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // Get total cart price
            var totalCartValue = cart.GetTotal();

            // Get current user ETH adres of payment
            var currentUserCrypto = _context.Users.FirstOrDefault(c => c.UserName == User.Identity.Name).CryptoWallet;

            // Get eth adres of artCreator
            var cartItemsWithID = cart.GetCartItems().ToList();
            var artsCreator = "";
            foreach (var item in cartItemsWithID)
            {
                artsCreator = item.Arts.Creator;
            }
            var creatorCrypto = _context.Users.FirstOrDefault(a => a.UserName == artsCreator).CryptoWallet;

            // Convert total value to eth WITH API
            decimal ethValue = totalCartValue;


                                                // Send to ETH adres section /// 
            // Save private key in enviroment variable - Zoek uit
            var account = new Account(privateETHSender);
            //Now let's create an instance of Web3 using our account pointing to our nethereum testchain
            var web3 = new Web3(account, web3Url);

            // Check the balance of the account we are going to send the Ether
            var balanceFromSender = await web3.Eth.GetBalance.SendRequestAsync(studentETHAddress);
            if (Web3.Convert.FromWei(balanceFromSender.Value) > ethValue)
            {
                // Lets transfer 1.11 Ether
                var transaction = await web3.Eth.GetEtherTransferService()
                    .TransferEtherAndWaitForReceiptAsync(staffMemberETHAddress, ethValue);

                return RedirectToAction("PaymentValid");
            }
            else
            {
                return RedirectToAction("PaymentError");
            }

                                                // End send to ETH adres section //
        }

        public ActionResult PaymentError()
        {
            return View();
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = _context.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {

                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
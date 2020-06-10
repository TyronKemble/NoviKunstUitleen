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
                return RedirectToAction("Payment");
            }
        }

        public ActionResult Payment()
        {

            // totale prijs moet ik hebben
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var cartTotal = cart.GetTotal();
            var cartItemsWithID = cart.GetCartItems().ToList();
            var artsCreator = "";
            foreach (var item in cartItemsWithID)
            {
                artsCreator = item.Arts.Creator;
            }
            // welk Eth adres het moet worden verzonden wie is de creator
            var userCreator = _context.Users.FirstOrDefault(a => a.UserName == artsCreator).CryptoWallet;

            // wat is het ETh adres van de creator 

            var result = new CryptoViewModel
            {
                TotalAmount = cartTotal,
                SendEthToAdress = userCreator,
            };
            //Eventueel totalbalance aangeven gebruiker
            // 
            return View(result);

        }

        public async Task<bool> GetCurrentEuroValue()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/coins/ethereum/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var euroValue = 0;
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString("https://api.coingecko.com/api/v3/coins/ethereum/tickers");
             
                // HTTP GET
                var responseTask = client.GetAsync("tickers");
                responseTask.Wait();
                    List<string> list = new List<string>();
                    var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                        var readTask = JsonConvert.DeserializeObject<dynamic>(json);
                        var name = readTask.tickers;
                        foreach (var item in readTask.tickers)
                        {
                            if (item.target == "Eur")
                            {
                                euroValue = item.last;
                            }
                        }

                        Console.WriteLine(euroValue);
                }

                }
            }

            return true;
        }

        public ActionResult PaymentCheck()
        {
            var order = (Order)Session["order"];
            
            order.Username = User.Identity.Name;
            order.OrderDate = DateTime.Now;

            //Save Order
            _context.Orders.Add(order);

            //Process the order
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            //return Json(new { url = Url.Action("Complete", "checkout", new { id = order.OrderId }) });
            return RedirectToAction("Complete",
                new { id = order.OrderId });
        }

        public ActionResult Test()
        {
            return RedirectToAction("Complete",
                new { id = 10});
        }

        public async Task<ActionResult> CheckPayment()
            {
            //https://stackoverflow.com/questions/185208/how-do-i-get-and-set-environment-variables-in-c
            // https://faucet.ropsten.be/ - Eth

            var cart = ShoppingCart.GetCart(this.HttpContext);
            // total cart price
            var totalCartValue = cart.GetTotal();

            // Get current User ETH adres of payment
            var currentUserCrypto = _context.Users.FirstOrDefault(c => c.UserName == User.Identity.Name).CryptoWallet;

            // welk Eth adres het moet worden verzonden wie is de creator
            var cartItemsWithID = cart.GetCartItems().ToList();
            var artsCreator = "";
            foreach (var item in cartItemsWithID)
            {
                artsCreator = item.Arts.Creator;
            }
            var creatorCrypto = _context.Users.FirstOrDefault(a => a.UserName == artsCreator).CryptoWallet;

            // Convert total value to eth WITH API
            decimal ethValue = totalCartValue;
            // Send to ETH adres 

            // Save private key in enviroment variable - Zoek uit
            var privateKey = "54b95bf755be54758e9617d636bc745215f29f14e08fa6bbc0ab307b652a6c3a";
            var account = new Account(privateKey);
            //Now let's create an instance of Web3 using our account pointing to our nethereum testchain
            var web3 = new Web3(account, "https://ropsten.infura.io/v3/cc7d9b90b1964ada9f24467dc0f0f780");

            // Check the balance of the account we are going to send the Ether
            var balanceFromSender = await web3.Eth.GetBalance.SendRequestAsync("0x28C5524Cb44062eDcf7fCb5d78F39B8B80aeF4d4");
            if (Web3.Convert.FromWei(balanceFromSender.Value) > ethValue)
            {
                // Lets transfer 1.11 Ether
                var transaction = await web3.Eth.GetEtherTransferService()
                    .TransferEtherAndWaitForReceiptAsync("0xc3202910De95C5B34c43B39E8178b35D4b45c8bB", ethValue);
                return RedirectToAction("PaymentCheck");
            }
            else
            {
                return RedirectToAction("PaymentError");
            }

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
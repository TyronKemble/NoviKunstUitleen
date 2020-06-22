using NoviKunstUitleen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nethereum.Web3;
using System.Threading.Tasks;
using NoviKunstUitleen.ViewModel;
using Nethereum.Hex.HexTypes;
using System.Numerics;
using Nethereum.Web3.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3.Accounts.Managed;
using System.Net.Http;

namespace NoviKunstUitleen.Controllers
{
    public class CryptoPaymentController : Controller
    {
        private ApplicationDbContext _context;
        private readonly string web3Url = "https://ropsten.infura.io/v3/cc7d9b90b1964ada9f24467dc0f0f780";
        private readonly string bankETHAddress = "0x0351e08Aca83B56958c720d4AE1AdCcF8a3807c3";
        private readonly string privateETHSender = "54b95bf755be54758e9617d636bc745215f29f14e08fa6bbc0ab307b652a6c3a";
        private readonly string depositToRopsten = "https://faucet.ropsten.be/donate/";

        public CryptoPaymentController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: CryptoPayment
        public async Task<ActionResult>  Index(string Username)
        {
            // get current user
            var user = _context.Users.FirstOrDefault(c => c.UserName == Username);

            // Web3 koppeling maken
            var web3 = new Web3(web3Url);

            // Get users current balance
            var balance = await web3.Eth.GetBalance.SendRequestAsync(user.CryptoWallet);

            // Get current ETH value 
            var etherAmount = Web3.Convert.FromWei(balance.Value);

            var results = new CryptoViewModel
            {
                UserName = user.Email,
                CryptoWallet = etherAmount
            };

            return View(results);
        }

        public async Task <ActionResult> Deposit(string Username)
        {
            // Get current username
            var user = _context.Users.FirstOrDefault(c => c.UserName == Username);

            // Create account for current user. 
            var account = new Account(privateETHSender);

            //Now let's create an instance of Web3 using our account pointing to our nethereum testchain
            var web3 = new Web3(account, web3Url);

                                          
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            // Get balance of user to deposit.
            var balance = await web3.Eth.GetBalance.SendRequestAsync(user.CryptoWallet);

            // Get eth from Ropsten test network
            var url = depositToRopsten + user.CryptoWallet;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return View("false");
            }

            // Get current ETH after deposit from Ropsten.
            balance = await web3.Eth.GetBalance.SendRequestAsync(user.CryptoWallet);
            var BalanceAfterDeposit = Web3.Convert.FromWei(balance.Value);


            var results = new CryptoViewModel
            {
                UserName = user.Email,
                CryptoWallet = BalanceAfterDeposit
            };

            return RedirectToAction("Index", results);
        }


        public async Task<ActionResult> WithDraw(decimal withDraw)
        {
            // Get ETH private ETH adres of Sender

            var account = new Account(privateETHSender);
            //create an instance of Web3 using our account pointing to our nethereum testchain
            var web3 = new Web3(account, web3Url);

            // Get current user
            var currentUser = _context.Users.FirstOrDefault(c => c.UserName == User.Identity.Name);

            // Check the balance of the account we are going to send the Ether from 
            var balanceFromSender = await web3.Eth.GetBalance.SendRequestAsync(currentUser.CryptoWallet);

            if (Web3.Convert.FromWei(balanceFromSender.Value) > withDraw)
            {
                // Lets transfer 1.11 Ether
                var transaction = await web3.Eth.GetEtherTransferService()
                    .TransferEtherAndWaitForReceiptAsync(bankETHAddress, withDraw);

                var BalanceAfterDeposit = Web3.Convert.FromWei(balanceFromSender.Value);
                var results = new CryptoViewModel
                {
                    UserName = currentUser.Email,
                    CryptoWallet = BalanceAfterDeposit
                };

                return RedirectToAction("Index", results);
            }
            else
            {
                return View();
            }

        }
    }
}
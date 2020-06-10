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


            // hier komt balance aanvraag met de gekozen naam op filter
            var user = _context.Users.FirstOrDefault(c => c.UserName == Username);
            // provide user Eth adres as param user.CryptoWallet

            // filter de juiste gebruiker 

            var web3 = new Web3("https://ropsten.infura.io/v3/cc7d9b90b1964ada9f24467dc0f0f780");
            // hier komt balance aanvraag met de gekozen naam op filter
            var balance = await web3.Eth.GetBalance.SendRequestAsync(user.CryptoWallet);
            // Komt in de view 
            var etherAmount = Web3.Convert.FromWei(balance.Value);

            // viewModel 

            var results = new CryptoViewModel
            {
                UserName = user.Email,
                CryptoWallet = etherAmount
            };

            return View(results);

        }

        public async Task <ActionResult> Deposit(string Username)
        {
            // hier komt balance aanvraag met de gekozen naam op filter
            var user = _context.Users.FirstOrDefault(c => c.UserName == Username);
            // provide user Eth adres as param user.CryptoWallet

            // Save private key in enviroment variable - Zoek uit
            var privateKey = "54b95bf755be54758e9617d636bc745215f29f14e08fa6bbc0ab307b652a6c3a";
            var account = new Account(privateKey);
            //Now let's create an instance of Web3 using our account pointing to our nethereum testchain
            var web3 = new Web3(account, "https://ropsten.infura.io/v3/cc7d9b90b1964ada9f24467dc0f0f780");

            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            // Check the balance of the account we are going to send the Ether
            var balance = await web3.Eth.GetBalance.SendRequestAsync(user.CryptoWallet);
            var url = "https://faucet.ropsten.be/donate/" + user.CryptoWallet;
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return View("false");
            }
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

            //https://stackoverflow.com/questions/185208/how-do-i-get-and-set-environment-variables-in-c
            // https://faucet.ropsten.be/ - Eth

            // ETH adres van degene die zend
            var privateKey = "54b95bf755be54758e9617d636bc745215f29f14e08fa6bbc0ab307b652a6c3a";
            var account = new Account(privateKey);
            //Now let's create an instance of Web3 using our account pointing to our nethereum testchain
            var web3 = new Web3(account, "https://ropsten.infura.io/v3/cc7d9b90b1964ada9f24467dc0f0f780");

            var currentUser = _context.Users.FirstOrDefault(c => c.UserName == User.Identity.Name);

            // Create central bank crypto 
            var bankAdres = "0x0351e08Aca83B56958c720d4AE1AdCcF8a3807c3";

            // Convert total value to eth WITH API
            decimal ethValue = withDraw;
            // Send to ETH adres 



            // Check the balance of the account we are going to send the Ether
            var balanceFromSender = await web3.Eth.GetBalance.SendRequestAsync(currentUser.CryptoWallet);

            if (Web3.Convert.FromWei(balanceFromSender.Value) > ethValue)
            {
                // Lets transfer 1.11 Ether
                var transaction = await web3.Eth.GetEtherTransferService()
                    .TransferEtherAndWaitForReceiptAsync(bankAdres, ethValue);

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
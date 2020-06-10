using NoviKunstUitleen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web;


namespace NoviKunstUitleen.ViewModel
{
    public class CryptoViewModel
    {

        public string UserName { get; set; }

        public decimal CryptoWallet { get; set; }

        public string SendEthToAdress { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalEuroValue { get; set; }


    }
}
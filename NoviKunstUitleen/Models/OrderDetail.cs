﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoviKunstUitleen.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ArtsId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string EndOfLoan { get; set; }

        public virtual Arts Arts { get; set; }
        public virtual Order Order { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoviKunstUitleen.Models
{
    public class ArtLendPeriod
    {
        [Key]
        public int ArtLendPeriod_Id { get; set; }
        public int? ArtsId { get; set; }
        public virtual Arts Arts { get; set; }
        public string EndOfLend { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoviKunstUitleen.Models
{
    public class Arts
    {
        public int ArtsId { get; set; }
        [Required(ErrorMessage = "Please enter Art Name"), MaxLength(20)]
        public string Name { get; set; }
        [DisplayName("Upload Art")]
        public string Image { get; set; }

        //public byte[] Images { get; set; }
        [Required(ErrorMessage = "Please enter Art price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter Number In Stock")]
        public int NumbersInStock { get; set; }
        [Required(ErrorMessage = "Please enter Numbers Available")]
        public int NumbersAvailable { get; set; }
        [Required]
        [Display(Name = "Date")]
        public string DateAdded { get; set; }
        [Required]
        public string Creator { get; set; }
    }
}
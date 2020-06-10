using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoviKunstUitleen.Models;

namespace NoviKunstUitleen.ViewModel
{
    public class NewArtsFormViewModel
    {
        public Arts Art { get; set; }

        public string Title
        {
            get
            {
                return  Art.ArtsId != 0 ? "Edit Art" : "New Art";
            }
        }
    }
}
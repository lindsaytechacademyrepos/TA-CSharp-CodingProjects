using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractical.ViewModels
{
    public class QuoteVM
    {
        
        public string quotedPrice { get; set; }

        public QuoteVM(decimal quotedPrice)
        {
            this.quotedPrice = String.Format("{0:C2}",quotedPrice);
        }

    }
}
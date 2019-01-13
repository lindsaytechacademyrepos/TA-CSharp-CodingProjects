using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractical.Models
{
    public class QuoteSubmission
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public DateTime dob { get; set; }
        public int carYear { get; set; }
        public string carMake { get; set; }
        public string carModel { get; set; }
        public int tickets { get; set; }
        public bool dui { get; set; }
        public bool fullCoverage { get; set; }
        public decimal quotedPrice { get; set; }
    }
}
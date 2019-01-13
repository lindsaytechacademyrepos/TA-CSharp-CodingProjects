using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractical.ViewModels
{
    public class AdminVM
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DOB { get; set; }
        public int CarYear { get; set; }
        public string CarModel { get; set; }
        public string CarMake { get; set; }
        public bool Dui { get; set; }
        public int tickets { get; set; }
        public bool fullCoverage { get; set; }
        public string quotedPrice { get; set; }

        public AdminVM()
        {

        }
    }
}
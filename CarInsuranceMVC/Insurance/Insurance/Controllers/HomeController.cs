using Insurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public double getQuote(Insuree insuree)
        {
            int baseRate = 50;

            //calculate ageRate
            DateTime dob = insuree.DateOfBirth;
            int ageRate = 0;
            if (DateTime.Now.Year - dob.Year < 18)
            {
                ageRate = 100;
            }
            if (DateTime.Now.Year - dob.Year < 25 || DateTime.Now.Year - dob.Year > 100)
            {
                ageRate = 25;
            }

            //calculate carModel variables
            int carYearRate = 0;
            if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
            {
                carYearRate = 25;
            }
            int carMakeRate = 0;
            if (insuree.CarMake == "Porsche")
            {
                carMakeRate = 25;
            }
            if (insuree.CarMake == "Porsche" && insuree.CarModel == "911 Carrera")
            {
                carMakeRate = 50;
            }

            //speeding ticket variables
            int speedingTicketRate = insuree.SpeedingTickets * 10;

            //Base Quote
            double quote = baseRate + ageRate + carYearRate + carMakeRate + speedingTicketRate;

            //DUI
            if (insuree.DUI)
            {
                quote = quote * 1.25;
            }

            //Coverage Level
            if (insuree.CoverageLevel)
            {
                quote = quote * 1.5;
            }

            return quote;
        }
    }
}
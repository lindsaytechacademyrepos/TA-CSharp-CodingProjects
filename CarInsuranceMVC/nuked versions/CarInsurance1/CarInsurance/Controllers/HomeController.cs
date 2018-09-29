using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;


namespace CarInsurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(string FirstName, string LastName, string EmailAddress, DateTime DateOfBirth, 
                            int CarYear, string CarMake, string CarModel, bool yesDUI, int speedingTickets, bool FullCoverage)
        {
            using (InsuranceEntities db = new InsuranceEntities())
            {
                //quote calculations
                int baserate = 50;
                int age = DateTime.Now.Year - DateOfBirth.Year;
                int ageRate = 0;
                if (age< 18)
                {
                    ageRate = 100;
                }
                if (age < 25 || age > 100)
                {
                    ageRate = 25;
                }
                int carYearRate = 0;
                if (CarYear < 200 || carYearRate > 2015)
                {
                    carYearRate = 25;
                }
                int carMakeRate = 0;
                if (CarMake == "Porsche")
                {
                    carMakeRate = 25;
                }
                if (CarMake =="Porshe" && CarModel =="911 Carrera")
                {
                    carMakeRate = 50;
                }
                int speedingTicketRate = speedingTickets * 10;
                double quote = baserate + ageRate+ carYearRate +carMakeRate + speedingTicketRate;
                if (yesDUI)
                {
                    quote = quote * 1.25;
                }
                if (FullCoverage)
                {
                    quote = quote * 1.5;
                }

                var insuree = new Insuree();
                insuree.FirstName = FirstName;
                insuree.LastName = LastName;
                insuree.EmailAddress = EmailAddress;
                insuree.DateOfBirth = DateOfBirth;
                insuree.CarYear = CarYear;
                insuree.CarMake = CarMake;
                insuree.CarModel = CarModel;
                insuree.DUI = yesDUI;
                insuree.SpeedingTicket = speedingTickets;
                insuree.CoverageType = FullCoverage;
                insuree.Quote = Convert.ToDecimal(quote);

                db.Insurees.Add(insuree);
                db.SaveChanges();

                ViewBag.Message = "Your request has been submitted";
                return View();
            }
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


    }
}
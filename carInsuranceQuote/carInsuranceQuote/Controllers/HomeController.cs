using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace carInsuranceQuote.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult GetQuote(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake, string carModel, bool dui, int speedingTickets, string coverageLevel)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                string queryString = @"INSERT INTO ApplicantDatas (FirstName, LastName, EmailAddress, DateOfBirth, CarYear, CarMake, CarModel, DUI, SpeedingTickets, CoverageLevel) 
                                    VALUES (@firstName, @lastName, @emailAddress, @dateOfBirth, @carYear, @carMake, @carModel, @)";

                using (carInsuranceQuoteEntities db = new carInsuranceQuoteEntities())
                {
                    var quoteRequest = new applicantData();
                    quoteRequest.FirstName = firstName;
                    quoteRequest.LastName = lastName;
                    quoteRequest.EmailAddress = emailAddress;
                    quoteRequest.DateOfBirth = dateOfBirth;


                }
            }
        }
      
    }
}
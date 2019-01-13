using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPractical.Models
{
    public abstract class QuoteCalculator
    {
        
        public static decimal calculateQuote(QuoteSubmission application)
        {
           decimal quotePrice = 50.00m;


            //If the user is under 25, add $25 to the monthly total.
            if (getAge(application) < 25)
                quotePrice += 25;

            //If the user is under 18, add $100 to the monthly total.
            if (getAge(application) < 18)
                quotePrice += 100;

            //If the user is over 100, add $25 to the monthly total.
            if (getAge(application) > 100)
                quotePrice += 25;

            //If the car's year is before 2000, add $25 to the monthly total.
            if(application.carYear < 2000)
                quotePrice += 25;

            //If the car's year is after 2015, add $25 to the monthly total.
            if (application.carYear > 2015)
                quotePrice += 25;

            //If the car's Make is a Porsche, add $25 to the price.
            if (application.carMake == "Porsche")
                quotePrice += 25;


            //If the car's Make is a Porsche and its model is a 911 Carrera, add an additional $25 to the price.
            if(application.carModel == "911 Carrera" && application.carMake == "Porsche")
                quotePrice += 25;


            //Add $10 to the monthly total for every speeding ticket the user has.
            quotePrice += 10 * application.tickets;

            //If the user has ever had a DUI, add 25% to the total.
            if (application.dui)
                quotePrice += quotePrice * .25m;

            //If it's full coverage, add 50% to the total.
            if (application.fullCoverage)
                quotePrice += quotePrice * .5m;

            return quotePrice;

        }

        //get age of someone using their dob
        public static int getAge(QuoteSubmission app)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - app.dob.Year;
            // Go back to the year the person was born in case of a leap year
            if (app.dob > today.AddYears(-age))
                age--;

            return age;
        }

        
    }
}
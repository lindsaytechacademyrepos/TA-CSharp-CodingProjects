/*A mock MVC application for Step 214 of C# and .NET*/
/*Author: Colby Lee*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using MVCPractical.Models;
using MVCPractical.ViewModels;

namespace MVCPractical.Controllers
{
    public class HomeController : Controller
    {

        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=db_autoquotes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Quote(string firstName, string lastName, string emailAddress, DateTime dob, int carYear, string carMake, string carModel, int tickets, Boolean fullCoverage = false, Boolean dui = false)
        {
            QuoteSubmission quoteApp = new QuoteSubmission();
            quoteApp.firstName = firstName;
            quoteApp.lastName = lastName;
            quoteApp.emailAddress = emailAddress;
            quoteApp.dob = dob;
            quoteApp.carYear = carYear;
            quoteApp.carMake = carMake;
            quoteApp.carModel = carModel;
            quoteApp.dui = dui;
            quoteApp.tickets = tickets;
            quoteApp.fullCoverage = fullCoverage;


            
            //Make sure all necessary fields are filled
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || dob.Equals(null) || string.IsNullOrEmpty(carMake) || string.IsNullOrEmpty(carModel))
            {
                return View("/Views/Shared/Error.cshtml");
            }

            //Check that they aren't trying to cheat the system with negative values
            if(tickets < 0 || carYear < 1800)
            {
                return View("/Views/Shared/Error.cshtml");
            }

            //calculate quote with submission
            else
            {
                quoteApp.quotedPrice = QuoteCalculator.calculateQuote(quoteApp);
                
                string queryString = @"INSERT INTO quotes (firstName, lastName, emailAddress, dob, carYear, carMake, carModel, dui, tickets, fullCoverage, quotedPrice) VALUES
                                       (@firstName, @lastName, @emailAddress, @dob, @carYear, @carMake, @carModel, @dui, @tickets, @fullCoverage, @quotedPrice)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@firstName", SqlDbType.VarChar);
                    command.Parameters.Add("@lastName", SqlDbType.VarChar);
                    command.Parameters.Add("@emailAddress", SqlDbType.VarChar);
                    command.Parameters.Add("@dob", SqlDbType.Date);
                    command.Parameters.Add("@carYear", SqlDbType.Int);
                    command.Parameters.Add("@carMake", SqlDbType.VarChar);
                    command.Parameters.Add("@carModel", SqlDbType.VarChar);
                    command.Parameters.Add("@dui", SqlDbType.Bit);
                    command.Parameters.Add("@tickets", SqlDbType.Int);
                    command.Parameters.Add("@fullCoverage", SqlDbType.Bit);
                    command.Parameters.Add("@quotedPrice", SqlDbType.Decimal);


                    command.Parameters["@firstName"].Value = firstName;
                    command.Parameters["@lastName"].Value = lastName;
                    command.Parameters["@emailAddress"].Value = emailAddress;
                    command.Parameters["@dob"].Value = dob;
                    command.Parameters["@carYear"].Value = carYear;
                    command.Parameters["@carMake"].Value = carMake;
                    command.Parameters["@carModel"].Value = carModel;
                    command.Parameters["@dui"].Value = dui;
                    command.Parameters["@tickets"].Value = tickets;
                    command.Parameters["@fullCoverage"].Value = fullCoverage;
                    command.Parameters["@quotedPrice"].Value = Math.Round(quoteApp.quotedPrice,2);




                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return View(new QuoteVM(quoteApp.quotedPrice));
            }
        }

        public ActionResult Admin()
        {
            string queryString = @"SELECT id, firstName, lastName, emailAddress, dob, carYear, carMake, carModel, dui, tickets, fullCoverage, quotedPrice FROM quotes";
            List<QuoteSubmission> submissions = new List<QuoteSubmission>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var submission = new QuoteSubmission();
                    submission.id = Convert.ToInt32(reader["id"]);
                    submission.firstName = reader["firstName"].ToString();
                    submission.lastName = reader["lastName"].ToString();
                    submission.emailAddress = reader["emailAddress"].ToString();
                    submission.dob = Convert.ToDateTime(reader["dob"]).Date;
                    submission.carYear = Convert.ToInt32(reader["carYear"]);
                    submission.carMake = reader["carMake"].ToString();
                    submission.carModel = reader["carModel"].ToString();
                    submission.dui = Convert.ToBoolean(reader["dui"]);
                    submission.tickets = Convert.ToInt32(reader["tickets"]);
                    submission.fullCoverage = Convert.ToBoolean(reader["fullCoverage"]);
                    submission.quotedPrice = Math.Round(Convert.ToDecimal(reader["quotedPrice"]),2);



                    submissions.Add(submission);
                }
            }

            var adminVms = new List<AdminVM>();
            foreach (var signup in submissions)
            {
                var adminVm = new AdminVM();
                adminVm.ID = signup.id;
                adminVm.FirstName = signup.firstName;
                adminVm.LastName = signup.lastName;
                adminVm.EmailAddress = signup.emailAddress;
                adminVm.DOB = signup.dob.Date;
                adminVm.CarYear = signup.carYear;
                adminVm.CarMake = signup.carMake;
                adminVm.CarModel = signup.carModel;
                adminVm.Dui = signup.dui;
                adminVm.tickets = signup.tickets;
                adminVm.fullCoverage = signup.fullCoverage;
                adminVm.quotedPrice = Convert.ToString(signup.quotedPrice);

                adminVms.Add(adminVm);
            }

            return View(adminVms);
        }
    }
}
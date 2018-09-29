using Newsletter_App_MVC_FA.Models;
using Newsletter_App_MVC_FA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newsletter_App_MVC_FA.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                //var signups = db.SignUps.Where(x=> x.Removed == null).ToList();
                var signups = (from c in db.SignUps
                               where c.Removed == null
                               select c);
                var signupVMs = new List<SignupVM>();

                foreach (var signup in signups)
                {
                    var signupVm = new SignupVM();
                    signupVm.Id = signup.Id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    signupVMs.Add(signupVm);

                }

                return View(signupVMs);
            }
        }

        public ActionResult Unsubscribe(int Id)
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                var signup = db.SignUps.Find(Id);
                signup.Removed = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
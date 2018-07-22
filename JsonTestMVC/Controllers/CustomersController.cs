using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JsonTestMVC.Models;

namespace JsonTestMVC.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers

        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult Index(Customer customer)
        {
            if (customer.Others == false || customer.SelfSend == false || customer.thirdParty == false)
                ModelState.AddModelError("Error", "Must select one option");

            return View();
        }


    }
}
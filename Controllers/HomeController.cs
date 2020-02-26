using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Kairos.Models;

// pattybranch added the below using statement


namespace Kairos.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        private User LoggedIn()
        {
            return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
        }
        private HomeContext dbContext;
        public HomeController(HomeContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View("Home");
        }

// Pattybranch added these for the stripe: New Method to take in 2 parameters
        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            // var customerService = new StripeCustomerService();
            // var chargeService = new StripeChargeService();

            // var customer = customerService.Create(new StripeCustomerCreateOptions 
            // {
            //     EmailTokenProvider = stripeEmail,
            //     SourceToken = stripeToken
            // });

            // var charge = chargeService.Create(new StripeChargeCreateOptions 
            // {
            //     Amount = 500, 
            //     MonitoringDescriptionAttribute = "",
            //     Currency = "usd",
            //     CustomerTaxIdDataOptions = customer.Id
            // });

            return View();
        }


        [HttpGet("shop")]
        public IActionResult Shop()
        {
            return View();
        }
        [HttpGet("results")]
        public IActionResult Results()
        {
            return View();
        }
        [HttpGet("shoppingcart")]
        public IActionResult ShoppingCart()
        {
            return View();
        }
        [HttpGet("watch")]
        public IActionResult ShowWatch()
        {
            return View("SHOW");
        }
    }
}

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
using Stripe;



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

// Pattybranch added these for the stripe: New Method to take in 2 parameters in order to process the payment
        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            List<Watch> UserCart = HttpContext.Session.GetObjectFromJson<List<Watch>>("UserCart");
            double CartAmount = 0;
            foreach (var w in UserCart)
            {
                CartAmount += w.Price;
            }
            var customer = customers.Create(new CustomerCreateOptions {
                Email = stripeEmail,
                Source= stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(CartAmount), 
                Description = "Test Payment",
                Currency = "usd",
                Customer = customer.Id
            });
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$444");
            Console.WriteLine(charge.Amount);

            if (charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return View();
            }
            else
            {
                
            }
            
            return View();
        }

        [HttpGet("shop")]
        public IActionResult Shop()
        {
            List<Watch> WatchInventory = dbContext.Watches.ToList();

            return View(WatchInventory);
        }
        [HttpGet("results")]
        public IActionResult Results(List<Watch> InventoryFilter)
        {   
            return View("Results", InventoryFilter);
        }
        [HttpGet("shoppingcart")]
        public IActionResult ShoppingCart()
        {
            if(HttpContext.Session.GetObjectFromJson<List<Watch>>("UserCart") != null)
            {
                List<Watch> Cart = HttpContext.Session.GetObjectFromJson<List<Watch>>("UserCart");
                Cart.ToList().ForEach( w => w.Price = Convert.ToInt64(w.Price) );
                foreach( var w in Cart)
                {
                    long price = Convert.ToInt64(w.Price);
                    w.Price = price;
                    Console.WriteLine(w.Price.GetType());
                }
                return View("ShoppingCart", Cart);
            }
            else
            {
                return View("Shop");
            }
        }
        [HttpGet("watch/{watchid}")]
        public IActionResult ShowWatch(int watchID)
        {
            Watch thisWatch = dbContext.Watches.FirstOrDefault(w => w.WatchId == watchID);
            return View("ShowWatch", thisWatch);
        }
        [HttpGet("addtocart/{watchid}")]
        public IActionResult AddToCart(int watchID)
        {
            Watch thisWatch = dbContext.Watches.FirstOrDefault(w => w.WatchId == watchID);
            if(HttpContext.Session.GetObjectFromJson<List<Watch>>("UserCart") == null)
            {
                Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
                List<Watch> Cart = new List<Watch>();
                Cart.Add(thisWatch);
                HttpContext.Session.SetObjectAsJson("UserCart", Cart);
                ViewBag.Cart = Cart;
                return RedirectToAction("ShoppingCart", "Home");
            }
            else if(HttpContext.Session.GetObjectFromJson<List<Watch>>("UserCart") != null)
            {
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                List<Watch> Cart = HttpContext.Session.GetObjectFromJson<List<Watch>>("UserCart");
                Cart.Add(thisWatch);
                HttpContext.Session.SetObjectAsJson("UserCart", Cart);
                ViewBag.Cart = Cart;
                return RedirectToAction("ShoppingCart", "Home");
            }
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            return View("ShowWatch/{watchId}", "Home");
        }
        [HttpGet("remove/{watchId}")]
        public IActionResult Remove(int watchID)
        {
            Watch thisWatch = dbContext.Watches.FirstOrDefault(w => w.WatchId == watchID);
            if(HttpContext.Session.GetObjectFromJson<List<Watch>>("UserCart") != null)
            {
                List<Watch> Cart = HttpContext.Session.GetObjectFromJson<List<Watch>>("UserCart");
                var itemToRemove = Cart.Single(w => w.WatchId == watchID);
                Cart.Remove(itemToRemove);
                HttpContext.Session.SetObjectAsJson("UserCart", Cart);
                ViewBag.Cart = Cart;
                return RedirectToAction("ShoppingCart", "Home");
            }
            else
            {
                return RedirectToAction("ShoppingCart", "Home");
            }
        }

        [HttpGet("lowhigh")]
        public IActionResult PriceLowHigh()
        {
            ViewBag.Results = dbContext.Watches.OrderBy(w => w.Price).ToList();
            return View("Results");
        }
        [HttpGet("highlow")]
        public IActionResult PriceHighLow()
        {
            ViewBag.Results = dbContext.Watches.OrderByDescending(w => w.Price).ToList();
            return View("Results");
        }
        [HttpGet("colorGold")]
        public IActionResult ColorGold()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Color == "Gold").ToList();
            return View("Results");
        }
        [HttpGet("colorBlack")]
        public IActionResult ColorBlack()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Color == "Black").ToList();
            return View("Results");
        }
        [HttpGet("colorSilver")]
        public IActionResult ColorSilver()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Color == "Silver").ToList();
            return View("Results");
        }
        [HttpGet("colorRoseGold")]
        public IActionResult ColorRoseGold()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Color == "Rose Gold").ToList();
            return View("Results");
        }
        [HttpGet("filterMens")]
        public IActionResult FilterMens()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Gender == "Men").ToList();
            return View("Results");
        }
        [HttpGet("filterWomens")]
        public IActionResult FilterWomens()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Gender == "Women").ToList();
            return View("Results");
        }
        [HttpGet("filterUnisex")]
        public IActionResult FilterUnisex()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Gender == "Unisex").ToList();
            return View("Results");
        }
        [HttpGet("filterSmall")]
        public IActionResult FilterSmall()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Size == "Small").ToList();
            return View("Results");
        }
        [HttpGet("filterMedium")]
        public IActionResult FilterMedium()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Size == "Medium").ToList();
            return View("Results");
        }
        [HttpGet("filterLarge")]
        public IActionResult FilterLarge()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Size == "Large").ToList();
            return View("Results");
        }
        [HttpGet("materialGold")]
        public IActionResult MaterialGold()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Material == "Gold").ToList();
            return View("Results");
        }
        [HttpGet("materialTitanium")]
        public IActionResult MaterialTitanium()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Material == "Titanium").ToList();
            return View("Results");
        }
        [HttpGet("materialStainlessSteel")]
        public IActionResult MaterialStainlessSteel()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Material == "Stainless Steel").ToList();
            return View("Results");
        }
        [HttpGet("materialCarbonFiber")]
        public IActionResult MaterialCarbonFiber()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Material == "Carbon Fiber").ToList();
            return View("Results");
        }
        [HttpGet("materialCeramic")]
        public IActionResult MaterialCeramic()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Material == "Ceramic").ToList();
            return View("Results");
        }
        [HttpGet("materialRubberSilicone")]
        public IActionResult MaterialRubberSilicone()
        {
            ViewBag.Results = dbContext.Watches.Where(w => w.Material == "Rubber/Silicone").ToList();
            return View("Results");
        }

    }
}

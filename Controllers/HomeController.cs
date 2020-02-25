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
            return View();
        }
        [HttpGet("watch")]
        public IActionResult ShowWatch()
        {
            return View();
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
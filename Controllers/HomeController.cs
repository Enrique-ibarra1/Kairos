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
            return View();
        }
        
    }
}
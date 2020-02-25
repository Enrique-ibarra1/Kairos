using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kairos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Kairos.Controllers
{
    public class LoginController : Controller
    {
        [Route("login")]
        private User LoggedIn()
        {
            return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
        }
        private HomeContext dbContext;
        public LoginController(HomeContext context)
        {
            dbContext = context;
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("login");
        }
        [HttpPost("registeruser")]
        public IActionResult RegisterUser(User register)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == register.Email))
                {
                    ModelState.AddModelError("Email", "Email has already been taken");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                register.Password = Hasher.HashPassword(register, register.Password);
                dbContext.Users.Add(register);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId", register.UserId);
                return RedirectToAction("Index", "Home");

            }
            else 
            {
                return View("Register");
            }
        }

        [HttpGet("Success")]
        public IActionResult Success ()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("Logout");
            }
            return View(userInDb);
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost("signin")]
        public IActionResult SignIn(LoginUser login)
        {
            if(ModelState.IsValid)
            {
                User dbUser = dbContext.Users.FirstOrDefault(u => u.Email == login.LoginEmail);
                if(dbContext.Users.Any(u => u.Email != login.LoginEmail))
                {
                    if(dbUser == null)
                    {
                        ModelState.AddModelError("LoginEmail", "Email or password not found");
                        return View("Login");
                    }
                }
                PasswordHasher<LoginUser> Hash = new PasswordHasher<LoginUser>();
                var result = Hash.VerifyHashedPassword(login, dbUser.Password, login.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Email or password not found");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("UserId", dbUser.UserId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }          
        }
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            User userInDb = LoggedIn();
            if(userInDb == null)
            {
                return RedirectToAction("Logout", "Login");
            }
            if(userInDb.Email != "email@email.com")
            {
                return RedirectToAction("Logout", "Login");
            }
            return View();
        }
        [HttpPost("createwatch")]
        public IActionResult CreateWatch(Watch formWatch)
        {
            dbContext.Watches.Add(formWatch);
            dbContext.SaveChanges();
            return RedirectToAction("Admin");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloEF.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace HelloEF.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {

            return View();
        }

        // Create
        [HttpPost("create")]
        public IActionResult Create(LogRegModel model)
        {
            Owner owner = model.Register;

            if(ModelState.IsValid)
            {
                if(dbContext.Owners.Any(o => o.Email == owner.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("Index");
                }

                PasswordHasher<Owner> hasher = new PasswordHasher<Owner>();
                owner.Password = hasher.HashPassword(owner, owner.Password);

                var newOwner = dbContext.Owners.Add(owner).Entity;
                dbContext.SaveChanges();

                HttpContext.Session.SetInt32("userId", newOwner.OwnerId);

                return RedirectToAction("Index", "Owner");
            }
            
            return View("Index");
        }
        [HttpPost("login")]
        public IActionResult Login(LogRegModel model)
        {
            LoginUser user = model.Login;
            if(ModelState.IsValid)
            {
                Owner toLogin = dbContext.Owners.FirstOrDefault(u => u.Email == user.EmailAttempt);
                if(toLogin == null)
                {
                    ModelState.AddModelError("Login.EmailAttempt", "Invalid Email/Password");
                    return View("Index");
                }
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, toLogin.Password, user.PasswordAttempt);
                if(result == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError("Login.EmailAttempt", "Invalid Email/Password");
                    return View("Index");
                }
                // Log user into session
                HttpContext.Session.SetInt32("userId", toLogin.OwnerId);
                return RedirectToAction("Index", "Owner");
            }
            return View("Index");
        }
        [HttpGet("logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

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
    [Route("owner")]
    public class OwnerController : Controller
    {
        private MyContext dbContext;
        public OwnerController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            // Owner that has walked the most dogs
            var maxWalks = dbContext.Owners
                .Include(o => o.WalksTaken)
                .Max(o => o.WalksTaken.Count);
            
            var mostActiveWalker = dbContext.Owners
                .Include(o => o.WalksTaken)
                .ThenInclude(w => w.WalkedDog)
                .FirstOrDefault(o => o.WalksTaken.Count == maxWalks);


            return View(dbContext.Owners.Include(o => o.Dogs).ToList());
        }

        // Show
        // localhost:5000/10
        [HttpGet("{ownerId}")]
        public IActionResult Show(int ownerId)
        {
            // Get One Dog by id
            Owner model = dbContext.Owners
                .Include(o => o.WalksTaken)
                    .ThenInclude(w => w.WalkedDog)
                .FirstOrDefault(d => d.OwnerId == ownerId);

            // get all walks associated with with owner, join dogs

            // if dog doesn't exist
            if(model == null)
                return RedirectToAction("Index");
            

            return View(model);
        }
        
        // Update
        [HttpPost("update/{ownerId}")]    
        public IActionResult Update(Owner owner, int ownerId)
        {
            if(ModelState.IsValid)
            {
                // Update a Owner
                
                // query for Owner with id
                Owner OwnerToUpdate = dbContext.Owners.FirstOrDefault(o => o.OwnerId == ownerId);
                // update the things we want
                OwnerToUpdate.FirstName = owner.FirstName;
                OwnerToUpdate.LastName = owner.LastName;
                OwnerToUpdate.DOB = owner.DOB;
                OwnerToUpdate.Email = owner.Email;
            
                // save changes
                dbContext.SaveChanges();



               
                return RedirectToAction("Index");
            }
            
            return View("Show");
        }
        // Delete
        [HttpGet("delete/{ownerId}")]
        public IActionResult Delete(int ownerId)
        {
            
            Owner toDelete = dbContext.Owners.FirstOrDefault(o => o.OwnerId == ownerId);
            // remove it
            dbContext.Owners.Remove(toDelete);
            // save changes
            dbContext.SaveChanges();
          
            return RedirectToAction("Index");
        }
    }
}

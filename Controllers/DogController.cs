using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloEF.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HelloEF.Controllers
{
    [Route("dog")]
    public class DogController : Controller
    {
        private MyContext dbContext;
        public DogController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View(dbContext.Dogs.Include(dog => dog.Owner).Take(10).ToList());
        }
        [HttpGet("new")]
        public IActionResult New()
        {
            ViewBag.Owners = dbContext.Owners.Take(10);
            return View();
        }
        // Show
        // localhost:5000/10
        [HttpGet("{dogId}")]
        public IActionResult Show(int dogId)
        {
            // Get One Dog by id
            Dog model = dbContext.Dogs.Include(d => d.Owner).FirstOrDefault(d => d.DogId == dogId);

            // if dog doesn't exist
            if(model == null)
                return RedirectToAction("Index");
            
            // grab all owners that isn't the current owner
            var otherOwners = dbContext.Owners
                // owners whos not present model's owner
                .Where(owner => owner.OwnerId != model.OwnerId);

            // though maybe not great UX :P, just going to grab all owners for this dropdow
            ViewBag.OtherOwners = dbContext.Owners.Take(10);

            return View(model);
        }
        // Create
        [HttpPost("create")]
        public IActionResult Create(Dog dog)
        {
            if(ModelState.IsValid)
            {
                // check for unique Name/Breed
                if(dbContext.Dogs.Any(doggie => doggie.Name == dog.Name && doggie.Breed == dog.Breed))
                {
                    ModelState.AddModelError("Name", "Name exists already!");
                    ViewBag.Dogs = dbContext.Dogs.Take(10);
                    return View("Index");
                }

                // Create a dog
                dbContext.Dogs.Add(dog);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            
            return View("Index");
        }
        // Update
        [HttpPost("update/{dogId}")]    
        public IActionResult Update(Dog Dog, int dogId)
        {
            if(ModelState.IsValid)
            {
                // Update a Dog
                
                // query for Dog with id
                Dog dogToUpdate = dbContext.Dogs.FirstOrDefault(d => d.DogId == dogId);
                // update the things we want
                dogToUpdate.Breed = Dog.Breed;
                dogToUpdate.Name = Dog.Name;
                dogToUpdate.Weight = Dog.Weight;
                dogToUpdate.OwnerId = Dog.OwnerId;
                // save changes
                dbContext.SaveChanges();



               
                return RedirectToAction("Index");
            }
            ViewBag.Dogs = dbContext.Dogs.Take(10);
            return View("Show");
        }
        // Delete
        [HttpGet("delete/{dogId}")]
        public IActionResult Delete(int dogId)
        {
            // delete a Dog
            // query for Dog with id
            Dog dogToDelete = dbContext.Dogs.FirstOrDefault(d => d.DogId == dogId);
            // remove it
            dbContext.Dogs.Remove(dogToDelete);
            // save changes
            dbContext.SaveChanges();
          
            return RedirectToAction("Index");
        }
    }
}

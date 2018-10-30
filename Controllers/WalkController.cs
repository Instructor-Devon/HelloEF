using System.Linq;
using HelloEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace HelloEF
{
    [Route("walk")]
    public class WalkController : Controller
    {
        private MyContext dbContext;
        public WalkController(MyContext context)
        {
            dbContext = context;
        }
        [Route("new")]
        public IActionResult New()
        {
            ViewBag.Dogs = dbContext.Dogs.ToList();
            ViewBag.User = HttpContext.Session.GetInt32("userId");
            return View();
        }
        [HttpPost("create")]
        public IActionResult Create(Walk walk)
        {
            if(ModelState.IsValid)
            {
                // kind of hacky... would be better with a datetime picker (bootstrap, etc)
                walk.StartTime = walk.StartTime.Add(walk.Time.TimeOfDay);
                dbContext.Walks.Add(walk);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Dog");
            }
            ViewBag.Dogs = dbContext.Dogs.ToList();
            ViewBag.User = HttpContext.Session.GetInt32("userId");
            return View("New");
        }
    }
}
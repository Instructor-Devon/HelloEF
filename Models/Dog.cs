using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloEF.Models
{
    public class Dog
    {
        [Key]
        public int DogId {get;set;}
        [Display(Name="Owned By")]
        public int OwnerId {get;set;}
        [Required]
        public string Name {get;set;}
        public string Breed {get;set;}
        public double Weight {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        
        // Helpful, but not in DB (no {get;set;})
        public int DaysSinceCreated
        {
            get
            {
                return (DateTime.Now - CreatedAt).Days;
            }
        }
        // Navigation Property (not in DB)
        // dbContext.Dogs.Include(dog => dog.Owner);
        public Owner Owner {get;set;}
        public List<Walk> WalksTaken {get;set;}
    }
    
}
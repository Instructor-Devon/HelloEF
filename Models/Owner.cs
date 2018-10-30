using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloEF.Models
{
    public class Owner
    {
        [Key]
        public int OwnerId {get;set;}
        [Display(Name="First Name")]
        [Required]
        public string FirstName {get;set;}
        [Required]
        [Display(Name="Last Name")]
        public string LastName {get;set;}
        [Required]
        [Display(Name ="Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB {get;set;}
        [EmailAddress]
        [Required]
        public string Email {get;set;}
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [Compare("Password")]
        [NotMapped]
        public string Confirm {get;set;}

        // Helpful, but not in DB (no {get;set;})
        public string FullName
        {
            get 
            {
                return $"{FirstName} {LastName}";
            } 
        }
        public int Age
        {
            get
            {
                return (DateTime.Now - DOB).Days / 365;
            }
        }
        
        // Navigation Property (not in DB)
        // dbContext.Owners.Include(own => own.Dogs).Where..//
        // dbContext.Owners.Include(own => own.Walks).ThenInclude(walk => walk.WalkedDog);
        public List<Dog> Dogs {get;set;}
        // WalksTaken.Count
        public List<Walk> WalksTaken {get;set;}
    }
    public class LoginUser
    {
        [EmailAddress]
        [Required]
        [Display(Name="Email")]
        public string EmailAttempt {get;set;}
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string PasswordAttempt {get;set;}
    }

}
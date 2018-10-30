using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloEF.Models
{
    public class Walk
    {
        [Key]
        public int WalkId {get;set;}
        public int DogId {get;set;}
        public int OwnerId {get;set;}
        [DataType(DataType.Date)]
        [Display(Name="Date")]
        // Validations??? 
        // Future??
        // Does it confict with other Walk events???
        public DateTime StartTime {get;set;}

        // kind of hacky... would be better with a datetime picker (bootstrap, etc)
        [NotMapped]
        [DataType(DataType.Time)]
        [Display(Name="Time")]
        
        public DateTime Time {get;set;}
        // IN MINTUES
        [Range(5, Int32.MaxValue, ErrorMessage="Duration must be greater than 5 minutes")]
        public int Duration {get;set;}
        public Dog WalkedDog {get;set;}
        public Owner Walker {get;set;}
    }
}
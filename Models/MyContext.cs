using Microsoft.EntityFrameworkCore;

namespace HelloEF.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) {}
        // Tables
        public DbSet<Dog> Dogs {get;set;}
        public DbSet<Owner> Owners {get;set;}
        public DbSet<Walk> Walks {get;set;}
    }
}
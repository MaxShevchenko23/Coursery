using System.Data.Common;
using Coursery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coursery.Infrastucture.Data;

public class CourseryDbContext : DbContext
{
    public CourseryDbContext() : base()
    {
        
    }


   public CourseryDbContext(DbContextOptions<CourseryDbContext> options) : base(options)
   {
       var optionsBuilder = new DbContextOptionsBuilder<CourseryDbContext>(options);
       
   }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       base.OnConfiguring(optionsBuilder);
       
       optionsBuilder.UseSqlite("Data Source=coursery.db");
   }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);
       
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseryDbContext).Assembly);
   }


   public DbSet<Enroll> Enrolls { get; set; }
   public DbSet<Course> Courses { get; set; }
   public DbSet<User> Users { get; set; }
   public DbSet<Module> Modules { get; set; }
   public DbSet<History> History { get; set; }

   public DbSet<Lesson> Lessons { get; set; }
   public DbSet<Review> Review { get; set; }
     
}
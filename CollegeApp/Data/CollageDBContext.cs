using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollageDBContext : DbContext
    {
        public CollageDBContext(DbContextOptions<CollageDBContext> options) : base(options)
        {

        }

        DbSet<Student> Students { get; set; }

        // Program.cs de servisi eklememiz gerekiyor

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student { 
                    Id = 1, 
                    SutudentName= "yunus",
                    Address="Türkiye",
                    Email="yunus@gmail.com",
                    DOB= new DateTime(2024,12,12)

                },
                new Student {
                    Id = 2,
                    SutudentName= "yusuf",
                    Address="Türkiye",
                    Email="yusuf@gmail.com",
                    DOB= new DateTime(2024,6,12)

                },
            });
        }

    }

}

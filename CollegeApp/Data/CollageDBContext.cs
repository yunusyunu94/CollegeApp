using CollegeApp.Data.Config;
using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollageDBContext : DbContext
    {
        public CollageDBContext(DbContextOptions<CollageDBContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        // Program.cs de servisi eklememiz gerekiyor


       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().HasData(new List<Student>()
            //{
            //    new Student { 
            //        Id = 1, 
            //        SutudentName= "yunus",
            //        Address="Türkiye",
            //        Email="yunus@gmail.com",
            //        DOB= new DateTime(2024,12,12)

            //    },
            //    new Student {
            //        Id = 2,
            //        SutudentName= "yusuf",
            //        Address="Türkiye",
            //        Email="yusuf@gmail.com",
            //        DOB= new DateTime(2024,6,12)

            //    },
            //});  // Elle veri girisi yaptik;

            // YUKARİDAKİ VERİ EKLEMEYİCE OLUSTURDUGUUZ DATA İCİNDE CONFİG DOSYASİNDAN EKLİYORUZ

            //modelBuilder.Entity<Student>(entity =>
            //{
            //    entity.Property(n => n.SutudentName).IsRequired();
            //    entity.Property(n => n.SutudentName).HasMaxLength(250);
            //    entity.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            //    entity.Property(n => n.Email).IsRequired().HasMaxLength(250);
            //});

            // YUKARİDAKİ KONFİGURASYANU DATA İCİNDE CONFİG DOSYASİNA TASİTİK


            // Table1
            modelBuilder.ApplyConfiguration(new StudentConfig());

        }

    }

}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(nameof(Student));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn(); // ID otamatik artsin

            builder.Property(n => n.SutudentName).IsRequired();
            builder.Property(n => n.SutudentName).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);



            // Elle veri girisi yaptik;
            builder.HasData(new List<Student>()
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

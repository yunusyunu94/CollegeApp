using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollageDBContext : DbContext
    {
        public CollageDBContext(DbContextOptions<CollageDBContext> options) : base(options)
        {

        }

        DbSet<Student> Students { get; set; } 
    }

    // Program.cs de servisi eklememiz gerekiyor
}

using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollageDBContext : DbContext
    {
        DbSet<Student> Students { get; set; }
    }
}

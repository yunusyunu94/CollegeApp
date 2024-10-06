namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>{
                new Student { Id = 1, SutudentName = "Venkat", Email = "studentemail1@gmail.com", Address ="Hyd, INDIA" },
                new Student { Id = 2, SutudentName = "Anil", Email = "studentemai2@gmail.com", Address ="Baglore, INDIA" }
            };

    }
}

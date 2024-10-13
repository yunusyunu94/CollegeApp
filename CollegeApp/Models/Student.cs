namespace CollegeApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string SutudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int Password { get; set; }
        public int ConfirmPassword { get; set; }
        public DateTime AdmissionDate { get; set; }
    }
}

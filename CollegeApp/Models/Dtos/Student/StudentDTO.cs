using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models.Dtos.Student
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required]
        public string SutudentName { get; set; }
        [EmailAddress]

        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
    }
}

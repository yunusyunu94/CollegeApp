using CollegeApp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models.Dtos.Student
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(100)]
        public string SutudentName { get; set; }

        [EmailAddress(ErrorMessage = "Please anter valid email address")]
        public string Email { get; set; }

        //[Range(10,20)]
        public int Age { get; set; }

        //[Required]
        public string Address { get; set; }
        //[Required]
        public int Password { get; set; }

        //[Required]
        [Compare(nameof(Password))]
        public int ConfirmPassword { get; set; }

        //[DateCheck]
        //public DateTime AdmissionDate { get; set; }
    }
}

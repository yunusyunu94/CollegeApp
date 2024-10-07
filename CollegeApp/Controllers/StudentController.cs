using CollegeApp.Models;
using CollegeApp.Models.Dtos.Students;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    // [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GeAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi

        public ActionResult<IEnumerable<StudentDTO>> GeStudents()
        {
            //var students = new List<StudentDTO>();
            //foreach (var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        SutudentName = item.SutudentName,
            //        Address = item.Address,
            //        Email = item.Email,
            //    };

            //    students.Add(obj); 
            //}


            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                SutudentName = s.SutudentName,
                Address = s.Address,
                Email = s.Email,
            });



            // Ok - 200 - Success
            return Ok(students);

        }

        [HttpGet]
        [Route("{İd:int}", Name = "GeStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi

        public ActionResult<StudentDTO> GeStudentById(int id)
        {
            // BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
            if (student == null)
                // NotFound - 404 - NotFound - Client error
                return NotFound($"The student with id {id} not fount");


            var studentsDTO = new StudentDTO()
            {
                Id=student.Id,
                SutudentName = student.SutudentName,
                Address = student.Address,
                Email = student.Email
            }; 


            // Ok - 200 - Success
            return Ok(studentsDTO);

        }

        [HttpGet("{name:alpha}", Name = "GeStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi

        public ActionResult<StudentDTO> GeStudentByName(string name)
        {
            // BadRequest - 400 - Badrequest - Client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Where(x => x.SutudentName == name).FirstOrDefault();

            if (student == null)
                // NotFound - 404 - NotFound - Client error
                return NotFound($"The student with name {name} not fount");


            var studentsDTO = new StudentDTO()
            {
                Id = student.Id,
                SutudentName = student.SutudentName,
                Address = student.Address,
                Email = student.Email
            };


            // Ok - 200 - Success
            return Ok(studentsDTO);

        }

        [HttpDelete("{İd:min(1):max(100)}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi

        public ActionResult<bool> DeleteStudent(int id)
        {
            // BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
                return BadRequest();


            var student = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
            if (student == null)
                // NotFound - 404 - NotFound - Client error
                return NotFound($"The student with id {id} not fount");

            CollegeRepository.Students.Remove(student);

            // Ok - 200 - Success
            return true;

        }
    }
}

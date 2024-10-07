using CollegeApp.Models;
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
        public ActionResult<IEnumerable<Student>> GeStudents()
        {
            // Ok - 200 - Success
            return Ok(CollegeRepository.Students);

        }

        [HttpGet]
        [Route("{İd:int}", Name = "GeStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK )]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi
        public ActionResult<Student> GeStudentById(int id)
        {
            // BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(x => x.Id == id).FirstOrDefault();
            if (student == null)
                // NotFound - 404 - NotFound - Client error
                return NotFound($"The student with id {id} not fount");


            // Ok - 200 - Success
            return Ok(student);

        }

        [HttpGet("{name:alpha}", Name = "GeStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi  
        public ActionResult<Student> GeStudentByName(string name)
        {
            // BadRequest - 400 - Badrequest - Client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Where(x => x.SutudentName == name).FirstOrDefault();

            if (student == null)
                // NotFound - 404 - NotFound - Client error
                return NotFound($"The student with name {name} not fount");

            // Ok - 200 - Success 
            return Ok(student);

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

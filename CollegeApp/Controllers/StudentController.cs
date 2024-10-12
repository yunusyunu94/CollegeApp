using CollegeApp.Models;
using CollegeApp.Models.Dtos.Student;
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
                Id = student.Id,
                SutudentName = student.SutudentName,
                Address = student.Address,
                Email = student.Email
            };


            // Ok - 200 - Success
            return Ok(studentsDTO); 

        }





        [HttpPost]
        [Route("Create")]
        // api/student/create
        [ProducesResponseType(StatusCodes.Status201Created)] // Kayıt olumlu 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model) // FromBody Sadece govde den almak istiyorum
        {
            /// Validationlari;
            
            // Yukaridaki [ApiController]  Yazmazsak " StudentDTO " yazdigimiz Validationlari calistirmak icin assagidaki
            // kontrolu yazmamiz lazim ;
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);


            ///




            if (model == null)
                return BadRequest();

            ///

            //// 1. Model duruma hata mesajı eklemek
            //if (model.AdmissionDate <= DateTime.Now)
            //{
            //    // 1. Model duruma hata mesajı eklemek
            //    ModelState.AddModelError("AdmissionDate error", "Admission date must be greater than or equal to todays date");
            //    return BadRequest(ModelState);

            //    // 2. Ozel metrigi kullanarak ozel niteligi kullanmaktir.
            //}

            // 2. Ozel metrigi kullanarak ozel niteligi kullanmaktir.
            // Validators klasoru ekliiyoruz tum islimler orada

            ///


            int newıd = CollegeRepository.Students.LastOrDefault().Id + 1; // İd yi elle 1 arttirdik
            Student student = new Student
            {
                Id = newıd,
                SutudentName = model.SutudentName,
                Address = model.Address,
                Email = model.Email
            };

            CollegeRepository.Students.Add(student);


            // Status - 201
            // https://localhost:7185/api/Student/3
            // New student details

            model.Id = student.Id;

            return CreatedAtRoute("GeStudentById", new { İd = model.Id }, model); // Yeni olusturulan kaydi almak icin baglantiyi hazirlayacak
            //return Ok(student);
        }


        [HttpPut]
        [Route("Update")]
        // api/student/update
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                BadRequest();

            var exisingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();

            if (exisingStudent == null)
                return NotFound();

            exisingStudent.SutudentName = model.SutudentName;
            exisingStudent.Email = model.Email;
            exisingStudent.Address = model.Address;

            return NoContent(); // Kayit guncellendi kayit yok yani icerik geri dondurmemize gerek yoksa bu sekilde yazabiliriz
                                // Geri donus 204 alicaz yani guncelleme basarili icerik yor


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

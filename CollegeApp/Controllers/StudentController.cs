using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Models;
using CollegeApp.Models.Dtos.Student;
using Dependency_Injection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Controllers
{
    // [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly ILogger<StudentController> _Logger;
        private readonly ILogger<StudentController> logger;
        private readonly CollageDBContext _dBContext;
        private readonly IMapper _mapper;

        public StudentController(ILogger<StudentController> logger, CollageDBContext dBContext, IMapper _mapper)
        {
            logger = logger;
            _dBContext = dBContext;
            _mapper = _mapper;
        }

        [HttpGet]
        [Route("All", Name = "GeAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi

        public async Task<ActionResult<IEnumerable<StudentDTO>>> GeStudents()
        {
            //_Logger.Log("Your Message");

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


            //var students = _dBContext.Students.Select(s => new StudentDTO()
            //{
            //    Id = s.Id,
            //    SutudentName = s.SutudentName,
            //    Address = s.Address,
            //    Email = s.Email,
            //    DOB= s.DOB,
            //});

            //var students = await _dBContext.Students.Select(s => new StudentDTO()
            //{
            //    Id = s.Id,
            //    SutudentName = s.SutudentName,
            //    Address = s.Address,
            //    DOB = s.DOB
            //}).ToListAsync();

            var students = await _dBContext.Students.ToListAsync();
            var studentDTOData = _mapper.Map<List<StudentDTO>>(students);

            // Ok - 200 - Success
            return Ok(studentDTOData);

        }

        [HttpGet]
        [Route("{İd:int}", Name = "GeStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi

        public async Task<ActionResult<StudentDTO>> GeStudentById(int id)
        {
            // BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
                return BadRequest();

            var student = await _dBContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (student == null)
                // NotFound - 404 - NotFound - Client error
                return NotFound($"The student with id {id} not fount");

            var studentDTOData = _mapper.Map<StudentDTO>(student);

            //var studentsDTO = new StudentDTO()
            //{
            //    Id = student.Id,
            //    SutudentName = student.SutudentName,
            //    Address = student.Address,
            //    Email = student.Email
            //};


            // Ok - 200 - Success
            return Ok(studentDTOData);

        }





        [HttpPost]
        [Route("Create")]
        // api/student/create
        [ProducesResponseType(StatusCodes.Status201Created)] // Kayıt olumlu 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] StudentDTO dto) // FromBody Sadece govde den almak istiyorum
        {
            /// Validationlari;

            // Yukaridaki [ApiController]  Yazmazsak " StudentDTO " yazdigimiz Validationlari calistirmak icin assagidaki
            // kontrolu yazmamiz lazim ;
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);


            ///




            if (dto == null)
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


            /*
             * 
             * int newıd = _dBContext.Students.LastOrDefault().Id + 1;  İd yi elle 1 arttirdik
             * 
             */

            //Student student = new Student
            //{

            //    SutudentName = dto.SutudentName,
            //    Address = dto.Address,
            //    Email = dto.Email,
            //    DOB = dto.DOB,
            //};




            var student = _mapper.Map<Student>(dto);

            await _dBContext.Students.AddAsync(student);

            await _dBContext.SaveChangesAsync();

            // Status - 201
            // https://localhost:7185/api/Student/3
            // New student details

            //model.Id = student.Id;
            dto.Id = student.Id;

            return CreatedAtRoute("GeStudentById", new { İd = dto.Id }, dto); // Yeni olusturulan kaydi almak icin baglantiyi hazirlayacak
            //return Ok(student);
        }


        [HttpPut]
        [Route("Update")]
        // api/student/update
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi
        public async Task<ActionResult> UpdateStudent([FromBody] StudentDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                BadRequest();

            var exisingStudent = await _dBContext.Students.AsNoTracking().Where(s => s.Id == dto.Id).FirstOrDefaultAsync();

            if (exisingStudent == null)
                return NotFound();

            // Yeni ogrenci ekleyelim ;
            //var newRecord = new Student()
            //{
            //    Id = exisingStudent.Id,
            //    SutudentName = exisingStudent.SutudentName,
            //    Address = exisingStudent.Address,
            //    Email = exisingStudent.Email,
            //    DOB = exisingStudent.DOB,

            //};

            var newRecord = _mapper.Map<Student>(dto);
            _dBContext.Students.Update(newRecord);
            // Burada ayni ID oldugundan hata verecektir ama AsNoTracking yani takip etme dersek varlik cercevesi izleme altinda olmayacaktir ve hata vermicektir



            //exisingStudent.SutudentName = dto.SutudentName;
            //exisingStudent.Email = dto.Email;
            //exisingStudent.Address = dto.Address;
            //exisingStudent.DOB = dto.DOB;


            await _dBContext.SaveChangesAsync();

            return NoContent(); // Kayit guncellendi kayit yok yani icerik geri dondurmemize gerek yoksa bu sekilde yazabiliriz
                                // Geri donus 204 alicaz yani guncelleme basarili icerik yor


        }


        // Assagidaki kod update de sadece 1 alani güncelleniyorsa tum verileri sunucuya gondermemk istiyorsak sadece guncellenen alanlara islem yapilmasi icin
        // 2 paket yuklememiz gerekiyor ;
        // 1.cisi Microsoft.AspNetCore.JsonPatch
        // 2.cisi Microsoft.AspNetCore.Mvc.NewtonsoftJson
        // paketleri yukledikten sonra program.cs icerisine " .AddNewtonsoftJson() " eklememiz gerekiyor
        [HttpPatch]
        [Route("{id:int}/UpdatePrtial")]
        // api/student/1/updatepartial
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi
        public async Task<ActionResult> UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            var exisingStudent = await _dBContext.Students.AsNoTracking().Where(s => s.Id == id).FirstOrDefaultAsync();

            if (exisingStudent == null)
                return NotFound();


            //var studentdDTO = new StudentDTO
            //{
            //    Id = exisingStudent.Id,
            //    SutudentName = exisingStudent.SutudentName,
            //    Email = exisingStudent.Email,
            //    Address = exisingStudent.Address,
            //    DOB = exisingStudent.DOB,
            //    //Age = exisingStudent.Age,
            //    //Password = exisingStudent.Password,
            //    //ConfirmPassword = exisingStudent.ConfirmPassword,
            //    //AdmissionDate = exisingStudent.AdmissionDate,
            //};

            var studentdDTO = _mapper.Map<StudentDTO>(exisingStudent);

            patchDocument.ApplyTo(studentdDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            exisingStudent = _mapper.Map<Student>(studentdDTO);

            _dBContext.Students.Update(exisingStudent);

            //exisingStudent.SutudentName = studentdDTO.SutudentName;
            //exisingStudent.Email = studentdDTO.Email;
            //exisingStudent.Address = studentdDTO.Address;
            //exisingStudent.Age = studentdDTO.Age;
            //exisingStudent.Password = studentdDTO.Password;
            //exisingStudent.ConfirmPassword = studentdDTO.ConfirmPassword;
            //exisingStudent.AdmissionDate = studentdDTO.AdmissionDate;


            await _dBContext.SaveChangesAsync();

            // 204 - NoContent
            return NoContent(); // Kayit guncellendi kayit yok yani icerik geri dondurmemize gerek yoksa bu sekilde yazabiliriz
                                // Geri donus 204 alicaz yani guncelleme basarili icerik yor


        }




        [HttpGet("{name:alpha}", Name = "GeStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi

        public async Task<ActionResult<StudentDTO>> GeStudentByName(string name)
        {
            // BadRequest - 400 - Badrequest - Client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = await _dBContext.Students.Where(x => x.SutudentName == name).FirstOrDefaultAsync();

            if (student == null)
                // NotFound - 404 - NotFound - Client error
                return NotFound($"The student with name {name} not fount");


            //var studentsDTO = new StudentDTO()
            //{
            //    Id = student.Id,
            //    SutudentName = student.SutudentName,
            //    Address = student.Address,
            //    Email = student.Email,
            //    DOB = student.DOB,
            //};

            var studentDTOData = _mapper.Map<StudentDTO>(student);

            // Ok - 200 - Success
            return Ok(studentDTOData);

        }

        [HttpDelete("{İd:min(1):max(100)}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Sunucu hatasi dahili sunucu hatasi

        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            // BadRequest - 400 - Badrequest - Client error
            if (id <= 0)
                return BadRequest();


            var student = await _dBContext.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (student == null)
                // NotFound - 404 - NotFound - Client error
                return NotFound($"The student with id {id} not fount");

            _dBContext.Students.Remove(student);
            await _dBContext.SaveChangesAsync();

            // Ok - 200 - Success
            return true;

        }
    }
}

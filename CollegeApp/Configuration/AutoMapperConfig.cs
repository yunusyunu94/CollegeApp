using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Models.Dtos.Student;

namespace CollegeApp.Configuration
{

    // Program.cs de OUTAMAPPER e bak

    public class AutoMapperConfig : Profile
    { 
        public AutoMapperConfig() 
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();

            // Veya

            //CreateMap<StudentDTO,Student>().ReverseMap();
        }
    }
}

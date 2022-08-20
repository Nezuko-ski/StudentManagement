using AutoMapper;
using StudentManagement.Dtos.StudentDtos;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Utils.AutoMapperProfiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentResponseDto>().ReverseMap();
            CreateMap<StudentRequestDto, Student>().ReverseMap();
        }
    }
}

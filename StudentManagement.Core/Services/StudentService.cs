using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.IServices;
using StudentManagement.Data;
using StudentManagement.Data.IRepositories;
using StudentManagement.Dtos.StudentDtos;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper, ApplicationDbContext context)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _context = context;
        }
        public async Task AddStudentAsync(StudentRequestDto studentRequestDto)
        {
            var student = _mapper.Map<Student>(studentRequestDto);
            student.CreatedDate = DateTime.Now;
            student.LastUpdatedDate = DateTime.Now;
            try
            {
                await _studentRepository.AddStudentAsync(student);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
        }

        public async Task DeleteStudentAsync(int id)
        {
            try
            {
                await _studentRepository.DeleteStudentAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return _mapper.Map<IEnumerable<StudentResponseDto>>(students);
        }

        public async Task<StudentResponseDto> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            //if(student == null)
            //{
            //    throw new ArgumentException(nameof(student));
            //}
            return _mapper.Map<StudentResponseDto>(student);
        }

        public async Task UpdateStudentAsync(StudentRequestDto studentRequestDto)
        {
            var dbStudent = await _context.Students
                .FirstOrDefaultAsync(v => v.Id == studentRequestDto.Id);
            if(dbStudent != null)
            {
                dbStudent.FirstName = studentRequestDto.FirstName;
                dbStudent.LastName = studentRequestDto.LastName;
                dbStudent.Email = studentRequestDto.Email;
                dbStudent.FavouriteQuote = studentRequestDto.FavouriteQuote;
                //student.CreatedDate = DateTime.Now;
                //student.LastUpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }    
        }
    }
}

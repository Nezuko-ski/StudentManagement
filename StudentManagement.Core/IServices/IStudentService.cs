using StudentManagement.Dtos.StudentDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Core.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync();
        Task<StudentResponseDto> GetStudentByIdAsync(int id);
        Task AddStudentAsync(StudentRequestDto studentRequestDto);
        Task UpdateStudentAsync(StudentRequestDto studentRequestDto);
        Task DeleteStudentAsync(int id);
    }
}

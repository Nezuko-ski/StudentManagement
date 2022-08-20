using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentManagement.Data.IRepositories;
using StudentManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Student> _db;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Students;
        }

        public async Task DeleteStudentAsync(int id)
        {
            _db.Remove(await GetStudentByIdAsync(id));
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();
        }

        public async Task AddStudentAsync(Student student)
        {
            await _db.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync() => await _db.AsNoTracking().ToListAsync();

        public async Task<Student> GetStudentByIdAsync(int id) => await _db.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        
    }
}

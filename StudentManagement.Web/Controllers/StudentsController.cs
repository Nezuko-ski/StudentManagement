using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.IServices;
using StudentManagement.Data;
using StudentManagement.Data.IRepositories;
using StudentManagement.Dtos.StudentDtos;
using StudentManagement.Models;

namespace StudentManagement.Web.Controllers
{
    public class StudentsController : Controller
    {

        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentService studentService, IMapper mapper, IStudentRepository studentRepository)
        {
            _studentService = studentService;
            _mapper = mapper;
            _studentRepository = studentRepository; 
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _studentService.GetAllStudentsAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await _studentRepository.GetStudentByIdAsync(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            } 
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentRequestDto student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.AddStudentAsync(student);
                }
                catch (Exception)
                {
                    View(student);
                }
 
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var studentDetails = await _studentService.GetStudentByIdAsync(id);
            if (studentDetails == null) return View("Not Found");
            var student = new Student()
            {
                Id = studentDetails.Id,
                Email = studentDetails.Email,
                FirstName = studentDetails.FullName.Split().First(),
                LastName = studentDetails.FullName.Split().Last(),
                FavouriteQuote = studentDetails.FavouriteQuote 
            };
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentRequestDto student)
        {
            if (id != student.Id) return View("Not Found");
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.UpdateStudentAsync(student);
                }
                catch (Exception)
                {
                    View(student);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var studentDetails = await _studentService.GetStudentByIdAsync(id);
            if (studentDetails == null) return View("Not Found");
            var student = new Student()
            {
                Id = studentDetails.Id,
                Email = studentDetails.Email,
                FirstName = studentDetails.FullName.Split().First(),
                LastName = studentDetails.FullName.Split().Last(),
                FavouriteQuote = studentDetails.FavouriteQuote
            };
            return View(student);    
        }

        //// POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Student student = new();
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.DeleteStudentAsync(id);
                }
                catch (Exception)
                {
                    View(student);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        //// GET: Students/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students.FindAsync(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(student);
        //}

        //// POST: Students/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Email,FirstName,LastName,FavouriteQuote,CreatedDate,LastUpdatedDate")] Student student)
        //{
        //    if (id != student.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(student);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!StudentExists(student.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(student);
        //}

        //// GET: Students/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var student = await _context.Students
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(student);
        //}



        //private bool StudentExists(int id)
        //{
        //    return _context.Students.Any(e => e.Id == id);
        //}
    }
}

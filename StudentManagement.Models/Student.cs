using System;

namespace StudentManagement.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
        public string FavouriteQuote { get; set; }
        public DateTime CreatedDate { get; set;} = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ef_Core_Assignment_3.Models
{
    internal class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public DateTime JoinDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

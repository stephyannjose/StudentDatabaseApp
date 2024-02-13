using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace StudentDatabaseApp
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace StudentDatabaseApp
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StudentDatabase;Trusted_Connection=True;");
        }
    }
}

using System;

namespace StudentDatabaseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new StudentDbContext())
            {
                // Create a new student
                var student = new Student
                {
                    Name = "John Doe",
                    DateOfBirth = new DateTime(2000, 1, 1)
                };

                // Add the student to the database
                context.Students.Add(student);
                context.SaveChanges();

                Console.WriteLine("Student added to the database.");
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;

namespace StudentDatabaseApp
{
    // Define your entity class
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    // Create your DbContext class
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        // Configure the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StudentDatabase;Trusted_Connection=True;");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var context = new StudentDbContext())
                {
                    // Create a new student
                    var student = new Student
                    {
                        Name = "Stephy Ann Jose",
                        DateOfBirth = new DateTime(1988, 4, 12)
                    };

                    // Add the student to the database
                    context.Students.Add(student);

                    // Save changes to the database
                    context.SaveChanges();

                    Console.WriteLine("Student added to the database.");
                }
            }
            catch (DbUpdateException ex)
            {
                // Print the outer exception message
                Console.WriteLine("An error occurred while updating the entries: " + ex.Message);

                // If there's an inner exception, print its message
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}

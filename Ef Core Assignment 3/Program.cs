using Ef_Core_Assignment_3.Models;
using Microsoft.EntityFrameworkCore;

namespace Ef_Core_Assignment_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //AddInstructor("John Doe", "john.doe@example.com", "A experienced instructor.", DateTime.Now);

            //AddCourse("PHP for Beginners", "Learn PHP from scratch.", 1, 1);

            //AddStudents();

            //EnrollStudent(1,100);

            //FindCourse(1);

            //FilterCourses(500, 1000);

            //PrintSubCategories(1);

            //PrintCourseRoster(1);

            //ChangeCoursePrice(1, 1200);

            //UpdateEnrollmentProgress(1, 1, 100);

            //PublishCourse(1);

            //UnenrollStudent(1, 1);


        }

        #region create
        // Add an instructor: Insert a new instructor and print the generated Id.
        public static void AddInstructor(string fullName, string email, string bio, DateTime joinDate)
        {
            using (var context = new ApplicationDbContext())
            {
                var instructor = new Instructor
                {
                    FullName = fullName,
                    Email = email,
                    Bio = bio,
                    JoinDate = joinDate
                };
                context.Instructors.Add(instructor);
                context.SaveChanges();
                Console.WriteLine($"Instructor added with Id: {instructor.Id}");
            }
        }

        //Add a course: Insert a course for an existing instructor and an existing category. Check that both exist first, otherwise print a message.
        public static void AddCourse(string title, string description, int instructorId, int categoryId)
        {
            using (var context = new ApplicationDbContext())
            {
                var instructor = context.Instructors.Find(instructorId);
                var category = context.Categories.Find(categoryId);

                if (instructor == null)
                {
                    Console.WriteLine("Instructor not found.");
                    return;
                }

                if (category == null)
                {
                    Console.WriteLine("Category not found.");
                    return;
                }

                var course = new Course
                {
                    Title = title,
                    Description = description,
                    InstructorId = instructorId,
                    CategoryId = categoryId
                };
                context.Courses.Add(course);
                context.SaveChanges();
                Console.WriteLine($"Course added with Id: {course.Id}");
            }
        }

        // Add several students: Insert three students using one AddRange and one SaveChanges. Print their generated Ids
        public static void AddStudents()
        {
            using (var context = new ApplicationDbContext())
            {
                var students = new List<Student>
                {
                    new Student { FullName = "Alice Johnson", Email = "alice.johnson@example.com", DateOfBirth = new DateTime(2000, 1, 1) },
                    new Student { FullName = "Bob Smith", Email = "bob.smith@example.com", DateOfBirth = new DateTime(1998, 5, 15) },
                    new Student { FullName = "Charlie Brown", Email = "charlie.brown@example.com", DateOfBirth = new DateTime(1999, 12, 25) }
                };
                context.Students.AddRange(students);
                context.SaveChanges();
                foreach (var student in students)
                {
                    Console.WriteLine($"Student added with Id: {student.Id}");
                }
            }
        }

        // Enroll a student: The student and the course must exist, the course must be published, and the student must not be enrolled already. Otherwise print why the enrollment was refused.
        public static void EnrollStudent(int studentId, int courseId)
        {
            using (var context = new ApplicationDbContext())
            {
                var student = context.Students.Find(studentId);
                var course = context.Courses.Find(courseId);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }
                if (!course.IsPublished)
                {
                    Console.WriteLine("Course is not published.");
                    return;
                }
                var existingEnrollment = context.Enrollments
                    .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
                if (existingEnrollment != null)
                {
                    Console.WriteLine("Student is already enrolled in this course.");
                    return;
                }
                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.Now
                };
                context.Enrollments.Add(enrollment);
                context.SaveChanges();
                Console.WriteLine($"Student {student.FullName} enrolled in course {course.Title}.");
            }
        }
        #endregion

        #region Read

        // Find a course: Get a course by Id using Find, then by title using FirstOrDefault. Print "not found" when the result is null.
        public static void FindCourse(int courseId = 0, string title = "")
        {
            using (var context = new ApplicationDbContext())
            {
                var courseById = context.Courses.Find(courseId);
                if (courseById != null || courseId != 0)
                {
                    Console.WriteLine($"Found course by Id: {courseById.Title}");
                }
                else
                {
                    Console.WriteLine("Course not found by Id.");
                }
                var courseByTitle = context.Courses.FirstOrDefault(c => c.Title == title);
                if (courseByTitle != null || title != "")
                {
                    Console.WriteLine($"Found course by Title: {courseByTitle.Title}");
                }
                else
                {
                    Console.WriteLine("Course not found by Title.");
                }
            }
        }

        // Filter courses: Courses priced between two values, ordered by price descending. Print only the title, level and price.
        public static void FilterCourses(decimal minPrice, decimal maxPrice)
        {
            using (var context = new ApplicationDbContext())
            {
                var courses = context.Courses
                    .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                    .OrderByDescending(c => c.Price)
                    .Select(c => new { c.Title, c.Level, c.Price })
                    .ToList();
                foreach (var course in courses)
                {
                    Console.WriteLine($"Title: {course.Title}, Level: {course.Level}, Price: {course.Price}");
                }
            }
        }

        // Sub-categories: Print the direct sub-categories of a given category, then print the top-level categories.
        public static void PrintSubCategories(int categoryId)
        {
            using (var context = new ApplicationDbContext())
            {
                var subCategories = context.Categories
                    .Where(c => c.ParentCategoryId == categoryId)
                    .ToList();
                Console.WriteLine($"Sub-categories of category {categoryId}:");
                foreach (var subCategory in subCategories)
                {
                    Console.WriteLine($"- {subCategory.Name}");
                }
                var topLevelCategories = context.Categories
                    .Where(c => c.ParentCategoryId == null)
                    .ToList();
                Console.WriteLine("Top-level categories:");
                foreach (var topLevelCategory in topLevelCategories)
                {
                    Console.WriteLine($"- {topLevelCategory.Name}");
                }
            }
        }

        // Course roster: Print how many students are enrolled in a course, then their names. Do it without joins: get the student Ids from the enrollments first, then get the students with Contains.
        public static void PrintCourseRoster(int courseId)
        {
            using (var context = new ApplicationDbContext())
            {
                var enrollmentStudentIds = context.Enrollments
                    .Where(e => e.CourseId == courseId)
                    .Select(e => e.StudentId)
                    .ToList();
                var students = context.Students
                    .Where(s => enrollmentStudentIds.Contains(s.Id))
                    .ToList();
                Console.WriteLine($"Number of students enrolled in course {courseId}: {students.Count}");
                foreach (var student in students)
                {
                    Console.WriteLine($"- {student.FullName}");
                }
            }
        }

        #endregion

        #region Update
        //Change a price: Load a course, change its price, and save.

        public static void ChangeCoursePrice(int courseId, decimal newPrice)
        {
            using (var context = new ApplicationDbContext())
            {
                var course = context.Courses.Find(courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }
                decimal oldPrice = course.Price;
                course.Price = newPrice;
                context.SaveChanges();
                Console.WriteLine($"Course {course.Title} price changed from {oldPrice} to {newPrice}.");
            }
        }

        // Update progress: Find the enrollment by its composite key and set the new progress. At 100 set the status to Completed. Reject values outside 0–100.
        public static void UpdateEnrollmentProgress(int studentId, int courseId, int newProgress)
        {
            using (var context = new ApplicationDbContext())
            {
                var enrollment = context.Enrollments.Find(studentId, courseId);
                if (enrollment == null)
                {
                    Console.WriteLine("Enrollment not found.");
                    return;
                }
                if (newProgress < 0 || newProgress > 100)
                {
                    Console.WriteLine("Progress must be between 0 and 100.");
                    return;
                }
                enrollment.ProgressPercentage = newProgress;
                if (newProgress == 100)
                {
                    enrollment.Status = EnrollmentStatus.Completed;
                }
                context.SaveChanges();
                Console.WriteLine($"Enrollment progress updated to {newProgress}%. Status: {enrollment.Status}");
            }
        }

        // Publish a course: Set the published flag and the published date.

        public static void PublishCourse(int courseId)
        {
            using (var context = new ApplicationDbContext())
            {
                var course = context.Courses.Find(courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }
                course.IsPublished = true;
                course.PublishedDate = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine($"Course {course.Title} is now published.");
            }
        }
        #endregion

        #region Delete
        // Unenroll: Delete an enrollment using its composite key.
        public static void UnenrollStudent(int studentId, int courseId)
        {
            using (var context = new ApplicationDbContext())
            {
                var enrollment = context.Enrollments.Find(studentId, courseId);
                if (enrollment == null)
                {
                    Console.WriteLine("Enrollment not found.");
                    return;
                }
                context.Enrollments.Remove(enrollment);
                context.SaveChanges();
                Console.WriteLine($"Student {studentId} unenrolled from course {courseId}.");
            }
        }

        // Delete a category: First try to remove a category that still has sub-categories and catch the DbUpdateException,
        // then print its message. After that, write a safe version that checks for sub-categories before deleting.
        public static void DeleteCategory(int categoryId)
        {
            using (var context = new ApplicationDbContext())
            {
                var category = context.Categories.Find(categoryId);
                if (category == null)
                {
                    Console.WriteLine("Category not found.");
                    return;
                }
                try
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    Console.WriteLine($"Category {category.Name} deleted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting category: {ex.Message}");
                }
            }
        }

        // Delete a course: Delete a course that has enrollments.
        // Print the number of its enrollments before and after, and explain in a comment why they were removed.
        public static void DeleteCourse(int courseId)
        {
            using (var context = new ApplicationDbContext())
            {
                var course = context.Courses.Find(courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }
                var enrollmentCountBefore = context.Enrollments.Count(e => e.CourseId == courseId);
                Console.WriteLine($"Number of enrollments before deleting course: {enrollmentCountBefore}");
                context.Courses.Remove(course);
                context.SaveChanges();
                var enrollmentCountAfter = context.Enrollments.Count(e => e.CourseId == courseId);
                Console.WriteLine($"Number of enrollments after deleting course: {enrollmentCountAfter}");
                // The enrollments were removed because of the cascade delete behavior configured in the model.
            }
        }
        #endregion

        #region Bulk
        // Bulk price raise: In a single statement, without loading the entities,
        // raise the price of all published courses of a given level by 10%. Print the number of affected rows.
        public static void BulkPriceRaise(CourseLevel level)
        {
            using (var context = new ApplicationDbContext())
            {
                var affectedRows = context.Courses
                    .Where(c => c.Level == level && c.IsPublished)
                    .ExecuteUpdate(c => c.SetProperty(course => course.Price, course => course.Price * 1.10m));
                Console.WriteLine($"Number of courses updated: {affectedRows}");
            }
        }

        // Bulk delete: In a single statement, delete all enrollments whose status is Dropped. Print the number of affected rows.
        public static void BulkDeleteDroppedEnrollments()
        {
            using (var context = new ApplicationDbContext())
            {
                var affectedRows = context.Enrollments
                    .Where(e => e.Status == EnrollmentStatus.Dropped)
                    .ExecuteDelete();
                Console.WriteLine($"Number of enrollments deleted: {affectedRows}");
            }
        }
        #endregion


    }
}

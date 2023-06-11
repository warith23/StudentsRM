using StudentsRM.Models;
using StudentsRM.Models.Student;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;
using StudentsRM.Entities;
using System.Linq.Expressions;

namespace StudentsRM.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel Create(CreateStudentModel request)
        {
            var response = new BaseResponseModel();
            var student = new Student
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender,
                HomeAddress = request.HomeAddress,
                // RegisteredBy = "Admin",
                DateCreated = DateTime.Today,
                // Courses = new List<Course>()
            };

            // var user = new User()
            // {
            //     Email = student.Email,
            //     Password = "12345",
            // };
            
            var selectCourses = _unitOfWork.Courses.GetAllByIds(request.CourseIds);
            var courses = new HashSet<Course>();
            foreach (var selectCourse in selectCourses)
            {
                var course = new Course
                {
                    Id = selectCourse.Id,
                };
                courses.Add(course);
            }
            student.Courses = courses;
            
            try
            {
                _unitOfWork.Students.Create(student);
                // _unitOfWork.Users.Create(user);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Succcess";
                return response;
            }
            catch (System.Exception)
            {
                response.Message = "AN error occurred";
                return response;
            }
        }

        public BaseResponseModel Delete(string studentId)
        {
            throw new NotImplementedException();
        }

        public StudentsResponseModel GetAll()
        {
            var response = new StudentsResponseModel();
            try
            {
                Expression<Func<Student, bool>> expression = s => s.IsDeleted == false;
                var students = _unitOfWork.Students.GetAll(expression);

                if (students is null)
                {
                    response.Message = "No student found on System";
                    return response;
                }

                response.Data = students.Select(
                    students => new StudentViewModel
                    {
                        FirstName = students.FirstName,
                        LastName = students.LastName,
                        HomeAddress = students.HomeAddress,
                        DateAdmitted = students.DateAdmitted,
                    }).ToList();
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message}";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public StudentResponseModel GetStudent(string studentId)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel Update(string studentId, UpdateStudentViewModel update)
        {
            throw new NotImplementedException();
        }
    }
}
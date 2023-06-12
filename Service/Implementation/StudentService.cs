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
                PhoneNumber = request.PhoneNumber,
                RegisteredBy = "Admin",
                DateAdmitted = DateTime.Today
                // DateCreated = DateTime.Today,
            };

            // var user = new User()
            // {
            //     Email = student.Email,
            //     Password = "12345",
            // };
            
            var Courses = _unitOfWork.Courses.GetAllByIds(request.CourseIds);
            var studentCourses = new HashSet<StudentCourse>();
            foreach (var course in Courses)
            {
                var studentCourse = new StudentCourse
                {
                    CourseId = course.Id,
                    StudentId = student.Id,
                    Course = course,
                    Student = student,
                    RegisteredBy = "Admin"
                };
                studentCourses.Add(studentCourse);
            }
            student.Courses = studentCourses;
            
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
                        MiddleName = students.MiddleName,
                        LastName = students.LastName,
                        HomeAddress = students.HomeAddress,
                        DateAdmitted = students.DateAdmitted,
                        Gender = students.Gender,
                        Email = students.Email
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
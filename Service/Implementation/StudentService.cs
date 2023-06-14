using StudentsRM.Models;
using StudentsRM.Models.Student;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;
using StudentsRM.Entities;
using System.Linq.Expressions;
using StudentsRM.Helper;

namespace StudentsRM.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel Create(CreateStudentModel request, string roleName)
        {
            var response = new BaseResponseModel();
            var defaultPassword = "12345";
            string saltString = HashingHelper.GenerateSalt();
            string hashedPassword = HashingHelper.HashPassword(defaultPassword, saltString);
            var createdBy = _httpContextAccessor.HttpContext.User.Identity.Name;

            var ifExist = _unitOfWork.Lecturers.Exists(s => (s.Email == request.Email));
            if (ifExist)
            {
                response.Message = "Email already in use";
                return response;
            }
            
            var student = new Student
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender,
                HomeAddress = request.HomeAddress,
                PhoneNumber = request.PhoneNumber,
                RegisteredBy = createdBy,
                DateAdmitted = DateTime.Today
                // DateCreated = DateTime.Today,
            };

            roleName ??= "Student";

            var role = _unitOfWork.Roles.Get(x => x.RoleName == roleName);

            if (role is null)
            {
                response.Message = $"Role does not exist";
                return response;
            }

            var user = new User()
            {
                Email = student.Email,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
                RoleId = role.Id,
                RegisteredBy = "Admin",
                CheckUserId = student.Id
            };
            
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
                _unitOfWork.Users.Create(user);
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
                // var GetCourse = _unitOfWork.Students.GetStudentCourse()

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
                        // Course = _unitOfWork.Students.GetStudentCourse(students.Courses)
                        // Course = students.Courses.Select(c => c.Course).Select(c => c.Name).ToList(),
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
            var response = new StudentResponseModel();

            Expression<Func<Student, bool>> expression = s => (s.Id == studentId) && (s.IsDeleted == false);

            var ifExist = _unitOfWork.Students.Exists(expression);

            if (!ifExist)
            {
                response.Message = "Student is not registered on system";
                return response;
            } 
             
            var student = _unitOfWork.Students.Get(studentId);
            response.Data = new StudentViewModel
            {
                Id = student.Id,
                FullName =  $"{student.FirstName} {student.MiddleName} {student.LastName}",
                Email = student.Email,
                // Course = student.Courses.Select(c => c.Course.Name).ToString()
            };
            response.Message = "Success";
            response.Status = true;
            return response;
        }
 
        public BaseResponseModel Update(string studentId, UpdateStudentViewModel update)
        {
            throw new NotImplementedException();
        }
    }
}
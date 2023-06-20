using StudentsRM.Models;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;
using StudentsRM.Entities;
using System.Linq.Expressions;
using StudentsRM.Helper;
using System.Security.Claims;
using StudentsRM.Models.Students;

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
            var selectCourse = _unitOfWork.Courses.Get(request.CourseId);

            var ifExist = _unitOfWork.Lecturers.Exists(s => (s.Email == request.Email) && (s.IsDeleted == false));
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
                DateAdmitted = DateTime.Today,
                Course = selectCourse,
                CourseId = selectCourse.Id,
                DateCreated = DateTime.Today,
                ModifiedBy = "",
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
                ModifiedBy = "",
                Student = student,
                StudentId = student.Id
            };
            
            // var Courses = _unitOfWork.Courses.GetAllByIds(request.CourseIds);
            // var studentCourses = new HashSet<StudentCourse>();
            // foreach (var course in Courses)
            // {
            //     var studentCourse = new StudentCourse
            //     {
            //         CourseId = course.Id,
            //         StudentId = student.Id,
            //         Course = course,
            //         Student = student,
            //         RegisteredBy = "Admin"
            //     };
            //     studentCourses.Add(studentCourse);
            // }
            // student.Courses = studentCourses;
            
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
            var response = new BaseResponseModel();
            var ifExist = _unitOfWork.Students.Exists(s => s.Id == studentId && !s.IsDeleted);

            if (!ifExist)
            {
                response.Message = "Student does not exist";
                response.Status = true;
                return response;
            }
            
            var student = _unitOfWork.Students.Get(studentId);
            student.IsDeleted = true;

            try
            {
                _unitOfWork.Students.Update(student);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Student successfully deleted";

                return response;
            }
            catch (System.Exception)
            {
                response.Message = "Failed to register Students at this time";
                return response;
            }
        }

        public StudentsResponseModel GetAll()
        {
            var response = new StudentsResponseModel();
            try
            {
                Expression<Func<Student, bool>> expression = s => s.IsDeleted == false;
                var students = _unitOfWork.Students.GetAllStudent(expression);

                if (students is null || students.Count == 0)
                {
                    response.Message = "No student found on System";
                    return response;
                }

                response.Data = students.Select(
                    students => new StudentViewModel 
                    {
                        Id = students.Id,
                        FirstName = students.FirstName,
                        MiddleName = students.MiddleName,
                        LastName = students.LastName,
                        CourseId = students.CourseId,
                        Course = students.Course.Name,
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

        public StudentsResponseModel GetAllLecturerStudents()
        {
            var response = new StudentsResponseModel();
           
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var getLecturer = _unitOfWork.Users.Get(u => u.Id == userIdClaim);
            var lecturer = _unitOfWork.Lecturers.Get(getLecturer.LecturerId);
            try
            {
                Expression<Func<Student, bool>> expression = s => (s.IsDeleted == false) && (s.CourseId == lecturer.CourseId);
                var students = _unitOfWork.Students.GetAllStudent(expression);

                if (students is null)
                {
                    response.Message = "No student found on System";
                    return response;
                }

                response.Data = students.Select(
                    students => new StudentViewModel 
                    {
                        Id = students.Id,
                        FirstName = students.FirstName,
                        MiddleName = students.MiddleName,
                        LastName = students.LastName,
                        Course = students.Course.Name,
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

            var ifExist = _unitOfWork.Students.Exists(s =>
                                (s.Id == studentId)
                                && (s.Id == studentId
                                && s.IsDeleted == false));    

            if (!ifExist)
            {
                response.Message = "Student is not registered on system";
                return response;
            }
            try
            {
                var student = _unitOfWork.Students.Get(studentId);
                response.Data = new StudentViewModel
                {
                    Id = student.Id,
                    FullName =  $"{student.FirstName} {student.MiddleName} {student.LastName}",
                    Email = student.Email,
                };
                response.Message = "Success";
                response.Status = true;
                
            }
            catch (System.Exception)
            {
                response.Message = "An error occurred";
                return response;
            } 
             
            
            return response;
        }

        public BaseResponseModel Update(UpdateStudentViewModel update, string studentId)
        {
              var response = new BaseResponseModel();
            var modifiedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            var selectCourse = _unitOfWork.Courses.Get(update.CourseId);
            
            Expression<Func<Student, bool>> expression = s =>
                                                (s.Id == studentId)
                                                && (s.Id == studentId
                                                && s.IsDeleted == false);

            var islecturerExist = _unitOfWork.Students.Exists(expression);

            if (!islecturerExist)
            {
                response.Message = "student does not exist!";
                return response;
            }

            var student = _unitOfWork.Students.Get(studentId);
            var user = _unitOfWork.Users.Get(x => x.StudentId == student.Id);

            student.Email = update.Email;
            student.Course = selectCourse;
            student.CourseId = selectCourse.Id;
            student.HomeAddress = update.HomeAddress;
            student.PhoneNumber = update.PhoneNumber;
            student.ModifiedBy = modifiedBy;

            user.Email = update.Email;
            user.ModifiedBy = modifiedBy;
            try
            {
                _unitOfWork.Students.Update(student);
                _unitOfWork.Users.Update(user);
                _unitOfWork.SaveChanges();
                response.Message = "student updated successfully.";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Could not update the student: {ex.Message}";
                return response;
            }
        }

        public StudentsResponseModel GetAllLecturerStudentsForResults()
        {
            var response = new StudentsResponseModel();
           
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var getLecturer = _unitOfWork.Users.Get(u => u.Id == userIdClaim);
            var lecturer = _unitOfWork.Lecturers.Get(getLecturer.StudentId);
            var semester = _unitOfWork.Semesters.Get(s => s.CurrentSemester == true);

             Expression<Func<Student, bool>> expression = s => (s.IsDeleted == false) 
                                                     && (s.CourseId == lecturer.CourseId)
                                                     && !s.Results
                .Any(r => r.StudentId == s.Id && r.SemesterId == semester.Id && r.CourseId == lecturer.CourseId);
                
            var students = _unitOfWork.Students.GetAllStudent(expression);
            

            try
            {

                if (students is null)
                {
                    response.Message = "No student found on System";
                    return response;
                }

                response.Data = students.Select(
                    students => new StudentViewModel 
                    {
                        Id = students.Id,
                        FullName =  $"{students.FirstName} {students.MiddleName} {students.LastName}",
                        Course = students.Course.Name,
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
    }
}
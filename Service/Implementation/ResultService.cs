using System.Security.Claims;
using StudentsRM.Entities;
using StudentsRM.Models;
using StudentsRM.Models.Results;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;

namespace StudentsRM.Service.Implementation
{
    public class ResultService : IResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResultService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;           
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel Create(AddResultViewModel request,string studentId)
        {
            var response = new BaseResponseModel();
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var getLecturer = _unitOfWork.Users.Get(u => u.Id == userIdClaim);
            var lecturer = _unitOfWork.Lecturers.Get(getLecturer.LecturerId);
            var course = _unitOfWork.Courses.Get(lecturer.CourseId);
            var selectSemester = _unitOfWork.Semesters.Get(s => s.CurrentSemester == true);
            var student = _unitOfWork.Students.Get(studentId);

            var checkStudent =  _unitOfWork.Results
                .Exists(r => r.SemesterId == selectSemester.Id && r.StudentId == student.Id && r.CourseId == course.Id);
            
            if (checkStudent )
            {
                response.Message = "Result already added";
                return response;
            }

            if (!lecturer.Course.Id.Equals(student.CourseId)) 
            {
                response.Message = "An error occurred";
                return response;
            }
            
            var result = new Result
            {
                CourseId = course.Id,
                Course = course,
                SemesterId = selectSemester.Id, 
                Semester = selectSemester,
                Student = student,
                StudentId = student.Id,
                Score = request.Score,
                RegisteredBy = lecturer.LastName,
                ModifiedBy = ""
            };
            
            student.Results.Add(result);
                

            try
            {
                _unitOfWork.Results.Create(result);            
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Succcess";
                return response;
            } 
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message}";
                return response;
            }

        }


        public BaseResponseModel Delete(string resultId)
        {
            throw new NotImplementedException();
        }

        public ResultsResponseModel GetAll()
        {
            throw new NotImplementedException();
        }

        public ResultResponseModel CheckStudentResult()
        {
            var response = new ResultResponseModel();
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var getStudent = _unitOfWork.Users.Get(u => u.Id == userIdClaim);
            var semester = _unitOfWork.Semesters.Get(s => s.CurrentSemester == true);
            var student = _unitOfWork.Students.Get(getStudent.StudentId);
            var result = _unitOfWork.Results.GetResult(r => (r.StudentId == student.Id) && (r.CourseId == student.CourseId)
                                                  && (r.SemesterId == semester.Id));

            if (result is null)
            {
                response.Message = "Result is not availlable currently";
                return response;
            }
            
            try
            {
                response.Data = new ResultViewModel
                {
                    Id = result.Id,
                    StudentId = result.StudentId,
                    SemesterId = result.SemesterId,
                    CourseId = result.CourseId,
                    SemesterName = result.Semester.SemesterName,
                    StudentName = result.Student.FirstName,
                    CourseName = result.Course.Name,
                    Score = result.Score 
                };
                response.Status = true;
                response.Message = "Succcess";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured{ex.Message}";
                return response;
            }         
            return response;
        }

        public BaseResponseModel Update(string resultId, UpdateResultViewModel update)
        {
            throw new NotImplementedException();
        }

        
        // public BaseResponseModel Create(AddResultViewModel request)
        // {
        //     var response = new BaseResponseModel();
        //     var userIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        //     var getLecturer = _unitOfWork.Users.Get(u => u.Id == userIdClaim);
        //     var lecturer = _unitOfWork.Lecturers.Get(getLecturer.LecturerStudentId);
        //     var course = _unitOfWork.Courses.Get(lecturer.CourseId);
        //     var selectSemester = _unitOfWork.Semesters.Get(s => s.CurrentSemester == true);
        //     var students = _unitOfWork.Students.GetAll(s => s.CourseId == lecturer.CourseId);

        //     if (!lecturer.Course.Id.Equals(students.Select(s => s.CourseId))) 
        //     {
        //         response.Message = "An error occurred";
        //         return response;
        //     }   

        //     try
        //     {
        //         foreach (var student in students)
        //         {
        //             var result = new Result
        //             {
        //                 CourseId = course.Id,
        //                 Course = course,
        //                 SemesterId = selectSemester.Id, 
        //                 Semester = selectSemester,
        //                 Student = student,
        //                 StudentId = student.Id,
        //                 Score = request.Score,
        //                 RegisteredBy = lecturer.LastName,
        //             };
        //             student.Results.Add(result);
        //             _unitOfWork.Results.Create(result);
        //         }            
        //         _unitOfWork.SaveChanges();
        //         response.Status = true;
        //         response.Message = "Succcess";
        //         return response;
        //     }
        //     catch (Exception ex)
        //     {
        //         response.Message = $"An error occurred {ex.Message}";
        //         return response;
        //     }

        // }
    }
} 
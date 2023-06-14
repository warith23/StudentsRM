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

        public ResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BaseResponseModel Create(AddResultViewModel request, string courseId, string lecturerId)
        {
            var response = new BaseResponseModel();
            var lecturer = _unitOfWork.Lecturers.Get(lecturerId);
            var course = _unitOfWork.Courses.Get(lecturer.CourseId);
            var semester = _unitOfWork.Semesters.Get(request.SemesterId);
            var students = _unitOfWork.Students.GetAll(c => c.Courses == lecturer.Course);

            // var studentCourse = _unitOfWork.Students.GetStudentCourse(students.)

            // if (lecturer.Course.Id == students)
            // {
                
            // }
            
            var ResultView = students.Select(
                s => new ResultViewModel
                {
                    // CourseId = s.Courses,
                    StudentId = s.Id,
                    Score = request.Score,
                }
            ).ToList();
            
            foreach (var student in students)
            {
                var result = new Result
                {
                    CourseId = course.Id,
                    Course = course,
                    SemesterId = semester.Id, 
                    Semester = semester,
                    Student = student,
                    StudentId = student.Id,
                    Score = request.Score,
                };
                student.Results.Add(result);
            }
            return null;

        }

        public BaseResponseModel Create(AddResultViewModel request, string courseId, List<string> studentId)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel Delete(string resultId)
        {
            throw new NotImplementedException();
        }

        public ResultsResponseModel GetAll()
        {
            throw new NotImplementedException();
        }

        public ResultResponseModel GetResult(string resultId)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel Update(string resultId, UpdateResultViewModel update)
        {
            throw new NotImplementedException();
        }
    }
}
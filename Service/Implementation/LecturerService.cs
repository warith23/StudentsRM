using StudentsRM.Models;
using StudentsRM.Models.Lecturer;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;
using StudentsRM.Entities;
using System.Linq.Expressions;

namespace StudentsRM.Service.Implementation
{
    public class LecturerService : ILecturerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LecturerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel Create(CreateLecturerModel request)
        {
            var response = new BaseResponseModel();

            //var ifExist = _unitOfWork.Lecturers.Exists(l => (l.FirstName == request.FirstName)
            var selectCourse = _unitOfWork.Courses.Get(request.CourseId);

            var lecturer = new Lecturer
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender,
                HomeAddress = request.HomeAddress,
                RegisteredBy = "Admin",
                // DateCreated = DateTime.Today,
                Course = selectCourse,
                CourseId = selectCourse.Id
            };

            
            try
            {
                _unitOfWork.Lecturers.Create(lecturer);
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

        public BaseResponseModel Delete(string lecturerId)
        {
            var response = new BaseResponseModel();

            var ifExist = _unitOfWork.Lecturers.Exists(l => l.Id == lecturerId);

            if (!ifExist)
            {
                response.Message = "Lecturer not found";
                return response;
            }

            var lecturer = _unitOfWork.Lecturers.Get(lecturerId);
            lecturer.IsDeleted = true;
            try
            {
                _unitOfWork.Lecturers.Update(lecturer);
                _unitOfWork.SaveChanges();
                response.Status= true;
                response.Message = "Successfully deleted";
                return response;
            }
            catch (System.Exception)
            {
                response.Message = "An error occurred";
                return response;
            }
        }

        public LecturersResponseModel GetAll()
        {
            var response = new LecturersResponseModel();
            try
            {
                Expression<Func<Lecturer, bool>> expression = l => l.IsDeleted == false;
                var lecturers = _unitOfWork.Lecturers.GetAll(expression);
                if (lecturers.Count == 0 || lecturers is null)
                {
                    response.Message = "Lecturers not found on system";
                    return response;
                }

                response.Data = lecturers.Select(
                    lecturers => new LecturerViewModel
                    {
                        Id = lecturers.Id,
                        FirstName = lecturers.FirstName,
                        LastName = lecturers.LastName,
                        Course = lecturers.Course.Name,
                        PhoneNumber = lecturers.PhoneNumber,
                        HomeAddress = lecturers.HomeAddress
                    }).ToList();
            }
            catch (Exception ex)
            {
                response.Message = $"An error occcurred {ex.Message}";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public LecturerResponseModel GetLecturer(string lecturerId)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel Update(string lecturerId, UpdateLecturerViewModel update)
        {
            throw new NotImplementedException();
        }
    }
}
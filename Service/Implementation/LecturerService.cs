using System.Linq.Expressions;
using StudentsRM.Entities;
using StudentsRM.Models.Lecturer;
using StudentsRM.Models;
using StudentsRM.Service.Interface;
using StudentsRM.Repository.Interface;
using StudentsRM.Helper;

namespace lecturersRM.Service.Implementation
{
    public class LecturerService : ILecturerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LecturerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel Create(CreateLecturerModel request, string roleName)
        {
            var response = new BaseResponseModel();
            var defaultPassword = "12345";
            string saltString = HashingHelper.GenerateSalt();
            string hashedPassword = HashingHelper.HashPassword(defaultPassword, saltString);

            var ifExist = _unitOfWork.Lecturers.Exists(l => (l.Email == request.Email));
            if (ifExist)
            {
                response.Message = "Email already in use";
            }

            var selectCourse = _unitOfWork.Courses.Get(request.CourseId);

            var lecturer = new Lecturer
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                HomeAddress = request.HomeAddress,
                RegisteredBy = "Admin",
                // DateCreated = DateTime.Today,
                Course = selectCourse,
                CourseId = selectCourse.Id
            };

            roleName ??= "Lecturer";

            var role = _unitOfWork.Roles.Get(x => x.RoleName == roleName);

            if (role is null)
            {
                response.Message = $"Role does not exist";
                return response;
            }

            var user = new User()
            {
                Email = lecturer.Email,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
                RoleId = role.Id,
                RegisteredBy = "Admin",
                CheckUserId = lecturer.Id
            };

            
            try
            {
                _unitOfWork.Lecturers.Create(lecturer);
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
                // Expression<Func<Lecturer, bool>> expression = l => l.IsDeleted == false;
                var lecturers = _unitOfWork.Lecturers.GetAll(l => l.IsDeleted == false);

                if (lecturers.Count == 0 || lecturers is null)
                {
                    response.Message = "No lecturer found on System";
                    return response;
                }

                response.Data = lecturers.Select(
                    lecturers => new LecturerViewModel
                    {
                        Id = lecturers.Id,
                        FirstName = lecturers.FirstName,
                        MiddleName = lecturers.MiddleName,
                        LastName = lecturers.LastName,
                        HomeAddress = lecturers.HomeAddress,
                        CourseId = lecturers.CourseId,
                        Course = lecturers.Course.Name,
                        Gender = lecturers.Gender,
                        Email = lecturers.Email
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

        public LecturerResponseModel GetLecturer(string lecturerId)
        {
            var response = new LecturerResponseModel();

            Expression<Func<Lecturer, bool>> expression = l => (l.Id == lecturerId) && (l.IsDeleted == false);

            var ifExist = _unitOfWork.Lecturers.Exists(expression);

            if (!ifExist)
            {
                response.Message = "Lecturer is not registered on system";
                return response;
            } 
             
            var lecturer = _unitOfWork.Lecturers.Get(lecturerId);
            response.Data = new LecturerViewModel
            {
                Id = lecturer.Id,
                FullName =  $"{lecturer.FirstName} {lecturer.MiddleName} {lecturer.LastName}",
                Email = lecturer.Email,
                Course = lecturer.Course.Name
            };
            response.Message = "Success";
            response.Status = true;
            return response;
        }

        public BaseResponseModel Update(string lecturerId, UpdateLecturerViewModel update)
        {
            throw new NotImplementedException();
        }
    }
}
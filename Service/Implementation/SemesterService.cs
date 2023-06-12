using StudentsRM.Models;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;
using StudentsRM.Entities;
using System.Linq.Expressions;
using StudentsRM.Models.Semester;

namespace StudentsRM.Service.Implementation
{  
    public class SemesterService : ISemesterService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SemesterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel Create(CreateSemesterViewModel request)
        {
            var response = new BaseResponseModel();
            //var ifExist = _unitOfWork.Semesters.Exists(s => s.Id == request.)

            var semester = new Semester
            {
                SemesterName = request.SemesterName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                RegisteredBy = "Admin"
            };

            try
            {
                _unitOfWork.Semesters.Create(semester);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Success";

                return response;
            }
            catch (System.Exception)
            {
                response.Message = "System Error";
                return response;
            }
        }

        public BaseResponseModel Delete(string semesterId)
        {
            throw new NotImplementedException();
        }

        public SemestersResponseModel GetAll()
        {
            var response = new SemestersResponseModel();

            try
            {
                var semesters = _unitOfWork.Semesters.GetAll(s => s.IsDeleted == false);

                if (semesters is null || semesters.Count == 0)
                {
                    response.Message = "Semesters not found";
                    return response;
                }

                response.Data = semesters.Select(
                    s => new SemesterViewModel
                    {
                        SemesterName = s.SemesterName,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate
                    }
                ).ToList();

                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = $"Error{ex.Message}";
                return response;
            }
            return response;
        }

        public SemesterResponseModel GetSemester(string semesterId)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel Update(string semesterId, UpdateSemesterViewModel update)
        {
            throw new NotImplementedException();
        }
    }
}
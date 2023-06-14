using StudentsRM.Models;
using StudentsRM.Models.Lecturer;

namespace StudentsRM.Service.Interface
{
    public interface ILecturerService
    {
        BaseResponseModel Create(CreateLecturerModel request, string roleName = null);
        BaseResponseModel Update(string lecturerId, UpdateLecturerViewModel update);
        BaseResponseModel Delete(string lecturerId);
        LecturerResponseModel GetLecturer(string lecturerId);
        LecturersResponseModel GetAll();
    }
}
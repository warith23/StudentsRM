using StudentsRM.Entities;
using StudentsRM.Models;
using StudentsRM.Models.Students;

namespace StudentsRM.Service.Interface
{
    public interface IStudentService
    {
        BaseResponseModel Create(CreateStudentModel request, string roleName = null);
        BaseResponseModel Update(UpdateStudentViewModel update, string studentId);
        BaseResponseModel Delete(string studentId);
        StudentResponseModel GetStudent(string studentId);
        StudentsResponseModel GetAllLecturerStudents();
        StudentsResponseModel GetAllLecturerStudentsForResults();
        StudentsResponseModel GetAll();
    }
}
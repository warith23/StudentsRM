using StudentsRM.Models;
using StudentsRM.Models.Student;

namespace StudentsRM.Service.Interface
{
    public interface IStudentService
    {
        BaseResponseModel Create(CreateStudentModel request, string roleName = null);
        BaseResponseModel Update(string studentId, UpdateStudentViewModel update);
        BaseResponseModel Delete(string studentId);
        StudentResponseModel GetStudent(string studentId);
        StudentsResponseModel GetAll();
    }
}
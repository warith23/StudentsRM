using StudentsRM.Models;
using StudentsRM.Models.Semester;

namespace StudentsRM.Service.Interface
{
    public interface ISemesterService
    {
        BaseResponseModel Create(CreateSemesterViewModel request);
        BaseResponseModel Update(string semesterId, UpdateSemesterViewModel update);
        BaseResponseModel Delete(string semesterId);
        SemesterResponseModel GetSemester(string semesterId);
        SemestersResponseModel GetAll();
    }
}
using StudentsRM.Models;
using StudentsRM.Models.Results;

namespace StudentsRM.Service.Interface
{
    public interface IResultService
    {
        BaseResponseModel Create(AddResultViewModel request);
        // BaseResponseModel Update(string resultId, UpdateResultViewModel update);
        BaseResponseModel Delete(string resultId);
        ResultResponseModel GetResult(string resultId);
        ResultsResponseModel GetAll();
    }
}
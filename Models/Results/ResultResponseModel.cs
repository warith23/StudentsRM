
namespace StudentsRM.Models.Results
{
    public class ResultResponseModel : BaseResponseModel
    {
        public ResultViewModel Data { get; set; }
    }

        public class ResultsResponseModel : BaseResponseModel
    {
        public List<ResultViewModel> Data { get; set; }
    }
}
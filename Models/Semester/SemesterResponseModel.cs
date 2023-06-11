
namespace StudentsRM.Models.Semester
{
    public class SemesterResponseModel : BaseResponseModel
    {
        public SemesterViewModel Data { get; set; }
    }
    public class SemestersResponseModel : BaseResponseModel
    {
        public List<SemesterViewModel> Data { get; set; }
    }
}
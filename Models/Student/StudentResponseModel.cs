

namespace StudentsRM.Models.Student
{
    public class StudentResponseModel : BaseResponseModel
    {
        public StudentViewModel Data { get; set; }
    }
        public class StudentsResponseModel : BaseResponseModel
    {
        public List<StudentViewModel> Data { get; set; }
    }
}
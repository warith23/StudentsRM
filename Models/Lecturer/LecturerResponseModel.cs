using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRM.Models.Lecturer
{
    public class LecturerResponseModel : BaseResponseModel
    {
       public LecturerViewModel Data { get; set; }    
    }

    public class LecturersResponseModel : BaseResponseModel
    {
        public List<LecturerViewModel> Data { get; set; }
    }
}
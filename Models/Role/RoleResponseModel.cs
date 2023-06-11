namespace StudentsRM.Models.Role
{
    public class RoleResponseModel : BaseResponseModel
    {
        public RoleViewModel Data { get; set; }
    }
    public class RolesResponseModel : BaseResponseModel
    {
        public List<RoleViewModel> Data { get; set; }
    }
}
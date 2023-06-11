using StudentsRM.Models;
using StudentsRM.Models.Role;

namespace StudentsRM.Service.Interface
{
    public interface IRoleService
    {
        BaseResponseModel Create(CreateRoleModel request);
        BaseResponseModel Update(string RoleId, UpdateRoleViewModel update);
        BaseResponseModel Delete(string RoleId);
        RoleResponseModel GetRole(string RoleId);
        RolesResponseModel GetAll();
    }
}
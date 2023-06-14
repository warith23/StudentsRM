using StudentsRM.Models.Auth;
using StudentsRM.Models.User;

namespace StudentsRM.Service.Interface
{
    public interface IUserService
    {
        UserResponseModel GetUser(string userId);
        // BaseResponseModel Register(SignUpViewModel request, string roleName = null);
        UserResponseModel Login(LoginViewModel request);
    }
}
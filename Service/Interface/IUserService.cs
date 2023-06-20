using StudentsRM.Models;
using StudentsRM.Models.Auth;
using StudentsRM.Models.User;

namespace StudentsRM.Service.Interface
{
    public interface IUserService
    {
        UserResponseModel GetUser(string userId);
        UserResponseModel Login(LoginViewModel request);
        BaseResponseModel UpdatePassword(UpdateUserViewModel update);
    }
}
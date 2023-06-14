using StudentsRM.Helper;
using StudentsRM.Models;
using StudentsRM.Models.Auth;
using StudentsRM.Models.User;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;

namespace StudentsRM.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public UserResponseModel GetUser(string userId)
        {
            var response = new UserResponseModel();
            var user = _unitOfWork.Users.GetUser(x => x.Id == userId);

            if (user is null)
            {
                response.Message = $"User with {userId} does not exist";
                return response;
            }

            response.Data = new UserViewModel
            {
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.RoleName,
            };
            response.Message = $"User successfully retrieved";
            response.Status = true;

            return response;
        }

        public UserResponseModel Login(LoginViewModel request)
        {
            var response = new UserResponseModel();
            try
            {
                var user = _unitOfWork.Users.GetUser(x =>  
                x.Email.ToLower() == request.Email.ToLower());

                if (user is null)
                {
                    response.Message = $"Account does not exist!";
                    return response;
                }

                string hashedPassword = HashingHelper.HashPassword(request.Password, user.HashSalt);

                if (!user.PasswordHash.Equals(hashedPassword))
                {
                    response.Message = $"Incorrect email or password!";
                    return response;
                }

                response.Data = new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    RoleName = user.Role.RoleName,
                };
                // if (user.Role.RoleName == "Student")
                // {
                //     var student = _unitOfWork.Students.Get(user.CheckUserId);
                // }
                response.Message = "Welcome";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message}";
                return response;
            }
        }


    }
}
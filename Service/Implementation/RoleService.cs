using StudentsRM.Models;
using StudentsRM.Models.Role;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;
using StudentsRM.Entities;
using System.Linq.Expressions;

namespace StudentsRM.Service.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel Create(CreateRoleModel request)
        {
            var response = new BaseResponseModel();
            var ifExist = _unitOfWork.Roles.Exists(c => c.RoleName == request.RoleName);

            if (ifExist)
            {
                response.Message = "Role already exist";
                return response;
            }

            var role = new Role
            {
                RoleName = request.RoleName,
                Description = request.Description,
                // DateCreated = DateTime.Now,
                // RegisteredBy = "Admin"
            };

            try
            {
                _unitOfWork.Roles.Create(role);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Role created successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Delete(string RoleId)
        {
            var response = new BaseResponseModel();
            var ifExist = _unitOfWork.Roles.Exists(c => c.Id == RoleId);

            if (!ifExist)
            {
                response.Message = "Role not found";
                return response;
            }

            var role = _unitOfWork.Roles.Get(RoleId);
            role.IsDeleted = true;

            try
            {
                _unitOfWork.Roles.Update(role);
                _unitOfWork.SaveChanges();
                response.Message = "Saved successfully";
                
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message}";
                return response;
            }
        }

        public RolesResponseModel GetAll()
        {
            var response = new RolesResponseModel();

            try
            {
                Expression<Func<Role,  bool>> expression = c => c.IsDeleted == false;
                var role = _unitOfWork.Roles.GetAll(expression);

                if (role is null || role.Count == 0)
                {
                    response.Message = "No roles found";
                    return response;
                }

                response.Data = role.Select(
                    role => new RoleViewModel
                    {
                        Id = role.Id,
                        RoleName = role.RoleName,
                        Description = role.Description
                    }
                ).ToList();
            }
            catch (System.Exception)
            {
                response.Message = "An error occurred";
                return response;
            }

            response.Message = "Success";
            response.Status = true;
            return response;
        }

        public RoleResponseModel GetRole(string RoleId)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel Update(string RoleId, UpdateRoleViewModel update)
        {
            throw new NotImplementedException();
        }
    }
}
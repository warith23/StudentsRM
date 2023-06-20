using StudentsRM.Models;
using StudentsRM.Models.Course;
using StudentsRM.Repository.Interface;
using StudentsRM.Service.Interface;
using StudentsRM.Entities;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentsRM.Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BaseResponseModel Create(CreateCourseModel request)
        {
            var response = new BaseResponseModel();
            var ifExist = _unitOfWork.Courses.Exists(c => c.Name == request.Name && c.IsDeleted == false);

            if (ifExist)
            {
                response.Message = "Course already registered";
                return response;
            }
            
            var course = new Course
            {
                Name = request.Name,
                RegisteredBy = "Admin",
                DateCreated = DateTime.Today
            };

            try
            {
                _unitOfWork.Courses.Create(course);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Course successfully registered on system";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"Failed to register course at this time {ex.Message}";
                return response;
            }
        }

        public BaseResponseModel Delete(string courseId)
        {
            var response = new BaseResponseModel();
            var ifExist = _unitOfWork.Courses.Exists(c => c.Id == courseId && !c.IsDeleted);

            if (!ifExist)
            {
                response.Message = "Course does not exist";
                response.Status = true;
                return response;
            }

            var course = _unitOfWork.Courses.Get(courseId);
            course.IsDeleted = true;

            try
            {
                _unitOfWork.Courses.Update(course);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Course successfully deleted";

                return response;
            }
            catch (System.Exception)
            {
                response.Message = "Failed to register course at this time";
                return response;
            }
        }

        public CoursesResponseModel GetAll()
        {
            var response = new CoursesResponseModel();
            
            try
            {
                Expression<Func<Course, bool>> expression = c => c.IsDeleted == false;
                var course = _unitOfWork.Courses.GetAll(expression);

                if (course is null || course.Count == 0)
                {
                    response.Message = "No course found";
                    return response;
                }
                
                response.Data = course.Select(
                    course => new CourseViewModel
                    {
                        Id = course.Id,
                        Name = course.Name
                        // RegisteredBy = course.RegisteredBy
                    }).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            return response;
        }

        public CourseResponseModel GetCourse(string courseId)
        {
            var response = new CourseResponseModel();

            Expression<Func<Course, bool>> expression = c => (c.Id == courseId) && (c.IsDeleted == false);

            var ifExist = _unitOfWork.Courses.Exists(expression);

            if (!ifExist)
            {
                response.Message = "Course is not registered on system";
                return response;
            } 
             
            var course = _unitOfWork.Courses.Get(courseId);
            response.Data = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name
            };
            response.Message = "Success";
            response.Status = true;
            return response;
        }

        public BaseResponseModel Update(string courseId, UpdateCourseViewModel update)
        {
            var response =  new BaseResponseModel();

            var ifExist = _unitOfWork.Courses.Exists(c => c.Id == courseId);
            if (!ifExist)
            {
                response.Message = "Course not found";
                return response;
            }

            var course = _unitOfWork.Courses.Get(courseId);
            course.Name = update.Name;
            // course.ModifiedBy = "Admin";

            try
            {
                _unitOfWork.Courses.Update(course);
                _unitOfWork.SaveChanges();
                response.Status = true;
                response.Message = "Updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred {ex.Message}";
                return response;
            }

        }
        
        public IEnumerable<SelectListItem> SelectCourses()
        {
            return _unitOfWork.Courses.SelectAll().Select(cou => new SelectListItem()
            {
                Text = cou.Name,
                Value = cou.Id
            }
            );
        }
    }
}
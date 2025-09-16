using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Users;
using WebApi.Models.Users;
using WebApi.Models;
using static Core.Exceptions.UserExceptions;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateUser(Guid userId, [FromBody] UserModel model)
        {
            try
            {
                var user = _createUserService.Create(userId, model.Name, model.Email, model.Age, model.Type, model.AnnualSalary, model.Tags);
                return Found(new UserData(user));
            }
            catch (UserAlreadyExistsException ex)
            {
                var error = new ApiError
                {
                    Message = ex.Message,
                    Code = "USER_ALREADY_EXISTS" // optional
                };
                return Request.CreateResponse(HttpStatusCode.Conflict, error);
            }

        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            try
            {
                var user = _getUserService.GetUser(userId);
                if (user == null)
                {
                    return DoesNotExist();
                }
                _updateUserService.Update(user, model.Name, model.Email, model.Age, model.Type, model.AnnualSalary, model.Tags);
                return Found(new UserData(user));
            }
            catch (ArgumentNullException ex)
            {
                var error = new ApiError
                {
                    Message = ex.Message,
                    Code = "NULL_ARGUMENT" // optional
                };

                /*
                 400 Bad Request.
                Reason: The client request was syntactically valid JSON, but semantically invalid for your business/domain rules (email cannot be null).
                 */

                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            _deleteUserService.Delete(user);
            return Found();
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            return Found(new UserData(user));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetUsers(int skip, int take, UserTypes? type = null, string name = null, string email = null, int? age = null)
        {
            var users = _getUserService.GetUsers(type, name, email)
                                       .Skip(skip).Take(take)
                                       .Select(q => new UserData(q))
                                       .ToList();
            return Found(users);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllUsers()
        {
            _deleteUserService.DeleteAll();
            return Found();
        }

        [Route("list/tag")]
        [HttpGet]
        public HttpResponseMessage GetUsersByTag(string tag)
        {
            // Call GetUsers with tag (null or empty tag will return all users)
            var users = _getUserService.GetUsers(tag: string.IsNullOrWhiteSpace(tag) ? null : tag)
                                       .Select(u => new UserData(u))
                                       .ToList();

            return Found(users);
        }
    }
}
using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using static Core.Exceptions.UserExceptions;

namespace Core.Services.Users
{
    [AutoRegister]
    public class CreateUserService : ICreateUserService
    {
        private readonly IUpdateUserService _updateUserService;
        private readonly IIdObjectFactory<User> _userFactory;
        private readonly IUserRepository _userRepository;
        private readonly IGetUserService _getUserService;

        public CreateUserService(IIdObjectFactory<User> userFactory, IUserRepository userRepository, IUpdateUserService updateUserService
            , IGetUserService getUserService)
        {
            _userFactory = userFactory;
            _userRepository = userRepository;
            _updateUserService = updateUserService;
            _getUserService = getUserService;

        }

        public User Create(Guid id, string name, string email, int age, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
        {
            var existing = _getUserService.GetUser(id);
            if (existing != null)
            {
                throw new UserAlreadyExistsException(id);
            }
            var user = _userFactory.Create(id);
            _updateUserService.Update(user, name, email, age, type, annualSalary, tags);
            _userRepository.Save(user);
            return user;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class UserExceptions
    {
        // Thrown when trying to create a user that already exists
        public class UserAlreadyExistsException : Exception
        {
            public Guid UserId { get; }

            public UserAlreadyExistsException(Guid userId)
                : base($"User with id {userId} already exists.")
            {
                UserId = userId;
            }
        }

        // Thrown when a user is not found
        public class UserNotFoundException : Exception
        {
            public Guid UserId { get; }

            public UserNotFoundException(Guid userId)
                : base($"User with id {userId} was not found.")
            {
                UserId = userId;
            }
        }

        // Add more user-related exceptions here as needed
    }
}

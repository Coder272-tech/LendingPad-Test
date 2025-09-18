using System;

namespace Core.Exceptions
{
    public static class OrderExceptions
    {
        public class OrderAlreadyExistsException : Exception
        {
            public OrderAlreadyExistsException(Guid id)
                : base($"Order with Id '{id}' already exists.")
            {
            }
        }

        public class OrderNotFoundException : Exception
        {
            public OrderNotFoundException(Guid id)
                : base($"Order with Id '{id}' was not found.")
            {
            }
        }
    }
}

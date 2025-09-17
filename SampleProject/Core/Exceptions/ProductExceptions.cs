using System;

namespace Core.Exceptions
{
    public static class ProductExceptions
    {
        public class ProductAlreadyExistsException : Exception
        {
            public ProductAlreadyExistsException(Guid id)
                : base($"A product with Id '{id}' already exists.")
            {
            }
        }

        public class ProductNotFoundException : Exception
        {
            public ProductNotFoundException(Guid id)
                : base($"No product found with Id '{id}'.")
            {
            }
        }
    }
}

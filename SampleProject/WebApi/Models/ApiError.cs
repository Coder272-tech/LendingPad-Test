using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public string Code { get; set; } // e.g., USER_ALREADY_EXISTS
    }
}
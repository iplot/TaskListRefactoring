using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskListRefactoring.Services
{
    public class ServiceResult
    {
        public string Errors { get; set; }

        public object Success { get; set; }
    }
}
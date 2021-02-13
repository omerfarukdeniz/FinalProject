using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success)
        {
            Success = success;
        }
        public Result(bool success, string message) : this(success) //iki constructorda çalışması için 
        {
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }

    }
}
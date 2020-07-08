using Oobj_Infra.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oobj_Infra.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message, StatusCodeEnum statusCodeEnum) : base(message)
        {
            StatusCode = statusCodeEnum;
        }

        public StatusCodeEnum StatusCode { get; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Oobj_Infra.Dto;
using Oobj_Infra.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oobj_Infra.Configurations
{
    public class CustomExceptionResult : ObjectResult
    {
        public CustomExceptionResult(APIResponseDto response, StatusCodeEnum statusCode = 0) : base(response)
        {
            StatusCode = statusCode == 0 ? (int)StatusCodeEnum.ExceptionError : (int)statusCode;
        }

        public CustomExceptionResult(string message, StatusCodeEnum statusCode = 0) : this(APIResponseDto.Fail(message), statusCode)
        {

        }
    }
}

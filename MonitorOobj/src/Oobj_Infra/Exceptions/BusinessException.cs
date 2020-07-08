using Oobj_Infra.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oobj_Infra.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException(string message) : base(message, StatusCodeEnum.BusinessError)
        {
        }
    }
}

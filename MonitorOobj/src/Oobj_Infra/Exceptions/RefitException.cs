using Oobj_Infra.Enumerators;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oobj_Infra.Exceptions
{
    public class RefitException : BaseException
    {
        public RefitException(string message, IApiResponse apiResponse = null) : base(message, StatusCodeEnum.RefitError)
        {
            Response = apiResponse;
        }

        public IApiResponse Response { get; set; }
    }
}

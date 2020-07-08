using Microsoft.AspNetCore.Mvc.Filters;
using Oobj_Infra.Configurations;
using Oobj_Infra.Dto;
using Oobj_Infra.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oobj_Api.Filters
{
    public class CustomValidationActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new CustomExceptionResult(APIResponseDto.FailValidationModelState(context.ModelState), StatusCodeEnum.ValidationError);
        }
    }
}

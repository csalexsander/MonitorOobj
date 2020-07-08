using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Oobj_Infra.Configurations;
using Oobj_Infra.Dto;
using Oobj_Infra.Enumerators;
using Oobj_Infra.Exceptions;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Oobj_Api.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IConfiguration configuration;
        public readonly string SentryDSN;

        public CustomExceptionFilterAttribute(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            SentryDSN = LocalEnvironment.SentryDsn;
        }

        public override void OnException(ExceptionContext context)
        {
            SendToSentry(context);

            context.Result = context.Exception switch
            {
                BusinessException businessException => new CustomExceptionResult(businessException.Message, businessException.StatusCode),
                ValidationException validationException => new CustomExceptionResult(APIResponseDto.Fail(validationException.Errors), validationException.StatusCode),
                RefitException refitException => new CustomExceptionResult(refitException.Message, refitException.StatusCode),
                _ => new CustomExceptionResult("An internal error has ocurred"),
            };
        }

        private void SendToSentry(ExceptionContext context)
        {
            using (SentrySdk.Init(SentryDSN))
            {
                var internalIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList?.Where(c => c.AddressFamily == AddressFamily.InterNetwork).ToString();

                SentrySdk.AddBreadcrumb($"{Environment.MachineName ?? string.Empty} - {internalIP ?? string.Empty }", "Machine Identification");

                SentrySdk.CaptureException(context.Exception);
            }
        }
    }
}

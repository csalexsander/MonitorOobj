using System;
using System.Collections.Generic;
using System.Text;

namespace Oobj_Infra.Configurations
{
    public static class LocalEnvironment
    {
        public static string SentryDsn = Environment.GetEnvironmentVariable("SentryDsn");
        public static string OobjBaseUrl = Environment.GetEnvironmentVariable("OobjBaseUrl");
    }
}

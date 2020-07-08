using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oobj_Service.Interfaces
{
    public interface ICnpjService
    {
        Task<IDictionary<string, string>> GetCpnjResponse();
        Task<IDictionary<string, string>> GetCnpjRecive();
    }
}

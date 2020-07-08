using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oobj_Service.Interfaces
{
    public interface IMemoryService
    {
        Task<IDictionary<string, string>> GetAvaliableMemory();
    }
}

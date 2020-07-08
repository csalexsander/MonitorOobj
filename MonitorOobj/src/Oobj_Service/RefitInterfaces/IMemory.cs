using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oobj_Service.RefitInterfaces
{
    public interface IMemory
    {
        [Get("memoriaDisponivel")]
        Task<ApiResponse<Dictionary<string, string>>> GetAvaliableMemory();
    }
}

using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oobj_Service.RefitInterfaces
{
    public interface ICnpj
    {
        [Get("respostasPorCnpj")]
        Task<ApiResponse<Dictionary<string, string>>> GetCpnjResponse();

        [Get("recebePorCnpj")]
        Task<ApiResponse<Dictionary<string, string>>> GetCnpjRecive();
    }
}

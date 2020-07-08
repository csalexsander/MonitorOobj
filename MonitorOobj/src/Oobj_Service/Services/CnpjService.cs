using Oobj_Service.Interfaces;
using Oobj_Service.RefitInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oobj_Service.Services
{
    public class CnpjService : ServiceBase<ICnpj>, ICnpjService
    {
        public CnpjService(Configuracoes configuracoes) : base(configuracoes)
        {
        }

        public async Task<IDictionary<string,string>> GetCpnjResponse()
        {
            return HandleReturn<string, string>(await restService.GetCpnjResponse());
        }

        public async Task<IDictionary<string, string>> GetCnpjRecive()
        {
            return HandleReturn<string, string>(await restService.GetCnpjRecive());
        }        
    }
}

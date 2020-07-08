using Oobj_Service.Interfaces;
using Oobj_Service.RefitInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oobj_Service.Services
{
    public class MemoryService : ServiceBase<IMemory>, IMemoryService
    {
        public MemoryService(Configuracoes configuracoes) : base(configuracoes)
        {

        }

        public async Task<IDictionary<string, string>> GetAvaliableMemory()
        {
            return HandleReturn<string, string>(await restService.GetAvaliableMemory());
        }
    }
}

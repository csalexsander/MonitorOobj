using Oobj_Infra.Enumerators;
using Oobj_Infra.Exceptions;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oobj_Service.Services
{
    public class ServiceBase<T>
    {
        private readonly Configuracoes configuracoes;
        protected readonly T restService;

        public ServiceBase(Configuracoes configuracoes)
        {
            this.configuracoes = configuracoes ?? throw new ArgumentNullException(nameof(configuracoes));
            this.restService = RestService.For<T>(configuracoes.UrlBase);
        }

        protected IDictionary<T, Z> HandleReturn<T, Z>(ApiResponse<Dictionary<string, string>> response)
        {
            if (!response.IsSuccessStatusCode)
                throw new RefitException(response.ReasonPhrase, apiResponse: response);

            return (IDictionary<T, Z>)response.Content;
        }
    }
}

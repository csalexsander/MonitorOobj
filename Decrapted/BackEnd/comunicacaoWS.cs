using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MonitorOobj.Models;
using Newtonsoft.Json;

namespace MonitorOobj.BackEnd
{
    public class ComunicacaoWS
    {
        private string BuscaJson(string desejado)
        {
            string json = "";
            string url = @"http://redis.oobj-dfe.com.br/" + desejado;
            using (var wc = new HttpClient())
            {
                json = wc.GetStringAsync(url).Result;
            }

            return json;
        }

        public List<Mensagens> capturarRecebe()
        {
            var mensagens = RemoveCaracteresIndesejados(BuscaJson("recebePorCnpj")).Split(',');


            var lista = PopulaVariavelAndLista(mensagens);

            return lista;
        }

        public Memoria capturaMemoria()
        {
            var memoriajson = RemoveCaracteresIndesejados(BuscaJson("memoriaDisponivel"));

            return PopulaVariavelMemoria(memoriajson);
        }

        public List<Mensagens> capturarResposta()
        {
            var mensagens = RemoveCaracteresIndesejados(BuscaJson("respostasPorCnpj")).Split(',');


            var lista = PopulaVariavelAndLista(mensagens);

            return lista;
        }

        public int[] capturaQuantidadetipoCNPJ()
        {
            int A = 0, B = 0, C = 0;
            var recebe = capturarRecebe();
            var resposta = capturarResposta();

            foreach (var item in recebe)
            {
                if(int.Parse(item.Msg) > 2000)
                {
                    A += 1;
                }
                else
                {
                    if (int.Parse(item.Msg) < 1000)
                    {
                        C += 1;
                    }
                    else
                    {
                        B += 1;
                    }

                }
            }

            int[] array = new int[] { A, B, C };

            return array;
        }

        public int QuantidadeResposta()
        {
            var resposta = capturarResposta();

            int j = 0;

            foreach (var item in resposta)
            {
                j += int.Parse(item.Msg);
            }

            return j;
        }

        public int QuantidadeTotal() {

            return QuantidadeRecebe() + QuantidadeResposta();
        }

        public int QuantidadeRecebe()
        {
            var recebe = capturarRecebe();

            int j = 0;

            foreach (var item in recebe)
            {
                j += int.Parse(item.Msg);
            }

            return j;
        }

        private string RemoveCaracteresIndesejados(string json)
        {
            json = json.Replace("{", "");
            json = json.Replace("}", "");
            json = json.Replace('"', ' ');
            json = json.Replace(" ", "");

            return json;
        }

        private List<Mensagens> PopulaVariavelAndLista(string[] split, bool calcula = false)
        {
            List<Mensagens> lista = new List<Mensagens>();
            foreach (var t in split)
            {
                var msg = new Mensagens();
                var temp = t.Split(':');
                msg.Fila = temp[0];
                msg.Msg = temp[2];
                msg.CNPJ = temp[1];
                lista.Add(msg);
            }

            return lista;
        }

        private Memoria PopulaVariavelMemoria(string memoriaJson)
        {
            Memoria memoria = new Memoria();
            var memory = memoriaJson.Split(',');
            memoria.MemoriaAtual = int.Parse(memory[0].Split(':')[1]);
            memoria.MemoriaAtualString = memory[1].Split(':')[1];
            memoria.MemoriaPico = int.Parse(memory[2].Split(':')[1]);
            memoria.MemoriaPicoString = memory[3].Split(':')[1];

            return memoria;
        }
    }
}

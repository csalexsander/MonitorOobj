using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MonitorOobj.Models;
using System.Net.Http;

namespace MonitorOobj.Controllers
{
    public class PainelController : Controller
    {
        public Memoria memoria;
        public int CNPJ_A { get; set; }
        public int CNPJ_B { get; set; }
        public int CNPJ_C { get; set; }
        public int MensagensTot { get; set; }
        public string recebeJson { get; set; }
        public string respostaJson { get; set; }
        public string memoriaJson { get; set; }

        // GET: Painel
        public ActionResult Index()
        {
            CNPJ_A = CNPJ_B = CNPJ_C = MensagensTot = 0;
            memoria = new Memoria();
            


            BuscaJson(respostaJson, recebeJson, memoriaJson);

            RemoveCaracteresIndesejados();

            memoriaJson = RemoveCaracteresIndesejadosMemoria(memoriaJson);

            PopulaVariavelMemoria(memoriaJson);

            var splitRecebe = recebeJson.Split(',');
            var splitresposta = respostaJson.Split(',');

            var filaRecebe = new List<Mensagens>();
            var filaresposta = new List<Mensagens>();

            PopulaVariavelRecebe(splitRecebe, ref filaRecebe);
            PopulaVariavelresposta(splitresposta, ref filaresposta);

            ViewBag.memoria = memoria;
            ViewBag.Recebe = filaRecebe;
            ViewBag.resposta = filaresposta;
            ViewBag.MensagensTot = MensagensTot;
            ViewBag.cnpjA = CNPJ_A;
            ViewBag.cnpjB = CNPJ_B;
            ViewBag.cnpjC = CNPJ_C;
            return View("Index");
        }

        private void PopulaVariavelMemoria(string memoriaJson)
        {
            var memory = memoriaJson.Split(',');

            memoria.MemoriaAtual = int.Parse(memory[0].Split(':')[1]);
            memoria.MemoriaAtualString = memory[1].Split(':')[1];
            memoria.MemoriaPico = int.Parse(memory[2].Split(':')[1]);
            memoria.MemoriaPicoString = memory[3].Split(':')[1];
        }

        private static string RemoveCaracteresIndesejadosMemoria(string memoriaJson)
        {
            memoriaJson = memoriaJson.Replace("{", "");
            memoriaJson = memoriaJson.Replace("}", "");
            memoriaJson = memoriaJson.Replace('"', ' ');
            memoriaJson = memoriaJson.Replace(" ", "");
            return memoriaJson;
        }

        private void PopulaVariavelRecebe(string[] split, ref List<Mensagens> fila)
        {
            foreach (var t in split)
            {
                var msg = new Mensagens();
                var temp = t.Split(':');
                msg.Fila = temp[0];
                msg.Msg = temp[2];
                if (int.Parse(msg.Msg) > 2000)
                {
                    CNPJ_A++;
                }
                else if (int.Parse(msg.Msg) > 1000)
                {
                    CNPJ_B++;
                }
                else
                {
                    CNPJ_C++;
                }
                MensagensTot += int.Parse(msg.Msg);
                msg.CNPJ = temp[1];
                fila.Add(msg);
            }
        }

        private void PopulaVariavelresposta(string[] split, ref List<Mensagens> fila)
        {
            foreach (var t in split)
            {
                var msg = new Mensagens();
                var temp = t.Split(':');
                msg.Fila = temp[0];
                msg.Msg = temp[2];
                if (int.Parse(msg.Msg) > 2000)
                {
                    CNPJ_A++;
                }
                else if (int.Parse(msg.Msg) > 1000)
                {
                    CNPJ_B++;
                }
                else
                {
                    CNPJ_C++;
                }
                MensagensTot += int.Parse(msg.Msg);
                msg.CNPJ = temp[1];
                fila.Add(msg);
            }
        }

        private void RemoveCaracteresIndesejados()
        {
            recebeJson = recebeJson.Replace("{", "");
            recebeJson = recebeJson.Replace("}", "");
            recebeJson = recebeJson.Replace('"', ' ');
            respostaJson = respostaJson.Replace("{", "");
            respostaJson = respostaJson.Replace("}", "");
            respostaJson = respostaJson.Replace('"', ' ');

        }

        private void BuscaJson(string resposta, string recebe, string memoria)
        {
            using (var wc = new HttpClient())
            {
                respostaJson = wc.GetStringAsync(@"http://redis.oobj-dfe.com.br/respostasPorCnpj").Result;
                recebeJson = wc.GetStringAsync(@"http://redis.oobj-dfe.com.br/recebePorCnpj").Result;
                memoriaJson = wc.GetStringAsync(@"http://redis.oobj-dfe.com.br/memoriaDisponivel").Result;
            }

        }
    }
}
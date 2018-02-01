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
            Memoria();
            Estatisticas();
            Recebe();
            Resposta();
            return View("Index", "LayoutMonitor");
        }

        // GET: PartialView
        public PartialViewResult TabelaPartialViewRecebe()
        {
           
            Recebe();
           
            return PartialView("TabelaPartialViewRecebe");
        }

        // GET: PartialView
        public PartialViewResult TabelaPartialViewResposta()
        {

            Resposta();

            return PartialView("TabelaPartialViewResposta");
        }

        private void Resposta()
        {
           var filaresposta = new List<Mensagens>();
                                 
            respostaJson = BuscaJson("respostasPorCnpj");

            respostaJson = RemoveCaracteresIndesejados(respostaJson);

            var splitresposta = respostaJson.Split(',');

            PopulaVariavelAndLista(splitresposta, ref filaresposta);
           
            var listaBaseResposta = RetornaListaBase(filaresposta);
           
            ViewBag.unidadeResposta = filaresposta;
            
            ViewBag.empresaResposta = listaBaseResposta;
        }

        private void Recebe()
        {
            var filaRecebe = new List<Mensagens>();

            recebeJson = BuscaJson("recebePorCnpj");

            recebeJson = RemoveCaracteresIndesejados(recebeJson);

            var splitRecebe = recebeJson.Split(',');

            PopulaVariavelAndLista(splitRecebe, ref filaRecebe);

            var listaBaseRecebe = RetornaListaBase(filaRecebe);

            ViewBag.unidadeRecebe = filaRecebe;

            ViewBag.empresaRecebe = listaBaseRecebe;
        }

        private void Memoria()
        {
            memoriaJson = BuscaJson("memoriaDisponivel");

            memoria = new Memoria();

            memoriaJson = RemoveCaracteresIndesejados(memoriaJson);

            PopulaVariavelMemoria(memoriaJson);

            ViewBag.memoria = memoria;
        }

        private void PopulaVariavelMemoria(string memoriaJson)
        {
            var memory = memoriaJson.Split(',');

            memoria.MemoriaAtual = int.Parse(memory[0].Split(':')[1]);
            memoria.MemoriaAtualString = memory[1].Split(':')[1];
            memoria.MemoriaPico = int.Parse(memory[2].Split(':')[1]);
            memoria.MemoriaPicoString = memory[3].Split(':')[1];
        }

        private void PopulaVariavelAndLista(string[] split, ref List<Mensagens> fila, bool calcula = false)
        {
            foreach (var t in split)
            {
                var msg = new Mensagens();
                var temp = t.Split(':');
                msg.Fila = temp[0];
                msg.Msg = temp[2];

                if (calcula) { 
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
                }

                msg.CNPJ = temp[1];
                fila.Add(msg);
            }
        }

        private string RemoveCaracteresIndesejados(string json)
        {
            json = json.Replace("{", "");
            json = json.Replace("}", "");
            json = json.Replace('"', ' ');
            json = json.Replace(" ", "");

            return json;
        }

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
            
        private List<Mensagens> RetornaListaBase(List<Mensagens> listaMensagens)
        {
            var listaBase = new List<Mensagens>();
            var listaManobra = new List<Mensagens>();
            listaManobra = listaMensagens;

            foreach (var mensagem in listaManobra)
            {
                if (listaBase.Exists(x => x.CNPJ == mensagem.CNPJ.Substring(0, 8)))
                {
                    var msg = listaBase.Find(x => x.CNPJ == mensagem.CNPJ.Substring(0, 8));
                    int count1 = int.Parse(msg.Msg);
                    int count2 = int.Parse(mensagem.Msg);
                    count1 += count2;
                    msg.Msg = count1.ToString();
                }
                else
                {
                    var Fila = mensagem.Fila;
                    var CNPJ = mensagem.CNPJ.Substring(0, 8);
                    var MSg = mensagem.Msg;
                    listaBase.Add(new Mensagens()
                    {
                        CNPJ = CNPJ,
                        Msg = MSg,
                        Fila = Fila
                    });        
                }
            }

            return listaBase;
        }

        private void Estatisticas()
        {
            CNPJ_A = CNPJ_B = CNPJ_C = MensagensTot = 0;

            var json = BuscaJson("respostasPorCnpj");
            json += "," + BuscaJson("recebePorCnpj");

            json = RemoveCaracteresIndesejados(json);

            var splitjson = json.Split(',');

            var listajson = new List<Mensagens>();

            PopulaVariavelAndLista(splitjson, ref listajson, true);

            ViewBag.cnpjB = CNPJ_B;
            ViewBag.cnpjC = CNPJ_C;
            ViewBag.cnpjA = CNPJ_A;
            ViewBag.MensagensTot = MensagensTot;
        }

    }
}
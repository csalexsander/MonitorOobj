using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonitorOobj.BackEnd;
using MonitorOobj.Models;

namespace MonitorOobj.Controllers
{
    public class MonitoramentoController : Controller
    {
        ComunicacaoWS ws = new ComunicacaoWS();

        public ActionResult Index()
        {
            return View("Index");
        }

        public int qtdRecebe()
        {
            return ws.QuantidadeRecebe();
        }

        public int qtdResposta()
        {
            return ws.QuantidadeResposta();
        }

        public int qtdTotal()
        {
            return ws.QuantidadeTotal();
        }

        public int percentMemoria()
        {
            var memoria = ws.capturaMemoria();

            double percent = (double) memoria.MemoriaAtual / memoria.MemoriaMaxima;

            int result = (int)(percent * 100);

            return result;
        }

        public int[] percentTipos()
        {
            var tipos = ws.capturaQuantidadetipoCNPJ();

            int total = tipos[0] + tipos[1] + tipos[2];

            double[] percent = new double[3];

            percent[0] = (double)tipos[0] / total;
            percent[1] = (double)tipos[1] / total;
            percent[2] = (double)tipos[2] / total;

            tipos[0] = ((int)(percent[0] * 100)) + 1;
            tipos[1] = ((int)(percent[1] * 100)) + 1;
            tipos[2] = (int)(percent[2] * 100);

            return tipos;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MonitorOobj.Controllers
{
    public class MonitoramentoController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
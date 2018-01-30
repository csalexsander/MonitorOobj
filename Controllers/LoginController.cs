using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitorOobj.Context;
using MonitorOobj.Models;

namespace MonitorOobj.Controllers
{
    public class LoginController : Controller
    {
        private readonly Contexto _context;
        public string Email { get; set; }
        public string Senha { get; set; }

        public LoginController(Contexto context)
        {
            _context = context;    
        }

        // GET: Login/Create
        public ActionResult Index()
        {
            var user = _context.Login.Single(b => b.Email == Email);

            if (String.IsNullOrEmpty(user.Email) && user.Senha == Senha)
            {
                //Login com sucesso
            }
            else
            {
                //Usuario ou senha incorreto
            }
            
            return View();
        }

    }
}

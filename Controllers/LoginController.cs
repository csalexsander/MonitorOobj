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

        private Login login;

        public LoginController(Contexto context)
        {
            _context = context;
            login = new Login();
        }

        // GET: Login/Create
        public ActionResult Index()
        {
            return View(login);
        }

        public ActionResult Logar(Login Model)
        {
            var user = _context.Login.FirstOrDefault(b => b.Email.Equals(Model.Email));

            if (user != null)
            {
                if (user.Senha.Equals(Model.Senha))
                {
                    Console.WriteLine("Login com sucesso");
                    return RedirectPermanent("/Painel");
                }
                else
                {
                    Console.WriteLine("Usuario ou senha incorretos");
                }
            }
            else
            {
                Console.WriteLine("Usuario ou senha incorretos");
            }

            return RedirectPermanent("Index");
        }

        public ActionResult ResetSenha()
        {
            return View();
        }
    }
}

using MonitorOobj.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MonitorOobj.Models
{
    public static class InicializaDB
    {
        public static void Initialize(Contexto _contexto)
        {
            try
            {
                _contexto.Database.EnsureCreated();

                if (_contexto.Login.Any())
                {
                    return;
                }

                var login = new Login
                {
                    Email = "alexsander.camargo@oobj.com.br",
                    Bloqueado = false,
                    DataCadastro = DateTime.Now,
                    Senha = "123456",
                    UltimoAcesso = DateTime.Now
                };

                _contexto.Login.Add(login);

                _contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

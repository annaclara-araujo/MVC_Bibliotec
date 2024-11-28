using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bibliotec.Contexts;
using Bibliotec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bibliotec_mvc.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        // Criando um obj na classe Contex
        Context context = new Context();

        // o metodo esta retornando a View 
        public IActionResult Index()
        {
            //pegar as informacoes da session, que sao necessarias para que apareça os detalhes 
            int id = int.Parse(HttpContext.Session.GetString("UsuarioID")!);
            ViewBag.admin = HttpContext.Session.GetString("Admin")!;

System.Console.WriteLine(ViewBag.admin);

            //busquei o usuario que esta logado
            Usuario usuarioEncontrado = context.Usuario.FirstOrDefault(usuario => usuario.UsuarioID == id)!;
            //se nn for encontrado ninguem 
            if (usuarioEncontrado == null)
            {
                return NotFound();
            }

            //procurar o curso que meu usurioEncontardo esta cadastrado 

            Curso cursoEncontrado = context.Curso.FirstOrDefault(curso => curso.CursoID == usuarioEncontrado.CursoID)!;

            if(cursoEncontrado == null)
            {
                ViewBag.Curso = "O usuário não possui curso cadastrado!";
                
            }else {
                // Preciso que vc mande o nome do curso para View
                ViewBag.Curso = cursoEncontrado.Nome;
            }

            ViewBag.Nome = usuarioEncontrado.Nome;
            ViewBag.Email = usuarioEncontrado;
            ViewBag.Contato = usuarioEncontrado.Contato; 
            ViewBag.DtNasc = usuarioEncontrado.DtNascimento.ToString("dd/MM/yyyy");













            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
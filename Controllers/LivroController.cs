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
    public class LivroController : Controller
    {
        private readonly ILogger<LivroController> _logger;

        public LivroController(ILogger<LivroController> logger)
        {
            _logger = logger;
        }

        Context context = new Context();
        public IActionResult Index()
        {
            ViewBag.admin = HttpContext.Session.GetString("Admin")!;

            //criar uma lista de livros
            List<Livro> listaLivros = context.Livro.ToList();

            //verificar se o livro tem reserva ou não
            var livrosReservados = context.LivroReserva.ToDictionary(livro => livro.LivroID, livror => livror.DtReserva);

            ViewBag.Livros = listaLivros;
            ViewBag.LivrosComReserva = livrosReservados;

            return View();
        }

        // metodo que retorna a tela de cadastro
        [Route("Cadastro")]
        public IActionResult Cadastro()
        {
            ViewBag.admin = HttpContext.Session.GetString("Admin")!;

            ViewBag.Categorias = context.Categoria.ToList();
            //retorna a view de cadastro:
            return View();
        }

        //metodo para cadastrar o livro:
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Livro novoLivro = new Livro();

            //O que o meu usuario escrevr no formulario sera atribuido ao novoLivro

            novoLivro.Nome = form["Nome"].ToString();
            novoLivro.Escritor = form["Escritor"].ToString();
            novoLivro.Idioma = form["Idioma"].ToString();
            novoLivro.Editora = form["Ediitora"].ToString();
            novoLivro.Editora = form["Descrição"].ToString();

            //img
            context.Livro.Add(novoLivro);

            context.SaveChanges();

            return RedirectToAction("Cadastro");
        }




        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}
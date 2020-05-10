using Blog.RequestModels.AdmPostagem;
using Microsoft.AspNetCore.Mvc;
using PWABlog;
using PWABlog.Models.Blog.Postagem;
using System;

namespace Blog.ViewMoldels
{
    public class AdmPostagemController : Controller
    {
        private readonly Database context;
        private readonly PostagemOrmService postagemOrmService;

        public AdmPostagemController(Database context, PostagemOrmService PostagemOrmService)
        {
            this.context = context;
            postagemOrmService = PostagemOrmService;
        }

        // GET: AdminPostagem
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminPostagem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPostagem/Create
        [HttpPost]
        public IActionResult Create(AdmPostagemCreate request)
        {
            var titulo = request.Titulo;
            var categoria = request.CategoriaId;
            var autor = request.AutorId;
            var texto = request.Texto;

            try
            {
                postagemOrmService.Create(titulo, categoria, autor, texto);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminPostagem/Details/5
        public IActionResult Details(int? id)
        {
            return View();
        }

        // GET: AdminPostagem/Edit/5
        public IActionResult Edit(int? id)
        {
            return View();
        }

        // POST: AdminPostagem/Edit/5
        [HttpPost]
        public IActionResult Edit(AdmPostagemEdit request)
        {
            var id = request.Id;
            var titulo = request.Titulo;
            var categoria = request.CategoriaId;
            var autor = request.AutorId;
            var texto = request.Texto;

            try
            {
                postagemOrmService.Edit(id, titulo, categoria, autor, texto);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminPostagem/Delete/5
        public IActionResult Delete(int? id)
        {
            return View();
        }

        // POST: AdminPostagem/Delete/5
        [HttpPost]
        public IActionResult Delete(AdmPostagemDelete request)
        {
            var id = request.Id;

            try
            {
                postagemOrmService.Delete(id);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

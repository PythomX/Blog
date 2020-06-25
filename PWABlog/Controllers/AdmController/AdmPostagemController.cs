using Blog.RequestModels.AdmPostagem;
using Microsoft.AspNetCore.Mvc;
using PWABlog;
using PWABlog.Models.Blog.Postagem;
using System;
using Microsoft.AspNetCore.Authorization;
using PWABlog.ViewModels.Admin;

namespace Blog.ViewMoldels
{
    [Authorize]
    public class AdmPostagemController : Controller
    {
        private readonly PostagemOrmService postagemOrmService;

        public AdmPostagemController(PostagemOrmService PostagemOrmService)
        {
            postagemOrmService = PostagemOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdmPostagensListarViewModel model = new AdmPostagensListarViewModel();
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Detalhar(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Criar()
        {
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Criar(AdmPostagemCreate request)
        {
            var titulo = request.Texto;
            var descricao = request.Descricao;
            var idAutor = request.IdAutor;
            var idCategoria = request.IdCategoria;
            var texto = request.Texto;
            var dataExibicao = DateTime.Parse(request.DataExibicao);
            
            try {                        
                postagemOrmService.Create(titulo, idCategoria, idAutor, descricao,  dataExibicao);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Criar");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            ViewBag.id = id;
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Editar(AdmPostagemEdit request)
        {
            var id = request.Id;
            var titulo = request.Texto;
            var descricao = request.Descricao;
            var idCategoria = Convert.ToInt32(request.IdCategoria);
            var texto = request.Texto;
            var dataExibicao = DateTime.Parse(request.DataExibicao);

            try {
                postagemOrmService.Edit(id, titulo, descricao, idCategoria, texto, dataExibicao);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Editar", new {id = id});
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.id = id;
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Remover(AdmPostagemDelete request)
        {
            var id = request.Id;

            try {
                postagemOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Remover", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}

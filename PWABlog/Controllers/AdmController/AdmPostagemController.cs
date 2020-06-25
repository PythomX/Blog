using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Postagem;
using PWABlog.RequestModels.AdmPostagem;
using PWABlog.ViewModels.Adm;

namespace PWABlog.Controllers.AdmController
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
            AdmPostagemListarViewModel model = new AdmPostagemListarViewModel();
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Detalhar(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Create(AdmPostagemCreateRequestModel request)
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
                return RedirectToAction("Create");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.id = id;
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Edit(AdmPostagemEditRequestModel request)
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
                return RedirectToAction("Edit", new {id = id});
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
        public RedirectToActionResult Delete(AdmPostagemDeleteRequestModel request)
        {
            var id = request.Id;

            try {
                postagemOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Delete", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}

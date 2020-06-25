using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;
using PWABlog.RequestModels.AdmAutor;
using PWABlog.ViewModels.Adm;

namespace PWABlog.Controllers.AdmController
{
    [Authorize]
    public class AdmAutorController : Controller
    {
        private readonly AutorOrmService autorOrmService;

        public AdmAutorController( AutorOrmService AutorOrmService)
        {
            autorOrmService = AutorOrmService;
        }

        [HttpGet]
        [Route("adm/autores")]
        [Route("adm/autores/listar")]
        public IActionResult Listar()
        {
            AdmAutorListarViewModel model = new AdmAutorListarViewModel();
            
            return View(model);
        }
        
        [HttpGet]
        [Route("adm/autores/{id}")]
        public IActionResult Detalhar(int id)
        {
            return View();
        }

        [HttpGet]
        [Route("adm/autores/criar")]
        public IActionResult Create()
        {
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]
        [Route("adm/autores/criar")]
        public RedirectToActionResult Create(AdmAutorCreateRequestModel request)
        {
            var nome = request.Nome;

            try {
                autorOrmService.Create(nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Create");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("adm/autores/editar/{id}")]
        public IActionResult Edit(int id)
        {
            ViewBag.id = id;
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]
        [Route("adm/autores/editar/{id}")]
        public RedirectToActionResult Edit(AdmAutorEditRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try {
                autorOrmService.Edit(id, nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Edit", new {id = id});
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        [Route("adm/autores/remover/{id}")]
        public IActionResult Delete(int id)
        {
            ViewBag.id = id;
            ViewBag.erro = TempData["erro-msg"];

            return View();
        }

        [HttpPost]
        [Route("adm/autores/remover/{id}")]
        public RedirectToActionResult Delete(AdmAutorDeleteRequestModel request)
        {
            var id = request.Id;

            try {
                autorOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Delete", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}

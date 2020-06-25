using System;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Categoria;
using PWABlog.RequestModels.AdmCategoria;
using PWABlog.ViewModels.Adm;

namespace PWABlog.Controllers.AdmController
{
    public class AdmCategoriaController : Controller
    {
        private readonly CategoriaOrmService categoriaOrmService;

        public AdmCategoriaController(CategoriaOrmService categoriaOrmService)
        {
            this.categoriaOrmService = categoriaOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdmCategoriListarViewModel model = new AdmCategoriListarViewModel();
            
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
        public RedirectToActionResult Create(AdmCategoriaCreateRequestModel request)
        {
            var nome = request.Nome;

            try {
                categoriaOrmService.Create(nome);
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
        public RedirectToActionResult Edit(AdmCategoriaEditRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try {
                categoriaOrmService.Edit(id, nome);
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
        public RedirectToActionResult Delete(AdmCategoriaDeleteRequestModel request)
        {
            var id = request.Id;

            try {
                categoriaOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Delete", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}

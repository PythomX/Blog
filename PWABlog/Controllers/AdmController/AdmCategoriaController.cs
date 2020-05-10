using Microsoft.AspNetCore.Mvc;
using Blog.RequestModels.AdminCategoria;
using System;
using PWABlog;
using PWABlog.Models.Blog.Categoria;

namespace Blog.ViewMoldels
{
    public class AdmCategoriaController : Controller
    {
        private readonly Database context;
        private readonly CategoriaOrmService categoriaOrmService;

        public AdmCategoriaController(Database context, CategoriaOrmService categoriaOrmService)
        {
            this.context = context;
            this.categoriaOrmService = categoriaOrmService;
        }

        // GET: AdminCategoria
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminCategoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminCategoria/Create
        [HttpPost]
        public IActionResult Create(AdmCategoriaCreate request)
        {
            var nome = request.Nome;

            try {
                categoriaOrmService.Create(nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminCategoria/Edit/5
        public IActionResult Edit(int? id)
        {
            return View();
        }

        // POST: AdminCategoria/Edit/5
        [HttpPost]
        public IActionResult Edit(AdmCategoriaEdit request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try {
                categoriaOrmService.Edit(id, nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminCategoria/Delete/5
        public IActionResult Delete(int? id)
        {
            return View();
        }

        // POST: AdminCategoria/Delete/5
        [HttpPost]
        public IActionResult Delete(AdmCategoriaDelete request)
        {
            var id = request.Id;

            try {
                categoriaOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

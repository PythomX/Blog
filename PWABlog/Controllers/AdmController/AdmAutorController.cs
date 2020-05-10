using Microsoft.AspNetCore.Mvc;
using System;
using Blog.Models.Blog.Autor;
using Blog.RequestModels.AdminAutor;
using PWABlog;

namespace Blog.ViewMoldels
{
    public class AdmAutorController : Controller
    {
        private readonly Database context;
        private readonly AutorOrmService autorOrmService;

        public AdmAutorController(Database context, AutorOrmService AutorOrmService)
        {
            this.context = context;
            autorOrmService = AutorOrmService;
        }

        // GET: AdminAutor
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminAutor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminAutor/Create
        [HttpPost]
        public IActionResult Create(AdmAutorCreate request)
        {
            var nome = request.Nome;

            try {
                autorOrmService.Create(nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminAutor/Edit/5
        public IActionResult Edit(int? id)
        {
            return View();
        }

        // POST: AdminAutor/Edit/5
        [HttpPost]
        public IActionResult Edit(AdmAutorEdit request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try {
                autorOrmService.Edit(id, nome);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminAutor/Delete/5
        public IActionResult Delete(int? id)
        {
            return View();
        }

        // POST: AdminAutor/Delete/5
        [HttpPost]
        public IActionResult Delete(AdmAutorDelete request)
        {
            var id = request.Id;

            try {
                autorOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Delete), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

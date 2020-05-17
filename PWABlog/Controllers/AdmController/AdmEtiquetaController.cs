using Microsoft.AspNetCore.Mvc;
using System;
using Blog.Models.Blog.Etiqueta;
using PWABlog;
using Blog.RequestModels.AdminEtiqueta;

namespace Blog.ViewMoldels
{
    public class AdminEtiquetaController : Controller
    {
        private readonly Database context;
        private readonly EtiquetaOrmService EtiquetaOrmService;

        public AdminEtiquetaController(Database context, EtiquetaOrmService EtiquetaOrmService)
        {
            this.context = context;
            this.EtiquetaOrmService = EtiquetaOrmService;
        }

        // GET: AdminEtiqueta
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminEtiqueta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminEtiqueta/Create
        [HttpPost]
        public IActionResult Create(AdmEtiquetaCreate request)
        {
            var nome = request.Nome;

            try
            {
                EtiquetaOrmService.Create(nome);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminEtiqueta/Edit/5
        public IActionResult Edit(int? id)
        {
            return View();
        }

        // POST: AdminEtiqueta/Edit/5
        [HttpPost]
        public IActionResult Edit(AdmEtiquetaEdit request)
        {
            var id = request.Id;
            var nome = request.Nome;

            try
            {
                EtiquetaOrmService.Edit(id, nome);
            }
            catch (Exception exception)
            {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AdminEtiqueta/Delete/5
        public IActionResult Delete(int? id)
        {
            return View();
        }

        // POST: AdminEtiqueta/Delete/5
        [HttpPost]
        public IActionResult Delete(AdmEtiquetaDelete request)
        {
            var id = request.Id;

            try
            {
                EtiquetaOrmService.Delete(id);
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

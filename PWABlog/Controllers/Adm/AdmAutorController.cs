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

        public AdmAutorController( AutorOrmService autorOrmService)
        {
            this.autorOrmService = autorOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdmAutorListarViewModel model = new AdmAutorListarViewModel();

            var listaEtiquetas = autorOrmService.GetAll();
            
            foreach (var autorEntity in listaEtiquetas)
            {
                var autorAdmin = new AutorAdmAutores();
                autorAdmin.Id = autorEntity.Id;
                autorAdmin.Nome = autorEntity.Nome;
                
                model.Autores.Add(autorAdmin);
            }
            
            return View(model);
        }
        
        

        [HttpGet]
        public IActionResult Create()
        {
            AdmAutorCriarViewModel model = new AdmAutorCriarViewModel();
            
            model.Erro = (string) TempData["erro-msg"];

            return View(model);
        }

        [HttpPost]
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
        public IActionResult Edit(int id)
        {
            AdmAutorEditViewModel model = new AdmAutorEditViewModel();
            
            var autorAEditar = autorOrmService.GetById(id);
            if (autorAEditar == null) {
                return RedirectToAction("Listar");
            }
            
            model.Erro = (string) TempData["erro-msg"];

            model.Id = autorAEditar.Id;
            model.Nome = autorAEditar.Nome;
            
            return View(model);
        }

        [HttpPost]
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
        public IActionResult Delete(int id)
        {
            AdmAutorDeleteViewModel model = new AdmAutorDeleteViewModel();
            
            var autorADeletar = autorOrmService.GetById(id);
            if (autorADeletar == null) {
                return RedirectToAction("Listar");
            }
            
            model.Erro = (string) TempData["erro-msg"];
            model.Id = autorADeletar.Id;
            model.Nome = autorADeletar.Nome;

            return View(model);
        }

        [HttpPost]
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

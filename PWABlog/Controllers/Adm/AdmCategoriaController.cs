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
            AdmCategoriaListarViewModel model = new AdmCategoriaListarViewModel();
            
            var listaCategorias = categoriaOrmService.GetAll();

            foreach (var etiquetaEntity in listaCategorias) {
                var etiquetaAdminEtiquetas = new CategoriaAdmCategorias();
                etiquetaAdminEtiquetas.Id = etiquetaEntity.Id;
                etiquetaAdminEtiquetas.Nome = etiquetaEntity.Nome;
                
                model.Categorias.Add(etiquetaAdminEtiquetas);
            }
            
            return View(model);
        }

        

        [HttpGet]
        public IActionResult Create()
        {
            AdmCategoriaCriarViewModel model = new AdmCategoriaCriarViewModel();
            
            model.Erro = (string) TempData["erro-msg"];
            
            return View(model);
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
            AdmCategoriaEditViewModel model = new AdmCategoriaEditViewModel();
            
            var categoriaAEditar = categoriaOrmService.GetById(id);
            if (categoriaAEditar == null) {
                return RedirectToAction("Listar");
            }
            
            model.Erro = (string) TempData["erro-msg"];

            model.Id = categoriaAEditar.Id;
            model.Nome = categoriaAEditar.Nome;

            return View(model);
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
            AdmCategoriaDeleteViewModel model = new AdmCategoriaDeleteViewModel();
            
            var categoriaADeletar = categoriaOrmService.GetById(id);
            if (categoriaADeletar == null) {
                return RedirectToAction("Listar");
            }
            
            model.Erro = (string) TempData["erro-msg"];
            model.Id = categoriaADeletar.Id;
            model.Nome = categoriaADeletar.Nome;
            
            return View(model);
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

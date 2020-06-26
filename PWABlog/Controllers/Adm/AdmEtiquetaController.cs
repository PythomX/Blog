using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.RequestModels.AdmEtiqueta;
using PWABlog.ViewModels.Adm;

namespace PWABlog.Controllers.AdmController
{
    [Authorize]
    public class AdmEtiquetaController : Controller
    {
        private readonly CategoriaOrmService categoriaOrmService;
        private readonly EtiquetaOrmService etiquetaOrmService;

        public AdmEtiquetaController(CategoriaOrmService categoriaOrmService, EtiquetaOrmService etiquetaOrmService)
        {
            this.categoriaOrmService = categoriaOrmService;
            this.etiquetaOrmService = etiquetaOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdmEtiquetaListarViewModel model = new AdmEtiquetaListarViewModel();
            
            var listaEtiquetas = etiquetaOrmService.GetAll();
            
            foreach (var etiquetaEntity in listaEtiquetas) {
                var etiquetaAdminEtiquetas = new EtiquetaAdminEtiquetas();
                etiquetaAdminEtiquetas.Id = etiquetaEntity.Id;
                etiquetaAdminEtiquetas.Nome = etiquetaEntity.Nome;
                etiquetaAdminEtiquetas.NomeCategoria = etiquetaEntity.Categoria.Nome;
                
                model.Etiquetas.Add(etiquetaAdminEtiquetas);
            }
            
            return View(model);
        }

        

        [HttpGet]
        public IActionResult Create()
        {
            AdmEtiquetaCriarViewModel model = new AdmEtiquetaCriarViewModel();
            
            model.Erro = (string) TempData["erro-msg"]; 
            
            var listaCategorias = categoriaOrmService.GetAll();
            
            foreach (var categoriaEntity in listaCategorias) {
                var categoriaAdminEtiquetas = new CategoriaAdminEtiquetas();
                categoriaAdminEtiquetas.IdCategoria = categoriaEntity.Id;
                categoriaAdminEtiquetas.NomeCategoria = categoriaEntity.Nome;
                
                model.Categorias.Add(categoriaAdminEtiquetas);
            }

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Create(AdmEtiquetaCreateRequestModel request)
        {
            var nome = request.Nome;
            var idCategoria = request.IdCategoria;

            try {
                etiquetaOrmService.Create(nome, idCategoria);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Create");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            AdmEtiquetaEditViewModel model = new AdmEtiquetaEditViewModel();
            
            var etiquetaAEditar = etiquetaOrmService.GetById(id);
            if (etiquetaAEditar == null) {
                return RedirectToAction("Listar");
            }
            
            model.Erro = (string) TempData["erro-msg"];
            
            var listaCategorias = categoriaOrmService.GetAll();
            
            foreach (var categoriaEntity in listaCategorias) {
                var categoriaAdminEtiquetas = new CategoriaAdminEtiquetas();
                categoriaAdminEtiquetas.IdCategoria = categoriaEntity.Id;
                categoriaAdminEtiquetas.NomeCategoria = categoriaEntity.Nome;
                
                model.Categorias.Add(categoriaAdminEtiquetas);
            }
            
            model.IdEtiqueta = etiquetaAEditar.Id;
            model.NomeEtiqueta = etiquetaAEditar.Nome;
            model.IdCategoriaEtiqueta = etiquetaAEditar.Categoria.Id;
            model.Titulo += model.NomeEtiqueta;

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Edit(AdmEtiquetaEditRequestModel request)
        {
            var id = request.Id;
            var nome = request.Nome;
            var idCategoria = request.IdCategoria;

            try {
                etiquetaOrmService.Edit(id, nome, idCategoria);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Edit", new {id = id});
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            AdmEtiquetaDeleteViewModel model = new AdmEtiquetaDeleteViewModel();
            
            var etiquetaADeletar = etiquetaOrmService.GetById(id);
            if (etiquetaADeletar == null) {
                return RedirectToAction("Listar");
            }
            
            model.Erro = (string) TempData["erro-msg"];
            
            model.IdEtiqueta = etiquetaADeletar.Id;
            model.NomeEtiqueta = etiquetaADeletar.Nome;
            model.Titulo += model.NomeEtiqueta;

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Delete(AdmEtiquetaDeleteRequestModel request)
        {
            var id = request.Id;

            try {
                etiquetaOrmService.Delete(id);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Delete", new {id = id});
            }

            return RedirectToAction("Listar");
        }
    }
}

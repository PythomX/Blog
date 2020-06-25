using Microsoft.AspNetCore.Mvc;
using System;
using Blog.Models.Blog.Etiqueta;
using PWABlog;
using Blog.RequestModels.AdmEtiqueta;
using PWABlog.Models.Blog.Categoria;

namespace Blog.ViewMoldels
{
    public class AdmEtiquetaController : Controller
    {
        private readonly CategoriaOrmService categoriaOrmService;
        private readonly EtiquetaOrmService etiquetaOrmService;

        public AdmEtiquetaController(Database context, EtiquetaOrmService EtiquetaOrmService)
        {
            this.categoriaOrmService = categoriaOrmService;
            this.etiquetaOrmService = EtiquetaOrmService;
        }

        // GET: AdminEtiqueta
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Listar()
        {
            AdmEtiquetasListarViewModel model = new AdmEtiquetasListarViewModel();
            
            // Obter as Etiquetas
            var listaEtiquetas = etiquetaOrmService.GetAll();
            
            // Alimentar o model com as etiquetas que serão listadas
            foreach (var etiquetaEntity in listaEtiquetas) {
                var etiquetaAdminEtiquetas = new EtiquetaAdmEtiquetas();
                etiquetaAdminEtiquetas.Id = etiquetaEntity.Id;
                etiquetaAdminEtiquetas.Nome = etiquetaEntity.Nome;
                etiquetaAdminEtiquetas.NomeCategoria = etiquetaEntity.Categoria.Nome;
                
                model.Etiquetas.Add(etiquetaAdminEtiquetas);
            }
            
            return View(model);
        }

        // GET: AdminEtiqueta/Create
        [HttpGet]
        public IActionResult Create()
        {
            AdmEtiquetasCriarViewModel model = new AdmEtiquetasCriarViewModel();
            
            // Definir possível erro de processamento (vindo do post do criar)
            model.Erro = (string) TempData["erro-msg"];
            
            // Obter as Categorias
            var listaCategorias = categoriaOrmService.ObterCategorias();
            
            // Alimentar o model com as categorias que serão colocadas no <select> do formulário
            foreach (var categoriaEntity in listaCategorias) {
                var categoriaAdminEtiquetas = new CategoriaAdminEtiquetas();
                categoriaAdminEtiquetas.IdCategoria = categoriaEntity.Id;
                categoriaAdminEtiquetas.NomeCategoria = categoriaEntity.Nome;
                
                model.Categorias.Add(categoriaAdminEtiquetas);
            }

            return View(model);
        }

        // POST: AdminEtiqueta/Create
        [HttpPost]
        public IActionResult Create(AdmEtiquetaCreate request)
        {
            AdmEtiquetasCriarViewModel model = new AdmEtiquetasCriarViewModel();
            
            // Definir possível erro de processamento (vindo do post do criar)
            model.Erro = (string) TempData["erro-msg"];
            
            // Obter as Categorias
            var listaCategorias = _categoriaOrmService.ObterCategorias();
            
            // Alimentar o model com as categorias que serão colocadas no <select> do formulário
            foreach (var categoriaEntity in listaCategorias) {
                var categoriaAdminEtiquetas = new CategoriaAdminEtiquetas();
                categoriaAdminEtiquetas.IdCategoria = categoriaEntity.Id;
                categoriaAdminEtiquetas.NomeCategoria = categoriaEntity.Nome;
                
                model.Categorias.Add(categoriaAdminEtiquetas);
            }

            return View(model);
            
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
                etiquetaOrmService.Edit(id, nome);
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
                etiquetaOrmService.Delete(id);
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

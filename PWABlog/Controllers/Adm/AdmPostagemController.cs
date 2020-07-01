using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using PWABlog.RequestModels.AdmPostagem;
using PWABlog.ViewModels.Adm;

namespace PWABlog.Controllers.Adm
{
    [Authorize]
    public class AdmPostagemController : Controller
    {
        private readonly AutorOrmService autorOrmService;
        private readonly CategoriaOrmService categoriaOrmService;
        private readonly EtiquetaOrmService etiquetaOrmService;
        private readonly PostagemOrmService postagemOrmService;

        public AdmPostagemController(AutorOrmService autorOrmService, CategoriaOrmService categoriaOrmService, EtiquetaOrmService etiquetaOrmService, PostagemOrmService postagemOrmService)
        {
            this.autorOrmService = autorOrmService;
            this.categoriaOrmService = categoriaOrmService;
            this.etiquetaOrmService = etiquetaOrmService;
            this.postagemOrmService = postagemOrmService;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            AdmPostagemListarViewModel model = new AdmPostagemListarViewModel();

            var postagens = postagemOrmService.GetAll();
            
            
            foreach (var post in postagens)
            {
                PostagemAdmPostagens postagemAdmPostagens = new PostagemAdmPostagens();

                postagemAdmPostagens.Id = post.Id;
                postagemAdmPostagens.Titulo = post.Titulo;
                postagemAdmPostagens.Autor = post.Autor.Nome;
                postagemAdmPostagens.Categoria = post.Categoria.Nome;
                postagemAdmPostagens.Versao = post.Revisoes.Count;
                postagemAdmPostagens.DataDeExibicao = post.DataExibicao.ToString("dd/MM/yyyy");
                postagemAdmPostagens.Versao = post.Revisoes.OrderByDescending(r => r.Versao).Last().Versao;
                
                
                model.Postagens.Add(postagemAdmPostagens);
            }
            
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            AdmPostagemCriarViewModel model = new AdmPostagemCriarViewModel();

            var listaAutores = autorOrmService.GetAll();
            
            foreach (var autorEntity in listaAutores)
            {
                var autorAdm = new AutorAdmPostagens();
                autorAdm.Id = autorEntity.Id;
                autorAdm.Nome = autorEntity.Nome;
                
                model.Autores.Add(autorAdm);
            }
            
            var listaCategorias = categoriaOrmService.GetAll();

            foreach (var categoriaEntity in listaCategorias) {
                var categoriaAdm = new CategoriaAdmPostagens();
                categoriaAdm.Id = categoriaEntity.Id;
                categoriaAdm.Nome = categoriaEntity.Nome;
                
                model.Categorias.Add(categoriaAdm);
            }
            
            var listaEtiquetas = etiquetaOrmService.GetAll();
            
            foreach (var etiquetaEntity in listaEtiquetas) {
                var etiquetaAdm = new EtiquetaAdmPostagens();
                etiquetaAdm.Id = etiquetaEntity.Id;
                etiquetaAdm.Nome = etiquetaEntity.Nome;
                
                model.Etiquetas.Add(etiquetaAdm);
            }
            
            model.Erro = (string) TempData["erro-msg"];

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Create(AdmPostagemCreateRequestModel request)
        {
            var titulo = request.Titulo;
            var categoria = request.Categoria;
            var autor = request.Autor;
            var descricao = request.Descricao;
            var texto = request.Texto;
            var etiquetas = request.Etiquetas;
            var dataExibicao = DateTime.Parse(request.DataExibicao);
            
            try {                        
                postagemOrmService.Create(titulo, categoria, autor, descricao, texto, etiquetas, dataExibicao);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Create");
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            AdmPostagemEditViewModel model = new AdmPostagemEditViewModel();
            
            model.Erro = (string) TempData["erro-msg"];
            
            var listaAutores = autorOrmService.GetAll();
            
            foreach (var autorEntity in listaAutores)
            {
                var autorAdm = new AutorAdmPostagens();
                autorAdm.Id = autorEntity.Id;
                autorAdm.Nome = autorEntity.Nome;
                
                model.Autores.Add(autorAdm);
            }
            
            var listaCategorias = categoriaOrmService.GetAll();

            foreach (var categoriaEntity in listaCategorias) {
                var categoriaAdm = new CategoriaAdmPostagens();
                categoriaAdm.Id = categoriaEntity.Id;
                categoriaAdm.Nome = categoriaEntity.Nome;
                
                model.Categorias.Add(categoriaAdm);
            }
            
            var listaEtiquetas = etiquetaOrmService.GetAll();
            
            foreach (var etiquetaEntity in listaEtiquetas) {
                var etiquetaAdm = new EtiquetaAdmPostagens();
                etiquetaAdm.Id = etiquetaEntity.Id;
                etiquetaAdm.Nome = etiquetaEntity.Nome;
                
                model.Etiquetas.Add(etiquetaAdm);
            }
            
            var postagem = postagemOrmService.GetById(id);
            model.Id = postagem.Id;
            model.AutorId = postagem.Autor.Id;
            model.CategoriaId = postagem.Categoria.Id;
            model.Titulo = postagem.Titulo;
            model.Texto = postagem.Revisoes.OrderByDescending(r => r.Versao).Last().Texto;
            
            
            
            
            foreach(var etiqueta in postagem.PostagensEtiquetas)
            {
                model.EtiquetasPostagem.Add(etiqueta.IdEtiqueta);
            }

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Edit(AdmPostagemEditRequestModel request)
        {
            var id = request.Id;
            var titulo = request.Titulo;
            var categoriaId = request.idCategoria;
            var autorId = request.idAutor;
            var texto = request.Texto;
            var descricao = request.Descricao;
            var etiquetas = request.Etiquetas;
            var dataExibicao = DateTime.Parse(request.DataExibicao);

            
            //string titulo, int categoriaId, int autorId, string descricao, string texto, List<int> etiquetas, DateTime dataExibicao
            try {
                postagemOrmService.Edit(id, titulo, categoriaId, autorId, descricao, texto, etiquetas, dataExibicao);
            } catch (Exception exception) {
                TempData["erro-msg"] = exception.Message;
                return RedirectToAction("Edit", new {id = id});
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            AdmPostagemDeleteViewModel model = new AdmPostagemDeleteViewModel();
            
            var postagem = postagemOrmService.GetById(id);

            model.Id = postagem.Id;
            model.Titulo = postagem.Titulo;
            
            model.Erro = (string) TempData["erro-msg"];

            return View(model);
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

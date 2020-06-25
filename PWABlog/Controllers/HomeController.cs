using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PWABlog.Models;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using PWABlog.Models.Blog.Postagem.Revisao;
using PWABlog.ViewModels.Home;

namespace PWABlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly CategoriaOrmService categoriaOrmService;
        private readonly PostagemOrmService postagemOrmService;

        public HomeController(
            ILogger<HomeController> logger,
            CategoriaOrmService categoriaOrmService,
            PostagemOrmService postagemOrmService
        ){
            this.logger = logger;
            this.categoriaOrmService = categoriaOrmService;
            this.postagemOrmService = postagemOrmService;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            
            List<PostagemEntity> listaPostagens = postagemOrmService.GetAll();
            
            foreach (PostagemEntity postagem in listaPostagens) {
                PostagemHomeIndex postagemHomeIndex = new PostagemHomeIndex();
                postagemHomeIndex.Titulo = postagem.Titulo;
                postagemHomeIndex.Texto = postagem.Descricao;
                postagemHomeIndex.Categoria = postagem.Categoria.Nome;
                postagemHomeIndex.NumeroComentarios = postagem.Comentarios.Count.ToString();
                postagemHomeIndex.Id = postagem.Id.ToString();

                // Obter última revisão
                RevisaoEntity ultimaRevisao = postagem.Revisoes.OrderByDescending(o => o.DataCriacao).FirstOrDefault();
                
                if (ultimaRevisao != null) {
                    postagemHomeIndex.DataCriacao = ultimaRevisao.DataCriacao.ToLongDateString();
                }

                model.Postagens.Add(postagemHomeIndex);
            }

            // Alimentar a lista de postagens populares que serão exibidas na view
            List<PostagemEntity> postagensPopulares = postagemOrmService.GetPostsPopular();
            
            foreach (PostagemEntity postagemPopular in postagensPopulares) {
                model.PostagensPopulares.Add(new PostagemHomeIndex.PostagemPopularHomeIndex()
                {
                    Categoria = postagemPopular.Categoria.Nome,
                    Id = postagemPopular.Id.ToString(),
                    Titulo = postagemPopular.Titulo
                });
            }


            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
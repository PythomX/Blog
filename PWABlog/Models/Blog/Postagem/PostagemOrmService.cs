using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;
using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemOrmService
    {
        private readonly Database databaseContext;

        private readonly RevisaoOrmService revisaoOrmService;
        
        public PostagemOrmService(Database databaseContext, RevisaoOrmService revisaoOrmService)
        {
            this.databaseContext = databaseContext;
            this.revisaoOrmService = revisaoOrmService;
        }

        public List<PostagemEntity> GetAll()
        {
            return databaseContext.Postagens
                .Include(p => p.Categoria)
                .Include(p => p.Revisoes)
                .Include(p => p.Comentarios)
                .ToList();
        }

        public List<PostagemEntity> GetPostsPopular()
        {
            return databaseContext.Postagens
                .Include(a => a.Autor)
                .OrderByDescending(c => c.Comentarios.Count)
                .Take(4)
                .ToList();
        }

        public int GetLastVersion(int postagemId)
        {
            var revisao = this.databaseContext.Postagens
              .Include(r => r.Revisoes)
              .Where(p => p.Id == postagemId)
              .Select(p => p.Revisoes.OrderByDescending(r => r.Versao).Last())
              .FirstOrDefault();

            if (revisao == null)
                return 0;

            return revisao.Versao;
        }

        internal PostagemEntity Create(string titulo, int idCategoria, int idAutor, string descricao, DateTime dataExibicao)
        {
            
            var autor = databaseContext.Autores.Find(idAutor);
            if (autor == null) {
                throw new Exception("O Autor informado para a Postagem não foi encontrado!");
            }

            var categoria = databaseContext.Categorias.Find(idCategoria);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Postagem não foi encontrada!");
            }

            var novaPostagem = new PostagemEntity
            {
                Titulo = titulo,
                Descricao = descricao,
                Autor = autor,
                Categoria = categoria,
                DataExibicao = dataExibicao
            };
            databaseContext.Postagens.Add(novaPostagem);
            databaseContext.SaveChanges();
            
            revisaoOrmService.CriarRevisao(novaPostagem.Id, descricao);
            
            return novaPostagem;
        }

        public PostagemEntity Edit(int id, string titulo, string descricao, int idCategoria, string texto, DateTime dataExibicao)
        {

            var postagem = databaseContext.Postagens.Find(id);
            if (postagem == null) {
                throw new Exception("Postagem não encontrada!");
            }

            var categoria = databaseContext.Categorias.Find(idCategoria);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Postagem não foi encontrada!");
            }

            postagem.Titulo = titulo;
            postagem.Descricao = descricao;
            postagem.Categoria = categoria;
            postagem.DataExibicao = dataExibicao;
            databaseContext.SaveChanges();
            
            revisaoOrmService.CriarRevisao(postagem.Id, texto);

            return postagem;
        }

        internal void Delete(int id)
        {

            var postagem = databaseContext.Postagens.Find(id);
            if (postagem == null) {
                throw new Exception("Postagem não encontrada!");
            }

            databaseContext.Postagens.Remove(postagem);
            databaseContext.SaveChanges();
        }


    }
}
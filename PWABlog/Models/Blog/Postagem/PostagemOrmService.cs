using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;
using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemOrmService
    {
        private readonly Database databaseContext;

        public PostagemOrmService(Database databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<PostagemEntity> getAll()
        {
            return databaseContext.Postagens
                .Include(p => p.Categoria)
                .Include(p => p.Revisoes)
                .Include(p => p.Comentarios)
                .ToList();
        }

        public List<PostagemEntity> getPostsPopular()
        {
            return databaseContext.Postagens
                .Include(a => a.Autor)
                .OrderByDescending(c => c.Comentarios.Count)
                .Take(4)
                .ToList();
        }

        public int getLastVersion(int postagemId)
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

        internal PostagemEntity Create(string titulo, int categoriaId, int autorId, string texto)
        {
            var categoria = this.databaseContext.Categorias.Find(categoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            var autor = this.databaseContext.Autores.Find(autorId);
            if (autor == null)
                throw new Exception("Autor não encontrado.");


            var postagem = new PostagemEntity
            {
                Autor = autor,
                Categoria = categoria
            };

            this.databaseContext.Postagens.Add(postagem);
            this.databaseContext.SaveChanges();

            var revisao = new RevisaoEntity
            {
                Texto = texto,
                DataCriacao = DateTime.Now,
                Versao = 1,
            };

            postagem.Revisoes.Add(revisao);
            this.databaseContext.SaveChanges();

            return postagem;
        }

        internal PostagemEntity Edit(int id, string titulo, int categoriaId, int autorId, string texto)
        {
            var postagem = this.databaseContext.Postagens.Find(id);
            if (postagem == null)
                throw new Exception("Postagem não encontrada.");

            var categoria = this.databaseContext.Categorias.Find(categoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            var autor = this.databaseContext.Autores.Find(autorId);
            if (autor == null)
                throw new Exception("Autor não encontrado.");

            postagem.Titulo = titulo;
            postagem.Categoria = categoria;
            postagem.Autor = autor;

            var revisao = new RevisaoEntity
            {
                Texto = texto,
                DataCriacao = DateTime.Now,
                Versao = this.getLastVersion(id) + 1,
            };

            postagem.Revisoes.Add(revisao);
            this.databaseContext.SaveChanges();

            return postagem;
        }

        internal void Delete(int id)
        {
            var postagem = this.databaseContext.Postagens.Find(id);
            if (postagem == null)
                throw new Exception("Postagem não encontrada.");

            this.databaseContext.Postagens.Remove(postagem);
        }


    }
}
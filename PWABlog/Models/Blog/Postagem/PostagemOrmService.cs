using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;
using System;
using System.Collections.Generic;
using System.Linq;
using PWABlog.Models.Blog.Etiqueta;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemOrmService
    {
        private readonly Database databaseContext;

        private readonly RevisaoOrmService revisaoOrmService;
        private readonly EtiquetaOrmService etiquetaOrmService;
        
        public PostagemOrmService(Database databaseContext, EtiquetaOrmService etiquetaOrmService, RevisaoOrmService revisaoOrmService)
        {
            this.databaseContext = databaseContext;
            this.revisaoOrmService = revisaoOrmService;
            this.etiquetaOrmService = etiquetaOrmService;
        }

        public PostagemEntity GetById(int id)
        {
            return this.databaseContext.Postagens
                .Include(c => c.Categoria)
                .Include(r => r.Revisoes)
                .Include(a => a.Autor)
                .Include(e => e.PostagensEtiquetas)
                .ThenInclude(post => post.Etiqueta)
                .Where(p => p.Id == id)
                .First();
        }
        
        public List<PostagemEntity> GetAll()
        {
            return databaseContext.Postagens
                .Include(c => c.Categoria)
                .Include(r => r.Revisoes)
                .Include(a => a.Autor)
                .Select(p => new
                {
                    p,
                    Revisoes = p.Revisoes.OrderByDescending(r => r.Versao).Last()
                })
                .AsEnumerable()
                .Select(e => e.p)
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

        internal PostagemEntity Create(string titulo, int categoriaId, int autorId, string descricao, string texto, List<int> etiquetas, DateTime dataExibicao)
        {
            
            var autor = databaseContext.Autores.Find(autorId);
            if (autor == null) {
                throw new Exception("O Autor informado para a Postagem não foi encontrado!");
            }

            var categoria = databaseContext.Categorias.Find(categoriaId);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Postagem não foi encontrada!");
            }

            var novaPostagem = new PostagemEntity
            {
                Autor = autor,
                Categoria = categoria,
                Titulo = titulo,
                Descricao = descricao,
                DataExibicao = dataExibicao
            };
            
            
            
            databaseContext.Postagens.Add(novaPostagem);
            databaseContext.SaveChanges();

            UpdateTag(novaPostagem.Id, etiquetas);
            
            revisaoOrmService.AddRevision(novaPostagem.Id, texto, 1);
            
            return novaPostagem;
        }

        private void UpdateTag(in int novaPostagemId, List<int> etiquetas)
        {
            
            
            var postagem = this.GetById(novaPostagemId);
            
            if (postagem == null)
                throw new Exception("Postagem não encontrada.");

            Dictionary<EtiquetaEntity, PostagemEtiquetaEntity> etiquetaEntities = new Dictionary<EtiquetaEntity, PostagemEtiquetaEntity>();

            if(postagem.PostagensEtiquetas != null)
            {
                foreach (var postagemEtiquetaEntity in postagem.PostagensEtiquetas)
                {
                    etiquetaEntities[postagemEtiquetaEntity.Etiqueta] = postagemEtiquetaEntity;
                }
            }
            
            
            foreach(var etiqueta in this.databaseContext.Etiquetas)
            {
                if(etiquetaEntities.ContainsKey(etiqueta))
                {
                    if(!etiquetas.Contains(etiqueta.Id))
                    {
                        etiquetaOrmService.AttachTag(etiqueta.Id, postagem.Id);
                    }
                } 
                else
                {
                    if(etiquetas.Contains(etiqueta.Id))
                    {
                        etiquetaOrmService.DetachTag(etiqueta.Id, postagem.Id);
                    }
                }
            }
            
            
            
        }

        public PostagemEntity Edit(int id, string titulo, int categoriaId, int autorId, string descricao, string texto, List<int> etiquetas, DateTime dataExibicao)
        {

            var postagem = databaseContext.Postagens.Find(id);
            if (postagem == null) {
                throw new Exception("Postagem não encontrada!");
            }

            var categoria = databaseContext.Categorias.Find(categoriaId);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Postagem não foi encontrada!");
            }
            
            var autor = databaseContext.Autores.Find(autorId);
            if (autor == null) {
                throw new Exception("O Autor informado para a Postagem não foi encontrado!");
            }

            postagem.Titulo = titulo;
            postagem.Descricao = descricao;
            postagem.Autor = autor;
            postagem.Categoria = categoria;
            postagem.DataExibicao = dataExibicao;
            
            UpdateTag(postagem.Id, etiquetas);
            
            databaseContext.SaveChanges();
            
            //Pegando a ultima revisao e sempre adicionando mais 1
            revisaoOrmService.AddRevision(postagem.Id, texto, GetLastVersion(id) + 1);

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
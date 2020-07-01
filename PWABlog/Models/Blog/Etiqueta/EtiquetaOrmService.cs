using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem;

namespace PWABlog.Models.Blog.Etiqueta
{
    public class EtiquetaOrmService
    {
        private readonly Database databaseContext;

        public EtiquetaOrmService(Database database)
        {
            this.databaseContext = database;
        }

        public List<EtiquetaEntity> GetAll()
        {
            return databaseContext.Etiquetas
                .Include(e => e.Categoria)
                .ToList();
        }

        public EtiquetaEntity GetById(int idEtiqueta)
        {
            var etiqueta = databaseContext.Etiquetas.Find(idEtiqueta);

            return etiqueta;
        }
        
        public List<EtiquetaEntity> GetByName(string nomeEtiqueta)
        {
            return databaseContext.Etiquetas.Where(c => c.Nome.Contains(nomeEtiqueta)).ToList();
        }
        
        public EtiquetaEntity Create(string nome, int idCategoria)
        {
            if (nome == null) {
                throw new Exception("A Etiqueta precisa de um nome!");
            }
            
            var categoria = databaseContext.Categorias.Find(idCategoria);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Etiqueta não foi encontrada!");
            }

            var novaEtiqueta = new EtiquetaEntity
            {
                Nome = nome, 
                Categoria = categoria
            };
            databaseContext.Etiquetas.Add(novaEtiqueta);
            databaseContext.SaveChanges();

            return novaEtiqueta;
        }

        public EtiquetaEntity Edit(int id, string nome, int idCategoria)
        {
            var etiqueta = databaseContext.Etiquetas.Find(id);
            if (etiqueta == null) {
                throw new Exception("Etiqueta não encontrada!");
            }
            
            var categoria = databaseContext.Categorias.Find(idCategoria);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Etiqueta não foi encontrada!");
            }

            etiqueta.Nome = nome;
            etiqueta.Categoria = categoria;
            databaseContext.SaveChanges();

            return etiqueta;
        }

        public EtiquetaEntity Delete(int id)
        {
            var etiqueta = this.databaseContext.Etiquetas.Find(id);
            if (etiqueta == null)
                throw new Exception("Etiqueta não encontrada!");

            this.databaseContext.Etiquetas.Remove(etiqueta);
            this.databaseContext.SaveChanges();

            return etiqueta;
        }

        public void AttachTag(int etiquetaId, int postagemId)
        {
            
            var etiqueta = this.databaseContext.Etiquetas.Find(etiquetaId);
            if (etiqueta == null)
                throw new Exception("Não foi possível encontrar a etiqueta!");

            var postagem = this.databaseContext.Postagens.Find(postagemId);
            if (postagem == null)
                throw new Exception("Não foi possível encontrar a postagem!");

            this.databaseContext.PostagensEtiquetas.Add(new PostagemEtiquetaEntity
            {
                Postagem = postagem,
                Etiqueta = etiqueta
            });

            databaseContext.SaveChangesAsync();
            
        }

        public void DetachTag(int etiquetaId, int postagemId)
        {
            
            var etiqueta = this.databaseContext.Etiquetas.Find(etiquetaId);
            if (etiqueta == null)
                throw new Exception("Não foi possível encontrar esta etiqueta!");

            var postagem = this.databaseContext.Postagens.Find(postagemId);
            if (postagem == null)
                throw new Exception("Não foi possível encontrar esta postagem!");

            var entity = this.databaseContext.PostagensEtiquetas.Where(ep => ep.IdEtiqueta == etiquetaId && ep.IdPostagem == postagemId).First();
            if (entity == null)
                throw new Exception("Falha ao encontrar etiqueta anexada!");

            this.databaseContext.PostagensEtiquetas.Remove(entity);
            this.databaseContext.SaveChanges();
            
        }
    }
}

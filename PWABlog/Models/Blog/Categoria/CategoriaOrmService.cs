using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PWABlog.Models.Blog.Categoria
{
    public class CategoriaOrmService
    {
        private readonly Database databaseContext;

        public CategoriaOrmService(Database databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<CategoriaEntity> GetAll()
        {
            return databaseContext.Categorias
                .Include(c => c.Etiquetas)
                .ToList();
        }

        public CategoriaEntity GetById(int idCategoria)
        {
            var categoria = databaseContext.Categorias.Find(idCategoria);

            return categoria;
        }

        public List<CategoriaEntity> GetByName(string nomeCategoria)
        {
            return databaseContext.Categorias.Where(c => c.Nome.Contains(nomeCategoria)).ToList();
            
        }


        public CategoriaEntity Create(string nome)
        {
            var novaCategoria = new CategoriaEntity { Nome = nome };
            databaseContext.Categorias.Add(novaCategoria);
            databaseContext.SaveChanges();

            return novaCategoria;
        }

        public CategoriaEntity Edit(int id, string nome)
        {
            var categoria = databaseContext.Categorias.Find(id);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada!");
            }

            categoria.Nome = nome;
            databaseContext.SaveChanges();

            return categoria;
        }

        public bool Delete(int id)
        {
            var categoria = databaseContext.Categorias.Find(id);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada!");
            }

            databaseContext.Categorias.Remove(categoria);
            databaseContext.SaveChanges();

            return true;
        }

    }
}
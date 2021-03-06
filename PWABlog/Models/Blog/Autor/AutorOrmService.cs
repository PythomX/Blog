﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PWABlog.Models.Blog.Autor
{
    public class AutorOrmService
    {
        private readonly Database databaseContext;

        public AutorOrmService(Database databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<AutorEntity> GetAll()
        {
            return databaseContext.Autores.ToList();
        }

        public AutorEntity GetById(int idCategoria)
        {
            var autor = databaseContext.Autores.Find(idCategoria);

            return autor;
        }

        public List<AutorEntity> GetByName(string nomeAutor)
        {
            return databaseContext.Autores.Where(c => c.Nome.Contains(nomeAutor)).ToList();
        }


        public AutorEntity Create(string nome)
        {
            var autor = new AutorEntity { Nome = nome };
            this.databaseContext.Autores.Add(autor);
            this.databaseContext.SaveChanges();

            return autor;
        }


        public AutorEntity Edit(int id, string nome)
        {
            var autor = this.databaseContext.Autores.Find(id);
            if (autor == null)
                throw new Exception("Autor não encontrada!");

            autor.Nome = nome;
            this.databaseContext.SaveChanges();

            return autor;
        }

        public AutorEntity Delete(int id)
        {
            var autor = this.databaseContext.Autores.Find(id);
            if (autor == null)
                throw new Exception("Autor não encontrada!");

            this.databaseContext.Autores.Remove(autor);
            this.databaseContext.SaveChanges();

            return autor;
        }


    }
}

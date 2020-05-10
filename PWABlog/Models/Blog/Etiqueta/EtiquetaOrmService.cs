using PWABlog;
using PWABlog.Models.Blog.Etiqueta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Blog.Etiqueta
{
    public class EtiquetaOrmService
    {
        private readonly Database databasecontext;

        public EtiquetaOrmService(Database database)
        {
            this.databasecontext = database;
        }

        public List<EtiquetaEntity> GetAll()
        {
            return this.databasecontext.Etiquetas.ToList();
        }

        public EtiquetaEntity Create(string nome)
        {
            var etiqueta = new EtiquetaEntity { Nome = nome};
            this.databasecontext.Etiquetas.Add(etiqueta);
            this.databasecontext.SaveChanges();

            return etiqueta;
        }

        public EtiquetaEntity Edit(int id, string nome)
        {
            var etiqueta = this.databasecontext.Etiquetas.Find(id);
            if (etiqueta == null)
                throw new Exception("Etiqueta não encontrada!");

            etiqueta.Nome = nome;
            this.databasecontext.SaveChanges();

            return etiqueta;
        }

        public EtiquetaEntity Delete(int id)
        {
            var etiqueta = this.databasecontext.Etiquetas.Find(id);
            if (etiqueta == null)
                throw new Exception("Etiqueta não encontrada!");

            this.databasecontext.Etiquetas.Remove(etiqueta);
            this.databasecontext.SaveChanges();

            return etiqueta;
        }
    }
}

using System;

namespace PWABlog.Models.Blog.Postagem.Revisao
{
    public class RevisaoOrmService
    {
        
        private readonly Database databaseContext;

        public RevisaoOrmService(Database databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void AddRevision(int postagemId, string texto, int versao)
        {
            var postagem = this.databaseContext.Postagens.Find(postagemId);
            if (postagem == null)
                throw new Exception("Postagem não encontrada.");

            var revisao = new RevisaoEntity
            {
                Postagem = postagem,
                Texto = texto,
                DataCriacao = DateTime.Now,
                Versao = versao,
            };

            this.databaseContext.Revisoes.Add(revisao);
            this.databaseContext.SaveChanges();
        }
        
    }
}
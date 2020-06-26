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

        public RevisaoEntity GetById(int id)
        {
            var revisao = databaseContext.Revisoes.Find(id);

            return revisao;
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
        
        public RevisaoEntity Create(int idPostagem, string texto)
        {

            var postagem = databaseContext.Postagens.Find(idPostagem);
            if (postagem == null) {
                throw new Exception("A Postagem informada para a Revisão não foi encontrada!");
            }

            var novaRevisao = new RevisaoEntity()
            {
                Postagem = postagem,
                Texto = texto,
                Versao = postagem.ObterUltimaRevisao().Versao + 1,
                DataCriacao = new DateTime()
            };
            databaseContext.Revisoes.Add(novaRevisao);
            databaseContext.SaveChanges();

            return novaRevisao;
        }

        public RevisaoEntity Edit(int id, string texto)
        {

            var revisao = databaseContext.Revisoes.Find(id);
            if (revisao == null) {
                throw new Exception("Revisão não encontrada!");
            }

            revisao.Texto = texto;
            databaseContext.SaveChanges();

            return revisao;
        }

        public bool Delete(int id)
        {

            var revisao = databaseContext.Revisoes.Find(id);
            if (revisao == null) {
                throw new Exception("Revisão não encontrada!");
            }

            databaseContext.Revisoes.Remove(revisao);
            databaseContext.SaveChanges();

            return true;
        }
        
    }
}
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

        public RevisaoEntity ObterRevisaoPorId(int id)
        {
            var revisao = databaseContext.Revisoes.Find(id);

            return revisao;
        }

        public RevisaoEntity CriarRevisao(int idPostagem, string texto)
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

        public RevisaoEntity EditarRevisao(int id, string texto)
        {

            var revisao = databaseContext.Revisoes.Find(id);
            if (revisao == null) {
                throw new Exception("Revisão não encontrada!");
            }

            revisao.Texto = texto;
            databaseContext.SaveChanges();

            return revisao;
        }

        public bool RemoverRevisao(int id)
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
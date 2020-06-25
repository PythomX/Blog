using System;

namespace PWABlog.Models.Blog.Postagem.Classificacao
{
    public class ClassificacaoOrmService
    {
        
        private readonly Database databaseContext;

        public ClassificacaoOrmService(Database databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public ClassificacaoEntity GetById(int id)
        {
            var classificacao = databaseContext.Classificacoes.Find(id);

            return classificacao;
        }

        public ClassificacaoEntity Create(int idPostagem, bool classificacaoDada)
        {

            var postagem = databaseContext.Postagens.Find(idPostagem);
            if (postagem == null) {
                throw new Exception("A Postagem informada para a Classificação não foi encontrada!");
            }

            var novaClassificacao = new ClassificacaoEntity()
            {
                Postagem = postagem,
                Classificacao = classificacaoDada
            };
            databaseContext.Classificacoes.Add(novaClassificacao);
            databaseContext.SaveChanges();

            return novaClassificacao;
        }

        public ClassificacaoEntity Edit(int id, bool classificacaoDada)
        {

            var classificacao = databaseContext.Classificacoes.Find(id);
            if (classificacao == null) {
                throw new Exception("Classificação não encontrada!");
            }

            classificacao.Classificacao = classificacaoDada;
            databaseContext.SaveChanges();

            return classificacao;
        }

        public bool Delete(int id)
        {

            var classificacao = databaseContext.Classificacoes.Find(id);
            if (classificacao == null) {
                throw new Exception("Classificação não encontrada!");
            }

            databaseContext.Classificacoes.Remove(classificacao);
            databaseContext.SaveChanges();

            return true;
        }
        
    }
}
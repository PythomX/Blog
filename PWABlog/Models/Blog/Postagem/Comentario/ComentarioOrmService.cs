using System;

namespace PWABlog.Models.Blog.Postagem.Comentario
{
    public class ComentarioOrmService
    {
        
        private readonly Database databaseContext;

        public ComentarioOrmService(Database databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public ComentarioEntity ObterComentarioPorId(int id)
        {
            var comentario = databaseContext.Comentarios.Find(id);

            return comentario;
        }

        public ComentarioEntity Create(int idPostagem, string texto, string autor, int idComentarioPai)
        {

            var postagem = databaseContext.Postagens.Find(idPostagem);
            if (postagem == null) {
                throw new Exception("A Postagem informada para o Comentário não foi encontrada!");
            }


            var novoComentario = new ComentarioEntity()
            {
                Postagem = postagem,
                Texto = texto,
                Autor = autor,
                DataCriacao = new DateTime()
            };


            if (idComentarioPai != 0) {
                var comentarioPai = databaseContext.Comentarios.Find(idPostagem);
                if (comentarioPai == null) {
                    throw new Exception("O Comentário Pai informado para o Comentário não foi encontrado!");
                } else {
                    novoComentario.ComentarioPai = comentarioPai;
                }
            }


            databaseContext.Comentarios.Add(novoComentario);
            databaseContext.SaveChanges();

            return novoComentario;
        }

        public ComentarioEntity Edit(int id, string texto)
        {

            var comentario = databaseContext.Comentarios.Find(id);
            if (comentario == null) {
                throw new Exception("Comentário não encontrado!");
            }

            comentario.Texto = texto;
            databaseContext.SaveChanges();

            return comentario;
        }

        public bool Delete(int id)
        {

            var comentario = databaseContext.Comentarios.Find(id);
            if (comentario == null) {
                throw new Exception("Comentário não encontrado!");
            }

            databaseContext.Comentarios.Remove(comentario);
            databaseContext.SaveChanges();

            return true;
        }
        
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemOrmService
    {
        private readonly Database _databaseContext;

        public PostagemOrmService(Database databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<PostagemEntity> ObterPostagens()
        {
            return _databaseContext.Postagens
                .Include(p => p.Categoria)
                .Include(p => p.Revisoes)
                .Include(p => p.Comentarios)
                .ToList();
        }
    }
}
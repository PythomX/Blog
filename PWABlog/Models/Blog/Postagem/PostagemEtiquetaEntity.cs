using System.ComponentModel.DataAnnotations;
using PWABlog.Models.Blog.Etiqueta;

namespace PWABlog.Models.Blog.Postagem
{
    public class PostagemEtiquetaEntity
    {
        public int IdPostagem { get; set; }
        
        public int IdEtiqueta { get; set; }
        
        public PostagemEntity Postagem { get; set; }
        
        public EtiquetaEntity Etiqueta { get; set; }
    }
}
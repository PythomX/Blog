using System.Collections.Generic;
using PWABlog.Models.Blog.Etiqueta;

namespace PWABlog.RequestModels.AdmPostagem
{
    public class AdmPostagemCreateRequestModel
    {
        public string Titulo { get; set; }
        public int Autor { get; set; }
        public int Categoria { get; set; }
        public string Texto { get; set; }
        public string Descricao { get; set; }
        public string DataExibicao { get; set; }
        
        public List<int> Etiquetas { get; set; }
    }
}

using System.Collections.Generic;

namespace PWABlog.RequestModels.AdmPostagem
{
    public class AdmPostagemEditRequestModel
    {
        
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int idAutor { get; set; }
        public int idCategoria { get; set; }
        public string Texto { get; set; }
        public string Descricao { get; set; }
        public string DataExibicao { get; set; }
        
        public List<int> Etiquetas { get; set; }
        
    }
}
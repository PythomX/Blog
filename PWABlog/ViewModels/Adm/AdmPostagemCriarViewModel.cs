using System.Collections.Generic;

namespace PWABlog.ViewModels.Adm
{
    public class AdmPostagemCriarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AutorAdmPostagens> Autores { get; set; }
        public ICollection<CategoriaAdmPostagens> Categorias { get; set; }
        public ICollection<EtiquetaAdmPostagens> Etiquetas { get; set; }
        
        public string Erro { get; set; }
        
        public AdmPostagemCriarViewModel()
        {
            Titulo = "Criar Postagem";
            Autores = new List<AutorAdmPostagens>();
            Categorias = new List<CategoriaAdmPostagens>();
            Etiquetas = new List<EtiquetaAdmPostagens>();
            
        }
    }

    public class AutorAdmPostagens
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
    public class CategoriaAdmPostagens
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class EtiquetaAdmPostagens
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
    
    
}
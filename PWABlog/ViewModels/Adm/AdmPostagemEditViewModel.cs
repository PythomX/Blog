using System;
using System.Collections.Generic;

namespace PWABlog.ViewModels.Adm
{
    public class AdmPostagemEditViewModel : ViewModelAreaAdministrativa
    {
        
        
        public ICollection<AutorAdmPostagens> Autores { get; set; }
        public ICollection<CategoriaAdmPostagens> Categorias { get; set; }
        public ICollection<EtiquetaAdmPostagens> Etiquetas { get; set; }
        public ICollection<int> EtiquetasPostagem { get; set; }

        public int Id { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataExibicao { get; set; }
        public string Texto { get; set; }
        public string Erro { get; set; }
        
        public AdmPostagemEditViewModel()
        {
            Titulo = "Editar postagem";
            Autores = new List<AutorAdmPostagens>();
            Categorias = new List<CategoriaAdmPostagens>();
            Etiquetas = new List<EtiquetaAdmPostagens>();
            EtiquetasPostagem = new List<int>();
        }
    }
}
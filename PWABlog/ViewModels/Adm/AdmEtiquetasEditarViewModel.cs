﻿﻿using System.Collections.Generic;
 using PWABlog.ViewModels.Adm;

 namespace PWABlog.ViewModels.Adm
{
    public class AdmEtiquetasEditarViewModel : ViewModelAreaAdministrativa
    {
        public int IdEtiqueta { get; set; }
        
        public string NomeEtiqueta { get; set; }
        
        public int IdCategoriaEtiqueta { get; set; }
        
        public string Erro { get; set; }

        public ICollection<CategoriaAdminEtiquetas> Categorias { get; set; }
        
        
        public AdmEtiquetasEditarViewModel()
        {
            Titulo = "Editar Etiqueta: ";
            Categorias = new List<CategoriaAdminEtiquetas>();
        }
    }
}
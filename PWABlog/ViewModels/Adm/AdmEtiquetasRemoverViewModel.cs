﻿using System.Collections.Generic;

namespace PWABlog.ViewModels.Adm
{
    public class AdmEtiquetasRemoverViewModel : ViewModelAreaAdministrativa
    {
        public int IdEtiqueta { get; set; }
        
        public string NomeEtiqueta { get; set; }
        
        public string Erro { get; set; }
        
        public AdmEtiquetasRemoverViewModel()
        {
            Titulo = "Remover Etiqueta: ";
        }
    }
}
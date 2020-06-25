﻿using System.Collections.Generic;

namespace PWABlog.ViewModels.Adm
{
    public class AdmEtiquetasListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<EtiquetaAdminEtiquetas> Etiquetas { get; set; }


        public AdmEtiquetasListarViewModel()
        {
            Titulo = "Etiquetas - Administrador";
            Etiquetas = new List<EtiquetaAdminEtiquetas>();
        }
    }

    public class EtiquetaAdminEtiquetas
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeCategoria { get; set; }
    }
}
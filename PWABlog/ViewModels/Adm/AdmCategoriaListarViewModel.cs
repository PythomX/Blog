﻿using System.Collections.Generic;

 namespace PWABlog.ViewModels.Adm
{
    public class AdmCategoriaListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<CategoriaAdmCategorias> Categorias { get; set; }
        
        public AdmCategoriaListarViewModel()
        {
            Titulo = "Categorias - Administrador";
            Categorias = new List<CategoriaAdmCategorias>();
        }
    }

    public class CategoriaAdmCategorias
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
    }
}
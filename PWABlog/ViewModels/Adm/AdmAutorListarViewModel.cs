﻿using System.Collections.Generic;

 namespace PWABlog.ViewModels.Adm
 {
    public class AdmAutorListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<AutorAdmAutores> Autores { get; set; }
        
        public AdmAutorListarViewModel()
        {
            Titulo = "Autores - Administrador";
            Autores = new List<AutorAdmAutores>();
        }
    }
    
    public class AutorAdmAutores
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
    
}
﻿using System;
 using System.Collections.Generic;

 namespace PWABlog.ViewModels.Adm
{
    public class AdmPostagemListarViewModel : ViewModelAreaAdministrativa
    {
        public ICollection<PostagemAdmPostagens> Postagens { get; set; }
        public AdmPostagemListarViewModel()
        {
            Titulo = "Postagens - Administrador";
            Postagens = new List<PostagemAdmPostagens>();
        }
    }

    public class PostagemAdmPostagens
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public string Autor { get; set; }
        public string DataDeExibicao { get; set; }
        public int Versao { get; set; }
        
    }
}
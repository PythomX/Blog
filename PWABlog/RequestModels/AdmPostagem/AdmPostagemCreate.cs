﻿namespace Blog.RequestModels.AdmPostagem
{
    public class AdmPostagemCreate
    {
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
        public string UrlCapa { get; set; }
        public string Texto { get; set; }
    }
}
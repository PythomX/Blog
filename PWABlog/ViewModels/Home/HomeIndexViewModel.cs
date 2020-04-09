﻿using System.Collections.Generic;

namespace PWABlog.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public string TituloPagina { get; set; }

        public ICollection<PostagemHomeIndex> Postagens { get; set; }
        
        public ICollection<CategoriaHomeIndex> Categorias { get; set; }
        
        public ICollection<EtiquetaHomeIndex> Etiquetas { get; set; }
        
        public ICollection<PostagemPopularHomeIndex> PostagensPopulares { get; set; }

        
        public HomeIndexViewModel()
        {
            Postagens = new List<PostagemHomeIndex>();
            Categorias = new List<CategoriaHomeIndex>();
            Etiquetas = new List<EtiquetaHomeIndex>();
            PostagensPopulares = new List<PostagemPopularHomeIndex>();
        }
    }

    public class PostagemHomeIndex
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public AutorEntity Autor { get; set; }
        public string UrlCapa { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public string UltimaAtualizacaoFormatada
        {
            get
            {
                TimeSpan lastUpdate = DateTime.Now.Subtract(this.UltimaAtualizacao);
                if (lastUpdate.TotalDays > 0)
                {
                    return string.Format("Last updated on {0}", this.UltimaAtualizacao.ToString("MM/dd/YYYY"));
                }
                else
                {
                    return string.Format("{0:D2}:{0:D2}:{1:D2}", (int)lastUpdate.TotalHours, (int)lastUpdate.TotalMinutes, lastUpdate.Seconds);
                }
            }
        }
    }
    
    public class CategoriaHomeIndex
    {
        public string Nome { get; set; }
        public string CategoriaId { get; set; }
    }
    
    public class EtiquetaHomeIndex
    {
        public string Nome { get; set; }
        public string EtiquetaId { get; set; }
    }
    
    public class PostagemPopularHomeIndex
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public AutorEntity Autor { get; set; }
    }
}
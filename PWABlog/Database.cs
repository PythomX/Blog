using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;
using PWABlog.Models.Blog.Postagem.Classificacao;
using PWABlog.Models.Blog.Postagem.Comentario;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PWABlog.Models.ControleAcesso;

namespace PWABlog
{
    public class Database :  IdentityDbContext<Usuario, Papel, int>
    {
        public DbSet<CategoriaEntity> Categorias { get; set; }
        
        public DbSet<AutorEntity> Autores { get; set; }
        
        public DbSet<EtiquetaEntity> Etiquetas { get; set; }
        
        public DbSet<PostagemEntity> Postagens { get; set; }
        
        public DbSet<RevisaoEntity> Revisoes { get; set; }
        
        public DbSet<ComentarioEntity> Comentarios { get; set; }
        
        public DbSet<ClassificacaoEntity> Classificacoes { get; set; }
        
        public DbSet<PostagemEtiquetaEntity> PostagensEtiquetas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql("Server=localhost; User=root; password=!@#$1234; Database=blog");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostagemEtiquetaEntity>().HasKey(pe => new { pe.IdEtiqueta, pe.IdPostagem });


        }

    }
}
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;
using PWABlog.Models.Blog.Postagem.Classificacao;
using PWABlog.Models.Blog.Postagem.Comentario;

namespace PWABlog
{
    public class Database : DbContext
    {
        public DbSet<CategoriaEntity> Categorias { get; set; }
        
        public DbSet<AutorEntity> Autores { get; set; }
        
        public DbSet<EtiquetaEntity> Etiquetas { get; set; }
        
        public DbSet<PostagemEntity> Postagens { get; set; }
        
        public DbSet<RevisaoEntity> Revisoes { get; set; }
        
        public DbSet<ComentarioEntity> Comentarios { get; set; }
        
        public DbSet<ClassificacaoEntity> Classificacoes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql("Server=localhost; User=root; password=!@#$1234; Database=blog");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostagemEtiquetaEntity>().HasKey(pe => new { pe.EtiquetaId, pe.PostagemId });


        }

        public void CreateFakeData()
        {
            // Autores
            var autor = new AutorEntity { Nome = "João Costa", FotoURL = "https://www.stf.jus.br/arquivo/cms/bancoImagemSco/bancoImagemSco_AP_344446.jpg" };
            this.Autores.Add(autor);

            // Categorias
            var categoria = new CategoriaEntity { Nome = "Programação" };
            this.Categorias.Add(categoria);
            this.Categorias.Add(new CategoriaEntity { Nome = "Banco de Dados" });
            this.Categorias.Add(new CategoriaEntity { Nome = "Infraestrutura" });

            // Etiquetas
            this.Etiquetas.Add(new EtiquetaEntity { Cor = "#8a71af", Nome = "DevOps", DataCriacao = DateTime.Now });
            this.Etiquetas.Add(new EtiquetaEntity { Cor = "#719f67", Nome = "Database", DataCriacao = DateTime.Now });
            this.Etiquetas.Add(new EtiquetaEntity { Cor = "#f6a6b2", Nome = "Front-end", DataCriacao = DateTime.Now });
            this.Etiquetas.Add(new EtiquetaEntity { Cor = "#fe5e51", Nome = "Back-end", DataCriacao = DateTime.Now });

            this.SaveChanges();
        }

    }
}
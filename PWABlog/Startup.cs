using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models.Blog.Autor;
using Blog.Models.Blog.Etiqueta;
using Blog.Models.ControleAcesso;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Postagem;

namespace PWABlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            using (var database = new Database())
            {
                database.Database.EnsureDeleted();

                database.Database.EnsureCreated();
            }

            //Email único, e a senha é necessária ter 8 digitos!
            services.AddIdentity<Usuario, Papel>(options => {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<Database>();

            // Adicionar o serviço do banco de dados
            services.AddDbContext<Database>();
            
            // Adicionar os serviços de ORM das entidades do domínio
            services.AddTransient<CategoriaOrmService>();
            services.AddTransient<PostagemOrmService>();
            services.AddTransient<AutorOrmService>();
            services.AddTransient<EtiquetaOrmService>();

            // Adicionar os serviços que possibilitam o funcionamento dos controllers e das views
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "adm.categorias",
                    pattern: "{controller=AdmCategorias}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "adm.etiquetas",
                    pattern: "{controller=AdmEtiqueta}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "adm.autor",
                    pattern: "{controller=AdmAutorController}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "adm.postagem",
                    pattern: "{controller=AdmPostagemController}/{action=Index}/{id?}"
                );
            });
        }
    }
}
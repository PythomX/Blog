using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using PWABlog.Models.Blog.Postagem.Classificacao;
using PWABlog.Models.Blog.Postagem.Comentario;
using PWABlog.Models.Blog.Postagem.Revisao;
using PWABlog.Models.ControleAcesso;

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
                database.Database.EnsureCreated();
            }

            //Email único, e a senha é necessária ter 6 digitos!
            services.AddIdentity<Usuario, Papel>(options => {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<Database>().AddErrorDescriber<DescritorDeErros>();

            //Configurar o mecanismo de controle de acesso
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/acesso/login";
            });
            
            // Adicionar o serviço do controle de acesso
            services.AddTransient<ControleDeAcessoService>();
            
            // Adicionar o serviço do banco de dados
            services.AddDbContext<Database>();
            
            // Adicionar os serviços de ORM das entidades do domínio
            services.AddTransient<AutorOrmService>();
            services.AddTransient<CategoriaOrmService>();
            services.AddTransient<EtiquetaOrmService>();
            services.AddTransient<PostagemOrmService>();
            services.AddTransient<ClassificacaoOrmService>();
            services.AddTransient<ComentarioOrmService>();
            services.AddTransient<RevisaoOrmService>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Rotas do Controle de Acesso
                endpoints.MapControllerRoute(
                    name: "controleDeAcesso",
                    pattern: "acesso/{action}",
                    defaults: new {controller = "ControleDeAcesso", action = "Login"}
                );

                // Rotas da Área Administrativa
                endpoints.MapControllerRoute(
                    name: "adm",
                    pattern: "adm",
                    defaults: new {controller = "Adm", action = "Painel"}
                );
                endpoints.MapControllerRoute(
                    name: "adm.autor",
                    pattern: "adm/autor/{action}/{id?}",
                    defaults: new {controller = "AdmAutor", action = "Listar"}
                );
                endpoints.MapControllerRoute(
                    name: "adm.categoria",
                    pattern: "adm/categoria/{action}/{id?}",
                    defaults: new {controller = "AdmCategoria", action = "Listar"}
                );
                endpoints.MapControllerRoute(
                    name: "adm.etiqueta",
                    pattern: "adm/etiqueta/{action}/{id?}",
                    defaults: new {controller = "AdmEtiqueta", action = "Listar"}
                );
                endpoints.MapControllerRoute(
                    name: "adm.postagem",
                    pattern: "adm/postagem/{action}/{id?}",
                    defaults: new {controller = "AdmPostagem", action = "Listar"}
                );
                
                // Rotas da Área Comum
                endpoints.MapControllerRoute(
                    name: "comum",
                    pattern: "/",
                    defaults: new {controller = "Home", action = "Index"}
                );
            });
        }
    }
}
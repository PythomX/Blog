using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models.ControleAcesso
{
    public class ControleDeAcessoService
    {
        
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;

        public ControleDeAcessoService(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task AutenticarUsuario(string usuario, string senha)
        {
            var result = await signInManager.PasswordSignInAsync(usuario, senha, false, false);
            
            if (!result.Succeeded) {
                throw new Exception("Usuário ou senha inválidos");
            }
        }

        public void DeslogarUsuario()
        {
            signInManager.SignOutAsync();
        }
        
        public async Task RegistrarUsuario(string email, string apelido, string senha)
        {
            var user = new Usuario
            {
                UserName = email,
                Email = email,
                Apelido = apelido
            };

            var result = await userManager.CreateAsync(user, senha);

            if (!result.Succeeded) {
                throw new RegistrarUsuarioException(result.Errors);
            }
        }
        
    }
}
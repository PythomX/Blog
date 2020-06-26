using Microsoft.AspNetCore.Identity;

namespace PWABlog.Models.ControleAcesso
{
    public class Usuario : IdentityUser<int>
    {
        
        public string Apelido { get; set; }
        
    }
}

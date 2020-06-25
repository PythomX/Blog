using System.Collections;
using System.Collections.Generic;

namespace PWABlog.ViewModels.ControleDeAcesso
{
    public class ControleDeAcessoRegistrarViewModel : ViewModelControleDeAcesso
    {
        
        public string Erro { get; set; }
        public IEnumerable ErrosRegistro { get; set; }

        public ControleDeAcessoRegistrarViewModel()
        {
            Titulo = "Registrar - Administrador";

            ErrosRegistro = new List<string>();
        }
        
        
    }
}
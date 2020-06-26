namespace PWABlog.ViewModels.Adm
{
    public class AdmCategoriaCriarViewModel : ViewModelAreaAdministrativa
    {
        
        public string Erro { get; set; }
        
        public AdmCategoriaCriarViewModel()
        {
            Titulo = "Criar nova Categoria";
        }
        
        
        
    }
}
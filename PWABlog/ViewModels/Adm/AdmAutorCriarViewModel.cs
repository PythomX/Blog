namespace PWABlog.ViewModels.Adm
{
    public class AdmAutorCriarViewModel : ViewModelAreaAdministrativa
    {
        public string Erro { get; set; }
        
        public AdmAutorCriarViewModel()
        {
            Titulo = "Autor - Administrador";
        }
    }
}
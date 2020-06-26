namespace PWABlog.ViewModels.Adm
{
    public class AdmPostagemDeleteViewModel : ViewModelAreaAdministrativa
    {
        
        
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string Erro { get; set; }
        
        public AdmPostagemDeleteViewModel()
        {
            Titulo = "Remover Postagem: ";
        }
        
    }
}
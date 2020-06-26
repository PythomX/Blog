namespace PWABlog.ViewModels.Adm
{
    public class AdmCategoriaDeleteViewModel : ViewModelAreaAdministrativa
    {
        
        public int Id { get; set; }
        
        public string Nome { get; set; }

        public string Erro { get; set; }
        
        public AdmCategoriaDeleteViewModel()
        {
            Titulo = "Remover categoria: ";
        }
        
    }
}
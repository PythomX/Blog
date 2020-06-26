namespace PWABlog.ViewModels.Adm
{
    public class AdmCategoriaEditViewModel : ViewModelAreaAdministrativa
    {
        public int Id { get; set; }
        public string Erro { get; set; }
        
        public string Nome { get; set; }
        
        public AdmCategoriaEditViewModel()
        {
            Titulo = "Editar categoria: ";
        }
    }
}
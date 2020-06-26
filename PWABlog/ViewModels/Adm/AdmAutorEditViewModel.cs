namespace PWABlog.ViewModels.Adm
{
    public class AdmAutorEditViewModel : ViewModelAreaAdministrativa
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Erro { get; set; }
        
        
        public AdmAutorEditViewModel()
        {
            Titulo = "Editar Autor: ";
        }
        
    }
}
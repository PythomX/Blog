namespace PWABlog.ViewModels.Adm
{
    public class AdmAutorDeleteViewModel :ViewModelAreaAdministrativa
    {
        
        public int Id { get; set; }
        
        public string Nome { get; set; }

        public string Erro { get; set; }
        
        public AdmAutorDeleteViewModel()
        {
            Titulo = "Remover Autor: ";
        }
        
    }
}
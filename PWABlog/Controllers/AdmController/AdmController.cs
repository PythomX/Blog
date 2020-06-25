using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.ViewModels.Adm;

namespace Blog.ViewMoldels
{
    [Authorize]
    public class AdmController : Controller
    {
        
        [HttpGet]
        public IActionResult Painel()
        {
            AdmPainelViewModel model = new AdmPainelViewModel();
            
            return View(model);
        }
        
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PWABlog.ViewModels.Adm;

namespace PWABlog.Controllers.AdmController
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
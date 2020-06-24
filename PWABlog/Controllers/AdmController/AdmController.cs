using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
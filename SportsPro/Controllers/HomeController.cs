using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        
        public ViewResult Index()
        {
            return View();
        }

        [Route("About")]
        public ViewResult About()
        {
            return View();
        }
    }
}
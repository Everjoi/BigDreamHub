using Microsoft.AspNetCore.Mvc;

namespace MySite.UI.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public  IActionResult Index()
        {
           
			return View("/UI/Views/Home/Index.cshtml");
		}

   
    }
}

using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class HomeController : SessionController
    {
        // GET: Home
        [HttpGet]
        public IActionResult Index() => View();
    }
}
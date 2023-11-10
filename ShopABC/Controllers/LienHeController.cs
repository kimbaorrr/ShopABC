using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class LienHeController : SessionController
    {
        // GET: LienHe
        [HttpGet]
        public IActionResult Index() => View();
    }
}
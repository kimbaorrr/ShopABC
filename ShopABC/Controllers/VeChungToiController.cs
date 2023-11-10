using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class VeChungToiController : SessionController
    {
        // GET: VeChungToi
        [HttpGet]
        public IActionResult Index() => View();
    }
}
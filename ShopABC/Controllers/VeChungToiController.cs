using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class VeChungToiController : SessionController
    {
        // GET: VeChungToi
        [HttpGet]
        [Route("ve-chung-toi")]
        public IActionResult Index() => View();
    }
}
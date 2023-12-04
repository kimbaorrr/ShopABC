using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class LienHeController : SessionController
    {
        // GET: LienHe
        [HttpGet]
        [Route("thong-tin-lien-he")]
        public IActionResult Index() => View();
    }
}
using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class FAQsController : SessionController
    {
        // GET: FAQs
        [HttpGet]
        [Route("cau-hoi-thuong-gap")]
        public IActionResult Index() => View();
    }
}
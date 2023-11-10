using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class FAQsController : SessionController
    {
        // GET: FAQs
        [HttpGet]
        public IActionResult Index() => View();
    }
}
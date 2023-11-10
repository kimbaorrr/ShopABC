using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class ChinhSachController : SessionController
    {
        // GET: ChinhSach
        [HttpGet]
        public IActionResult DoiHang() => View();
        [HttpGet]
        public IActionResult BaoHanh() => View();
        [HttpGet]
        public IActionResult BaoMat() => View();
        [HttpGet]
        public IActionResult HoanTien() => View();
        [HttpGet]
        public IActionResult TuyenDung() => View();
    }
}
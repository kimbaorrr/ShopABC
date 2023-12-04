using Microsoft.AspNetCore.Mvc;
namespace ShopABC.Controllers
{
    public class ChinhSachController : SessionController
    {
        // GET: ChinhSach
        [HttpGet]
        [Route("chinh-sach-doi-hang")]
        public IActionResult DoiHang() => View();
        [HttpGet]
        [Route("chinh-sach-bao-hanh")]
        public IActionResult BaoHanh() => View();
        [HttpGet]
        [Route("chinh-sach-bao-mat")]
        public IActionResult BaoMat() => View();
        [HttpGet]
        [Route("chinh-sach-hoan-tien")]
        public IActionResult HoanTien() => View();
        [HttpGet]
        [Route("thong-tin-tuyen-dung")]
        public IActionResult TuyenDung() => View();
    }
}
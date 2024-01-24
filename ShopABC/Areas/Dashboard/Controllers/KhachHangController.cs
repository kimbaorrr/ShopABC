using Microsoft.AspNetCore.Mvc;

namespace ShopABC.Areas.Dashboard.Controllers
{
    public class KhachHangController : SessionController
    {
        // GET: Dashboard/KhachHang
        [HttpGet]
        [Route("admin/danh-sach-khach-hang")]
        public IActionResult DanhSachKhachHang()
            => View();
        [HttpGet]
        [Route("admin/khach-hang-than-thiet")]
        public IActionResult KhachHangThanThiet()
            => View();
    }
}
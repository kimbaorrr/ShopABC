using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;

namespace ShopABC.Areas.Dashboard.Controllers
{
    public class HeThongController : SessionController
    {
        // GET: Dashboard/HeThong
        [HttpGet]
        [Route("admin/trang-thai-he-thong")]
        public IActionResult TrangThai()
            => View();
        [HttpGet]
        [Route("admin/nhat-ky-truy-cap")]
        public IActionResult NhatKyTruyCap()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).Nhatkytruycap.Value)
                return View();
            return Redirect("404");
        }
    }
}
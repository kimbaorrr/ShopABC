using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;

namespace ShopABC.Areas.Dashboard.Controllers
{
    public class HomeController : SessionController
    {
        // GET: Dashboard/Home
        [HttpGet]
        [Route("admin")]
        public IActionResult Index()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).XemBangdieukhien.Value)
                return View();
            return Redirect("404");
        }
        [HttpPost]
        [Route("admin/doanh-thu-theo-tuan")]
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
        public string DoanhThu()
        {
            try
            {
                if (get_Session().GetString("tendn") != null && get_MaNV_Session() != null && !string.IsNullOrEmpty(get_pkey_Session()))
                    return ShopABC_SanPham.get_DoanhThu_Theo_Tuan();
                return "Không thể vượt qua xác thực !";
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return string.Empty;
        }
    }
}
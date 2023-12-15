using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;

namespace ShopABC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class LoginController : Controller
    {
        // GET: Dashboard/Login
        [HttpGet]
        [Route("admin/dang-nhap-he-thong")]
        public IActionResult Index()
            => View();
        [HttpGet]
        [Route("admin/quen-mat-khau")]
        public IActionResult QuenMatKhau()
            => View();
        [HttpPost, ValidateAntiForgeryToken()]
        [Route("admin/dang-nhap-he-thong")]
        public IActionResult Index(ShopABC_TaiKhoan a)
        {
            try
            {
                bool dn = ShopABC_NhanVien.get_NV_DangNhap().Any(x => x.Tendn.Equals(a.TenDN.Trim().Normalize().ToLower()) && x.Matkhau.Equals(a.MatKhau + ShopABC_TaiKhoan.salt()));
                if (ModelState.IsValid && dn)
                {
                    ISession sess = get_Session();
                    sess.SetString("pkey", ShopABC_TaiKhoan.privateKey());
                    sess.SetString("tendn", a.TenDN);
                    sess.SetInt32("manv", ShopABC_NhanVien.get_MaNV(a.TenDN));
                    ShopABC_TaiKhoan._History(
                        HttpContext.Connection.RemoteIpAddress.ToString(),
                        ShopABC_NhanVien.get_MaNV(a.TenDN),
                        "Đăng nhập",
                        Request.Headers["User-Agent"].ToString()
                        );
                    return Ok();
                }
                return Unauthorized("Cảnh báo: Thông tin đăng nhập không đúng !");
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult QuenMatKhau(ShopABC_QuenMatKhau a)
            => View();
        public ISession get_Session() => HttpContext.Session;
    }
}
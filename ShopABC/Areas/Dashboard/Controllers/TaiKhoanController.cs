using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;

namespace ShopABC.Areas.Dashboard.Controllers
{
    public class TaiKhoanController : SessionController
    {
        // GET: Dashboard/TaiKhoan
        [HttpGet]
        [Route("admin/dang-xuat")]
        public IActionResult Logout()
        {
            get_Session().Clear();
            return RedirectToAction("Index", "Login", new { Area = "Dashboard" });
        }
        [HttpGet]
        [Route("admin/phan-quyen-truy-cap")]
        public IActionResult PhanQuyenTruyCap()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_Session().GetInt32("manv")).Phanquyentruycap.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/ho-so-ca-nhan")]
        public IActionResult HoSoCaNhan() => View();
        // POST Methods
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/doi-mat-khau")]
        public string DoiMatKhau(string mkcu, string mkmoi, string nhaplaimk)
        {
            try
            {
                ShopABC_TaiKhoan a = new ShopABC_TaiKhoan()
                {
                    MaNV = (int)get_Session().GetInt32("manv"),
                    TenDN = get_Session().GetString("tendn"),
                    MatKhau = mkmoi,
                    MatKhauCu = mkcu,
                    NhapLaiMatKhau = nhaplaimk
                };
                a.doiMatKhau();
                return a.ThongBaoLoi;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
    }
}
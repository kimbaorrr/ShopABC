using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;
using ShopABC_DB;

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
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).Phanquyentruycap.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/ho-so-ca-nhan")]
        public IActionResult HoSoCaNhan() => View();
        // POST Methods
        /// <summary>
        /// Đổi mật khẩu nhân viên
        /// </summary>
        /// <param name="mkcu">Truyền tham số Mật khẩu cũ</param>
        /// <param name="mkmoi">Truyền tham số Mật khẩu mới</param>
        /// <returns>Thông báo trạng thái thay đổi</returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/doi-mat-khau")]
        public IActionResult DoiMatKhau(string mkcu, string mkmoi)
        {
            try
            {
                using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                {
                    NvDangnhap a = e.NvDangnhaps.FirstOrDefault(
                        x => x.Manv == get_MaNV_Session() &&
                        x.Tendn.Equals(get_tendn_Session())
                        );
                    if (!mkcu.Equals(a.Matkhau))
                        return Unauthorized("Mật khẩu cũ không chính xác !");
                    a.Matkhau = mkmoi;
                    e.SaveChanges();
                    ShopABC_TaiKhoan._History(
                        get_IP_Addr(),
                        get_MaNV_Session(),
                        "Đổi mật khẩu",
                        get_User_Agent()
                        );
                    return Ok("Đổi mật khẩu thành công !");
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
    }
}
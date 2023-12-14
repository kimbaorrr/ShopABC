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
        /// <param name="nhaplaimk">Truyền tham số Nhập lại mật khẩu</param>
        /// <returns>Thông báo trạng thái thay đổi</returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/doi-mat-khau")]
        public string DoiMatKhau(string mkcu, string mkmoi, string nhaplaimk)
        {
            try
            {
                using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                {

                    NvDangnhap a = e.NvDangnhaps.FirstOrDefault(x => x.Manv == (int)get_MaNV_Session() && x.Tendn.Equals(get_tendn_Session()));
                    if (!mkmoi.Equals(nhaplaimk))
                        return "Mật khẩu mới và nhập lại không chính xác !";
                    if (!ShopABC_TaiKhoan.hash_MatKhau(mkcu).Equals(a.Matkhau))
                        return "Mật khẩu cũ không chính xác !";
                    a.Matkhau = ShopABC_TaiKhoan.hash_MatKhau(mkmoi);
                    e.SaveChanges();
                    return "Đổi mật khẩu thành công !";
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
    }
}
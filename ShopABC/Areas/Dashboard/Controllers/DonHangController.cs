using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;

namespace ShopABC.Areas.Dashboard.Controllers
{
    public class DonHangController : SessionController
    {
        // GET: Dashboard/DonHang
        [HttpGet]
        [Route("admin/danh-sach-don-hang")]
        public IActionResult DanhSachDonHang() => View();
        [HttpGet]
        [Route("admin/don-hang-da-huy")]
        public IActionResult DaHuy() => View();
        [HttpGet]
        [Route("admin/don-hang-cho-giao")]
        public IActionResult ChoGiaoHang()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_Session().GetInt32("manv")).XacnhanDonhang.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/don-hang-dang-giao")]
        public IActionResult DangGiao() => View();
        [HttpGet]
        [Route("admin/don-hang-giao-thanh-cong")]
        public IActionResult GiaoThanhCong() => View();
        [HttpGet]
        [Route("admin/don-hang/{hdid?}/{pkey?}")]
        public IActionResult ChiTietDonHang(int hdid, string pkey)
        {
            try
            {
                if (pkey.Equals(get_Session().GetString("pkey")))
                    ViewData["TTDonHang"] = ShopABC_DonHang.get_CTDonHang_Theo_SoHD(hdid);
                return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        // POST Methods
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/bat-dau-giao-hang")]
        public string XacNhan_GiaoHang(int hdid, string hd)
        {
            try
            {
                ShopABC_DonHang b = new ShopABC_DonHang();
                b.SoHD = hdid;
                b.xacnhan_GiaoHang(hd);
                return b.ThongBaoLoi;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/xac-nhan-da-giao-hang")]
        public string XacNhan_DaGiao(int hdid, string hd, string lydo = null)
        {
            try
            {
                ShopABC_DonHang c = new ShopABC_DonHang();
                c.SoHD = hdid;
                c.xacnhan_DaGiao(hd);
                return c.ThongBaoLoi;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;
using ShopABC_DB;

namespace ShopABC.Areas.Dashboard.Controllers
{
    public class DonHangController : SessionController
    {
        // GET: Dashboard/DonHang
        [HttpGet]
        [Route("admin/danh-sach-don-hang")]
        public IActionResult DanhSachDonHang()
            => View();
        [HttpGet]
        [Route("admin/don-hang-da-huy")]
        public IActionResult DaHuy()
            => View();
        [HttpGet]
        [Route("admin/don-hang-cho-giao")]
        public IActionResult ChoGiaoHang()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).XacnhanDonhang.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/don-hang-dang-giao")]
        public IActionResult DangGiao()
            => View();
        [HttpGet]
        [Route("admin/don-hang-giao-thanh-cong")]
        public IActionResult GiaoThanhCong()
            => View();
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
        /// <summary>
        /// Chuyển đổi trạng thái từ "Chờ giao" => "Đang giao" bằng cách thay đổi giá trị cột "MaTT" trên CSDL
        /// </summary>
        /// <param name="hdid">Số hóa đơn</param>
        /// <param name="hd">Hành động</param>
        /// <returns>Thông báo trạng thái của đơn hàng</returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/bat-dau-giao-hang")]
        public string XacNhan_GiaoHang(int hdid, string hd)
        {
            try
            {
                using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                {
                    Donhang a = e.Donhangs.FirstOrDefault(x => x.Sohd == hdid);
                    switch (hd)
                    {
                        case "xacnhan":
                            a.Matt = 2;
                            a.Ngaygiao = DateTime.Now;
                            e.SaveChanges();
                            return $"Đã xác nhận đơn hàng a.Sohd !\nĐơn hàng sẽ chuyển sang trạng thái Giao hàng !";
                        case "huydon":
                            a.Matt = 1;
                            a.Ngayhuy = DateTime.Now;
                            e.SaveChanges();
                            return $"Đã hủy đơn hàng {a.Sohd} !";
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return string.Empty;
        }
        /// <summary>
        /// Chuyển đổi trạng thái từ "Đang giao" => "Đã giao" bằng cách thay đổi giá trị cột "MaTT" trên CSDL
        /// </summary>
        /// <param name="hdid">Số hóa đơn</param>
        /// <param name="hd">Hành động</param>
        /// <param name="lydo">Lý do giao không thành công</param>
        /// <returns>Thông báo trạng thái của đơn hàng</returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/xac-nhan-da-giao-hang")]
        public string XacNhan_DaGiao(int hdid, string hd, string lydo = null)
        {
            try
            {
                using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                {
                    Donhang a = e.Donhangs.FirstOrDefault(x => x.Sohd == hdid);
                    switch (hd)
                    {
                        case "dagiaohang":
                            a.Matt = 6;
                            a.Ngaynhan = DateTime.Now;
                            e.SaveChanges();
                            return $"Đơn hàng {a.Sohd} đã giao thành công !";
                        case "khongthegiao":
                            a.Matt = 7;
                            a.Lydo = "Không xác định";
                            e.SaveChanges();
                            return $"Xác nhận đơn hàng {a.Sohd} giao không thành công !\nLý do: {a.Lydo}";
                    }
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return string.Empty;
        }
    }
}
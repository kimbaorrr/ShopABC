using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;
using ShopABC_DB;

namespace ShopABC.Areas.Dashboard.Controllers
{
    public class SanPhamController : SessionController
    {
        [HttpGet]
        [Route("admin/them-san-pham")]
        public IActionResult ThemSanPham()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_Session().GetInt32("manv")).ThemSanpham.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/danh-sach-san-pham")]
        public IActionResult DanhSachSanPham()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_Session().GetInt32("manv")).XemSanphamDssp.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/duyet-san-pham")]
        public IActionResult DuyetSanPham()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_Session().GetInt32("manv")).DuyetSanpham.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/san-pham/{p?}/{spid?}/{pkey?}")]
        public IActionResult ChiTietSanPham(string p, int spid, string pkey)
        {
            try
            {
                if (pkey.Equals(get_Session().GetString("pkey")))
                {
                    ShopABC_ChiTietSanPham a = new ShopABC_ChiTietSanPham();
                    Sanpham l = ShopABC_SanPham.get_SanPham_Theo_MaSP(spid);
                    a.MaSP = l.Masp;
                    a.TenSP = l.Tensp;
                    a.GiaBan = l.Giaban;
                    a.GiamGia = l.Giamgia;
                    a.KieuDang = l.Kieudang;
                    a.ChatLieu = l.Chatlieu;
                    a.MaDM = l.Madm;
                    a.MaHSX = l.Mahsx;
                    a.MaLoai = l.MadmNavigation.MaloaiNavigation.Maloai;
                    a.MaMau = l.Mamau;
                    get_Session().SetString("HinhSP", l.Hinhsp);
                    a.MaNV = (int)l.Manv;
                    a.NgayNhap = l.Ngaynhap.Value;
                    return View(a);
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        // POST Method
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(ShopABC_ChiTietSanPham a)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        a.MaNV = (int)get_Session().GetInt32("manv");
                        a.them_SanPham();
                        set_ThongBao(a.ThongBaoLoi.Item1, a.ThongBaoLoi.Item2);
                        if (a.ThongBaoLoi.Item1.Contains("thành công"))
                            log_History("Thêm sản phẩm " + a.ThongBaoLoi.Item3);
                    }
                    catch (Exception ex)
                    {
                        set_ThongBao("Lỗi hệ thống !" + "\n" + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") + " " + ex.Message, 2);
                        return View();
                    }
                    ModelState.Clear();
                }
                return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ChiTietSanPham(ShopABC_ChiTietSanPham a, string hanhdong)
        {
            try
            {
                if (ModelState.IsValid)
                    switch (hanhdong)
                    {
                        case "suadoi":
                            a.sua_SanPham();
                            set_ThongBao(a.ThongBaoLoi.Item1, a.ThongBaoLoi.Item2);
                            if (a.ThongBaoLoi.Item1.Contains("thành công"))
                                log_History("Sửa sản phẩm " + a.ThongBaoLoi.Item3);
                            return View(a);
                        case "xoabo":
                            a.xoa_SanPham();
                            set_ThongBao(a.ThongBaoLoi.Item1, a.ThongBaoLoi.Item2);
                            if (string.IsNullOrEmpty(ViewBag.ThongBao))
                            {
                                log_History("Xóa sản phẩm " + a.ThongBaoLoi.Item3);
                                return RedirectToAction("DanhSachSanPham", "SanPham");
                            }
                            return View(a);
                    }
                return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public string DuyetSanPham(int spid, string hd)
        {
            try
            {
                string b = ShopABC_ChiTietSanPham.duyet_SanPham(spid, hd, get_Session().GetInt32("manv"));
                switch (hd)
                {
                    case "duyetbai":
                        log_History("Duyệt sản phẩm " + spid);
                        break;
                    case "huybo":
                        log_History("Hủy duyệt sản phẩm " + spid);
                        break;
                }
                return b;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
    }
}
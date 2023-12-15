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
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).ThemSanpham.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/danh-sach-san-pham")]
        public IActionResult DanhSachSanPham()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).XemSanphamDssp.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/duyet-san-pham")]
        public IActionResult DuyetSanPham()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).DuyetSanpham.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/san-pham/{spid?}/{pkey?}")]
        public IActionResult ChiTietSanPham(int spid, string pkey, string returnURL)
        {
            try
            {
                if (get_pkey_Session().Equals(pkey))
                {
                    Sanpham l = ShopABC_SanPham.get_SanPham_Theo_MaSP(spid);
                    ShopABC_ChiTietSanPham a = new ShopABC_ChiTietSanPham()
                    {
                        MaSP = l.Masp,
                        TenSP = l.Tensp,
                        GiaBan = l.Giaban,
                        GiamGia = l.Giamgia,
                        KieuDang = l.Kieudang,
                        ChatLieu = l.Chatlieu,
                        MaDM = l.Madm,
                        MaHSX = l.Mahsx,
                        MaLoai = l.MadmNavigation.MaloaiNavigation.Maloai,
                        MaMau = l.Mamau,
                        MaNV = (int)l.Manv,
                        NgayNhap = l.Ngaynhap.Value
                    };
                    get_Session().SetString("HinhSP", l.Hinhsp);
                    ViewBag.returnURL = returnURL;
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
        [Route("admin/them-san-pham")]
        public IActionResult ThemSanPham(ShopABC_ChiTietSanPham a)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                    {
                        if (ShopABC_SanPham.get_SanPham().Any(x => x.Tensp.Equals(a.TenSP)))
                        {
                            set_ThongBao("Tên sản phẩm đã tồn tại trên hệ thống !", 1);
                            return View(a);
                        }
                        if (!ShopABC_SanPham.get_DanhMucSP().Any(x => x.Madm == a.MaDM && x.Maloai == a.MaLoai))
                        {
                            set_ThongBao("Danh mục không khớp với Loại sản phẩm đã chọn !", 1);
                            return View(a);
                        }
                        string kt_tep = kiemtra_Tep(a);
                        if (string.IsNullOrEmpty(kt_tep))
                        {
                            Sanpham sp = new Sanpham()
                            {
                                Madm = a.MaDM,
                                Tensp = a.TenSP,
                                Chatlieu = a.ChatLieu,
                                Kieudang = a.KieuDang,
                                Giaban = a.GiaBan,
                                Giamgia = a.GiamGia,
                                Mahsx = a.MaHSX,
                                Hinhsp = string.Join("#", a.rand_HinhSP),
                                Mamau = a.MaMau,
                                Duyet = false,
                                Manv = a.MaNV,
                                Mota = a.MoTa,
                                Noidung = a.NoiDung,
                                Thuevat = a.ThueVAT
                            };
                            e.Sanphams.Add(sp);
                            e.SaveChanges();
                            set_ThongBao("Thêm sản phẩm thành công !", 0);
                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            set_ThongBao(kt_tep, 1);
                            return View(a);
                        }
                    }

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
        [Route("admin/san-pham/{spid?}/{pkey?}")]
        public IActionResult ChiTietSanPham(ShopABC_ChiTietSanPham a, string hd, string returnURL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                    {
                        Sanpham sp = e.Sanphams.FirstOrDefault(x => x.Masp == a.MaSP);
                        switch (hd)
                        {
                            case "suadoi":
                                string kt_sp = kiemTra_SanPham(sp, a);
                                if (string.IsNullOrEmpty(kt_sp))
                                {
                                    sp.Tensp = a.TenSP;
                                    sp.Mota = a.MoTa;
                                    sp.Chatlieu = a.ChatLieu;
                                    sp.Kieudang = a.KieuDang;
                                    sp.Giaban = a.GiaBan;
                                    sp.Giamgia = a.GiamGia;
                                    sp.Mamau = a.MaMau;
                                    sp.Madm = a.MaDM;
                                    sp.MadmNavigation.Maloai = a.MaLoai;
                                    sp.Noidung = a.NoiDung;
                                    sp.Duyet = false;
                                    sp.Manv = a.MaNV;
                                    sp.Mahsx = a.MaHSX;
                                    sp.Ngaynhap = a.NgayNhap;
                                    sp.Thuevat = a.ThueVAT;
                                    e.SaveChanges();
                                    set_ThongBao("Sửa sản phẩm thành công !", 0);
                                }
                                else
                                {
                                    set_ThongBao(kt_sp, 1);
                                }
                                return View(a);
                            case "xoabo":
                                if (ShopABC_DonHang.get_ChiTietDonHang().Any(x => x.Masp == a.MaSP))
                                {
                                    set_ThongBao("Sản phẩm đã có đơn hàng. Không thể xóa !", 1);
                                    return View(a);
                                }
                                // Xóa các hình ảnh đã lưu
                                foreach (string i in sp.Hinhsp.Split("#"))
                                    ShopABC_Tools.del_Image($"SanPham/{i}");
                                e.Sanphams.Remove(sp);
                                e.SaveChanges();
                                log_History($"Xóa sản phẩm {a.MaSP}");
                                return Redirect(returnURL);
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        /// <summary>
        /// Duyệt sản phẩm
        /// </summary>
        /// <param name="spid">Mã sản phẩm</param>
        /// <param name="hd">Hành động</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/duyet-san-pham")]
        public IActionResult DuyetSanPham(int spid, string hd)
        {
            try
            {
                using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                {
                    Sanpham a = e.Sanphams.FirstOrDefault(x => x.Masp == spid);
                    switch (hd)
                    {
                        case "duyet":
                            a.Duyet = true;
                            a.Ngayduyet = DateTime.Now;
                            a.Nguoiduyet = get_MaNV_Session();
                            e.SaveChanges();
                            log_History($"Duyệt sản phẩm {spid} !");
                            return Ok($"Duyệt sản phẩm {spid} thành công !");
                        case "huybo":
                            foreach (string i in a.Hinhsp.Split("#"))
                                ShopABC_Tools.del_Image($"SanPham/{i}");
                            e.Sanphams.Remove(a);
                            e.SaveChanges();
                            log_History($"Hủy duyệt sản phẩm {spid} !");
                            return Ok($"Đã hủy sản phẩm {spid} !");
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return NotFound();
        }
        private string kiemtra_Tep(ShopABC_ChiTietSanPham a)
        {
            byte dem = 0;
            a.rand_HinhSP = new string[a.HinhSP.Count];
            foreach (IFormFile in_hinhsp in a.HinhSP)
            {
                if (a.HinhSP.Count == 0)
                    return "Chưa chọn tệp tải lên !";
                if (a.HinhSP.Count > 5)
                    return "Không được tải lên nhiều hơn 5 tệp !";
                if (in_hinhsp.Length <= 0)
                    return $"Tệp thứ {dem + 1} rỗng !";
                if (in_hinhsp.Length >= 10485760)
                    return $"Dung lượng tệp thứ {dem + 1} không được vượt quá 10MB !";
                if (!in_hinhsp.ContentType.Contains("image/"))
                    return $"Tệp thứ {dem + 1} không đúng định dạng !";
                string randName = $"sp-{DateTime.Now.ToString("ddMMyyyyHHmmssfffff")}.webp";
                string today = DateTime.Now.ToString("ddMMyyyy");
                Directory.CreateDirectory($"wwwroot/uploads/images/SanPham/{today}");
                using (FileStream stream = new FileStream($"wwwroot/uploads/images/SanPham/{today}/{randName}.webp", FileMode.Create))
                    in_hinhsp.CopyTo(stream);
                a.rand_HinhSP[dem] = randName;
                dem++;
            }
            return string.Empty;
        }
        private string kiemTra_SanPham(Sanpham sp, ShopABC_ChiTietSanPham a)
        {
            if (!sp.Tensp.Equals(a.TenSP))
            {
                if (ShopABC_SanPham.get_SanPham().Any(x => x.Tensp.Equals(a.TenSP)))
                    return "Đã có sản phẩm cùng tên trên hệ thống. Không thể sửa !";
            }
            if (ShopABC_DonHang.get_ChiTietDonHang().Any(x => x.Masp == a.MaSP))
                return "Sản phẩm này đã có đơn đặt hàng. Không thể sửa !";
            if (!ShopABC_SanPham.get_DanhMucSP().Any(x => x.Madm == a.MaDM && x.Maloai == a.MaDM))
                return "Danh mục không khớp với Loại sản phẩm đã chọn !";
            return string.Empty;
        }
    }
}
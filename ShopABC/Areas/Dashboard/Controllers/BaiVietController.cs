using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;
using ShopABC_DB;

namespace ShopABC.Areas.Dashboard.Controllers
{
    public class BaiVietController : SessionController
    {
        // GET: Dashboard/BaiViet
        [HttpGet]
        [Route("admin/dang-bai-viet")]
        public IActionResult DangBaiViet()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).ThemBaiviet.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/duyet-bai-viet")]
        public IActionResult DuyetBaiViet()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).DuyetBaiviet.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/tat-ca-bai-viet")]
        public IActionResult TatCaBaiViet()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_MaNV_Session()).XemBaivietTatcabaiviet.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/bai-viet-cua-toi")]
        public IActionResult BaiVietCuaToi()
            => View();
        [HttpGet]
        [Route("admin/bai-viet/{bvid?}/{pkey?}")]
        public IActionResult ChiTietBaiViet(int bvid, string pkey, string returnURL)
        {
            try
            {
                if (pkey.Equals(get_Session().GetString("pkey")))
                {
                    Baiviet bv = ShopABC_BaiViet.get_BaiViet_Theo_ID(bvid);
                    ShopABC_ChiTietBaiViet a = new ShopABC_ChiTietBaiViet()
                    {
                        TieuDe = bv.Tieude,
                        NoiDung = bv.Noidung,
                        NgayDang = bv.Ngaydang.Value,
                        MaBV = bv.Mabv,
                        SoLanDoc = bv.Luotxem,
                    };
                    get_Session().SetString("HinhBV", bv.Hinhbv);
                    get_Session().SetInt32("bv-manv", bv.Manv);
                    if (bv.Draft == false)
                        a.IsPublic = true;
                    else
                        a.IsPublic = false;
                    ViewBag.returnURL = returnURL;
                    return View(a);
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/Dashboard/404");
        }
        // POST Methods
        /// <summary>
        /// Đăng bài viết
        /// </summary>
        /// <param name="a">Đối tượng ChiTietBaiViet</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/dang-bai-viet")]
        public IActionResult DangBaiViet(ShopABC_ChiTietBaiViet a)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                    {
                        try
                        {
                            string kt_Tep = kiemtra_Tep(a);
                            if (string.IsNullOrEmpty(kt_Tep))
                            {
                                Baiviet bv = new Baiviet()
                                {
                                    Manv = a.MaNV,
                                    Tieude = a.TieuDe,
                                    Noidung = a.NoiDung,
                                    Duyet = a.Duyet,
                                    Hinhbv = a.rand_HinhBV,
                                    Draft = a.IsDraft
                                };
                                e.Baiviets.Add(bv);
                                e.SaveChanges();
                                log_History($"Thêm bài viết {bv.Mabv}"); // Lưu log
                                set_ThongBao("Thêm bài viết thành công !", 0);
                                return View();
                            }
                            else
                            {
                                set_ThongBao(kt_Tep, 1);
                                return View(a);
                            }
                        }
                        catch (Exception ex)
                        {
                            ShopABC_CSDL.log_errs(ex.Message);
                        }
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
        /// <summary>
        /// Duyệt bài viết
        /// </summary>
        /// <param name="bvid">Mã bài viết</param>
        /// <param name="hd">Hành động/Thao tác</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/duyet-bai-viet")]
        public IActionResult DuyetBaiViet(int bvid, string hd)
        {
            try
            {
                using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                {
                    Baiviet a = e.Baiviets.FirstOrDefault(x => x.Mabv == bvid);
                    switch (hd)
                    {
                        case "duyet":
                            a.Duyet = true;
                            a.Ngayduyet = DateTime.Now;
                            a.Nguoiduyet = get_MaNV_Session();
                            e.SaveChanges();
                            ShopABC_TaiKhoan._History(
                                get_IP_Addr(),
                                get_MaNV_Session(),
                                $"Xuất bản bài viết {bvid} !",
                                get_User_Agent()
                                );
                            return Ok($"Đã duyệt bài viết {bvid} !");
                        case "huybo":
                            ShopABC_Tools.del_Image($"Blog/{a.Hinhbv}");
                            e.Baiviets.Remove(a);
                            e.SaveChanges();
                            ShopABC_TaiKhoan._History(
                                get_IP_Addr(),
                                get_MaNV_Session(),
                                $"Hủy xuất bản & xóa bài viết {bvid}",
                                get_User_Agent()
                                );
                            return Ok($"Đã hủy bài viết {bvid} !");
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
        /// <summary>
        /// Cập nhật & xóa bài viết
        /// </summary>
        /// <param name="a">Đối tượng ChiTietBaiViet chứa thông tin bài viết cụ thể</param>
        /// <param name="hanhdong">Hành động/Thao tác</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        [Route("admin/bai-viet/{bvid?}/{pkey?}")]
        public IActionResult ChiTietBaiViet(ShopABC_ChiTietBaiViet a, string hd, string returnURL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                    {
                        Baiviet bv = e.Baiviets.FirstOrDefault(x => x.Mabv == a.MaBV);
                        switch (hd)
                        {
                            case "suadoi":
                                bv.Tieude = a.TieuDe;
                                bv.Noidung = a.NoiDung;
                                bv.Ngaydang = a.NgayDang;
                                bv.Duyet = false;
                                if (a.IsPublic)
                                    bv.Draft = false;
                                else
                                    bv.Draft = true;
                                e.SaveChanges();
                                set_ThongBao($"Sửa bài viết thành công !", 0);
                                ShopABC_TaiKhoan._History(
                                    get_IP_Addr(),
                                    get_MaNV_Session(),
                                    $"Sửa bài viết {a.MaBV}",
                                    get_User_Agent()
                                    );
                                return View(a);
                            case "xoabo":
                                ShopABC_Tools.del_Image($"Blog/{bv.Hinhbv}");
                                List<BvBinhluan> bl = e.BvBinhluans.Where(c => c.Mabv == bv.Mabv).ToList();
                                e.BvBinhluans.RemoveRange(bl);
                                e.Baiviets.Remove(bv);
                                e.SaveChanges();
                                ShopABC_TaiKhoan._History(
                                get_IP_Addr(),
                                get_MaNV_Session(),
                                $"Xóa bài viết {a.MaBV}",
                                get_User_Agent()
                                );
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
        /// Thực hiện kiểm tra tệp và thêm tệp vào hệ thống
        /// </summary>
        /// <param name="a">Đối tượng Chi tiết Bài viết</param>
        /// <returns>Chuỗi rỗng nếu không có lỗi</returns>
        private string kiemtra_Tep(ShopABC_ChiTietBaiViet a)
        {
            if (a.HinhBV == null)
                return "Chưa chọn tệp tải lên !";
            if (a.HinhBV.Length <= 0)
                return "Tệp rỗng !";
            if (a.HinhBV.Length >= 10485760)
                return "Dung lượng tệp không được vượt quá 10MB ! !";
            if (!a.HinhBV.ContentType.Contains("image/"))
                return "Tệp không đúng định dạng !";
            // Tải ảnh lên Server
            string randName = $"blog-{DateTime.Now.ToString("ddMMyyyyHHmmssfffff")}.webp";
            using (FileStream stream = new FileStream($"wwwroot/uploads/images/Blog/{randName}", FileMode.Create))
                a.HinhBV.CopyTo(stream);
            a.rand_HinhBV = randName;
            return string.Empty;
        }
    }
}
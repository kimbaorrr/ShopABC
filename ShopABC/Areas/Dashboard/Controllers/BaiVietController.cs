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
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_Session().GetInt32("manv")).ThemBaiviet.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/duyet-bai-viet")]
        public IActionResult DuyetBaiViet()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_Session().GetInt32("manv")).DuyetBaiviet.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/tat-ca-bai-viet")]
        public IActionResult TatCaBaiViet()
        {
            if (ShopABC_NhanVien.get_PhanQuyen_NhanVien(get_Session().GetInt32("manv")).XemBaivietTatcabaiviet.Value)
                return View();
            return Redirect("404");
        }
        [HttpGet]
        [Route("admin/bai-viet-cua-toi")]
        public IActionResult BaiVietCuaToi()
        {
            return View();
        }
        [HttpGet]
        [Route("admin/bai-viet/{p?}/{bvid?}/{pkey?}")]
        public IActionResult ChiTietBaiViet(string p, int bvid, string pkey)
        {
            try
            {
                if (pkey.Equals(get_Session().GetString("pkey")))
                {
                    ShopABC_ChiTietBaiViet a = new ShopABC_ChiTietBaiViet();
                    Baiviet bv = ShopABC_BaiViet.get_BaiViet_Theo_ID(bvid);
                    a.TieuDe = bv.Tieude;
                    a.NoiDung = bv.Noidung;
                    get_Session().SetString("HinhBV", bv.Hinhbv);
                    get_Session().SetInt32("bv-manv", bv.Manv);
                    a.NgayDang = bv.Ngaydang.Value;
                    a.MaBV = bv.Mabv;
                    a.SoLanDoc = bv.Luotxem;
                    if (bv.Draft == false)
                        a.IsPublic = true;
                    else
                        a.IsPublic = false;
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
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DangBaiViet(ShopABC_ChiTietBaiViet a, string hanhdong = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
                    {
                        try
                        {
                            if (kiemtra_Tep(a))
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
                            }
                        }
                        catch (Exception ex)
                        {
                            ShopABC_CSDL.log_errs(ex.Message);
                        }
                    }
                }
                ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public string DuyetBaiViet(int bvid, string hd)
        {
            try
            {
                string a = ShopABC_ChiTietBaiViet.duyet_BaiViet(bvid, hd, get_Session().GetInt32("manv"));
                switch (hd)
                {
                    case "duyetbai":
                        log_History("Đã xuất bản bài viết " + bvid + " !");
                        break;
                    case "huybo":
                        log_History("Hủy bài viết " + bvid + " !");
                        break;
                }
                return a;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ChiTietBaiViet(ShopABC_ChiTietBaiViet a, string hanhdong)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    switch (hanhdong)
                    {
                        case "suadoi":
                            a.sua_BaiViet();
                            set_ThongBao(a.ThongBaoLoi.Item1, a.ThongBaoLoi.Item2);
                            if (a.ThongBaoLoi.Item1.Contains("thành công"))
                                log_History("Sửa bài viết " + a.ThongBaoLoi.Item3);
                            return View(a);
                        case "xoabo":
                            a.xoa_BaiViet();
                            log_History("Xóa bài viết " + a.ThongBaoLoi.Item3);
                            return RedirectToAction("BaiVietCuaToi", "BaiViet");
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

        private bool kiemtra_Tep(ShopABC_ChiTietBaiViet a)
        {
            try
            {
                if (a.HinhBV == null)
                {
                    a.ThongBaoLoi = Tuple.Create<string, byte, int>("Chưa chọn tệp tải lên !", 1, a.MaBV);
                    return false;
                }
                if (a.HinhBV.Length <= 0)
                {
                    a.ThongBaoLoi = Tuple.Create<string, byte, int>("Tệp rỗng !", 1, a.MaBV);
                    return false;
                }
                if (a.HinhBV.Length >= 10485760)
                {
                    a.ThongBaoLoi = Tuple.Create<string, byte, int>("Dung lượng tệp không được vượt quá 10MB ! !", 1, a.MaBV);
                    return false;
                }
                if (!a.HinhBV.ContentType.Contains("image/"))
                {
                    a.ThongBaoLoi = Tuple.Create<string, byte, int>("Tệp không đúng định dạng !", 1, a.MaBV);
                    return false;
                }
                string randName = "blog-" + DateTime.Now.ToString("ddMMyyyyHHmmssfffff") + ".webp";
                using (FileStream stream = new FileStream("wwwroot/uploads/images/Blog/" + randName, FileMode.Create))
                    a.HinhBV.CopyTo(stream);
                a.rand_HinhBV = randName;

            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return true;
        }
    }
}
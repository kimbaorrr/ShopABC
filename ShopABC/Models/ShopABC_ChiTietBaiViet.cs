using ShopABC_DB;
using System.ComponentModel.DataAnnotations;
namespace ShopABC.Models
{
    public class ShopABC_ChiTietBaiViet
    {
        [DataType(DataType.MultilineText, ErrorMessage = "Giá trị không hợp lệ !")]
        [MaxLength(255, ErrorMessage = "Tiêu đề không được vượt quá 255 kí tự !")]
        [Required(ErrorMessage = "Trường này không được bỏ trống !")]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }
        [DataType(DataType.MultilineText, ErrorMessage = "Giá trị không hợp lệ !")]
        [Display(Name = "Nội dung bài viết")]
        public string NoiDung { get; set; }
        [Display(Name = "Ngày đăng")]
        [DataType(DataType.DateTime, ErrorMessage = "Giá trị không hợp lệ !")]
        public DateTime? NgayDang { get; set; }
        public Tuple<string, byte, int> ThongBaoLoi { get; set; }
        [Display(Name = "Nhân viên")]
        public int MaNV { get; set; }
        [Display(Name = "Hình bài viết")]
        public IFormFile HinhBV { get; set; }
        public int MaBV { get; set; }
        public bool Duyet { get; set; }
        private string rand_HinhBV { get; set; }
        [Display(Name = "Số lần đọc")]
        public int? SoLanDoc { get; set; }
        public bool IsDraft { get; set; }
        [Display(Name = "Công khai (ON)")]
        public bool IsPublic { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_ChiTietBaiViet()
        {
            this.MaBV = 0;
            this.TieuDe = null;
            this.MaNV = 0;
            this.NoiDung = null;
            this.HinhBV = null;
            this.Duyet = false;
            this.ThongBaoLoi = Tuple.Create<string, byte, int>(null, 0, 0);
            this.SoLanDoc = 0;
            this.NgayDang = DateTime.Now;
            this.IsDraft = false;
            this.IsPublic = false;
        }
        /// <summary>
        /// Khởi gán giá trị cho các biến từ đối tượng truyền vào
        /// </summary>
        /// <param name="a">Truyền tham số của Đối tượng</param>
        public ShopABC_ChiTietBaiViet(ShopABC_ChiTietBaiViet a)
        {
            this.MaBV = a.MaBV;
            this.TieuDe = a.TieuDe;
            this.NoiDung = a.NoiDung;
            this.MaNV = a.MaNV;
            this.NgayDang = a.NgayDang;
            this.HinhBV = a.HinhBV;
            this.Duyet = a.Duyet;
            this.ThongBaoLoi = a.ThongBaoLoi;
            this.SoLanDoc = a.SoLanDoc;
            this.IsDraft = a.IsDraft;
            this.IsPublic = a.IsPublic;
        }
        /// <summary>
        /// Thêm mới một bài viết vào CSDL
        /// </summary>
        /// <param name="manv">Truyền tham số Mã Nhân viên</param>
        /// <param name="hinhsp">Truyền tham số Hình sản phẩm</param>
        /// <returns>Nội dung thông báo & Mã trạng thái</returns>
        public void dang_BaiViet()
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        if (kiemtra_Tep(this))
                        {
                            Baiviet bv = new Baiviet()
                            {
                                Manv = this.MaNV,
                                Tieude = this.TieuDe,
                                Noidung = this.NoiDung,
                                Duyet = this.Duyet,
                                Hinhbv = this.rand_HinhBV,
                                Draft = this.IsDraft
                            };
                            e.Baiviets.Add(bv);
                            e.SaveChanges();
                            e.Database.CommitTransaction();
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>("Thêm bài viết thành công !", 0, bv.Mabv);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShopABC_CSDL.log_errs(ex.Message);
                        e.Database.RollbackTransaction();
                    }
                }
            }
        }
        /// <summary>
        /// Cập nhật thông tin Bài viết trên CSDL
        /// </summary>
        /// <returns>Nội dung thông báo & Mã trạng thái</returns>
        public void sua_BaiViet()
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        Baiviet bv = e.Baiviets.Where(x => x.Mabv == this.MaBV).FirstOrDefault();
                        bv.Tieude = this.TieuDe;
                        bv.Noidung = this.NoiDung;
                        bv.Ngaydang = this.NgayDang;
                        bv.Duyet = false;
                        if (this.IsPublic)
                            bv.Draft = false;
                        else
                            bv.Draft = true;
                        e.SaveChanges();
                        e.Database.CommitTransaction();
                        this.ThongBaoLoi = Tuple.Create<string, byte, int>("Sửa bài viết thành công !", 0, bv.Mabv);
                    }
                    catch (Exception ex)
                    {
                        ShopABC_CSDL.log_errs(ex.Message);
                        e.Database.RollbackTransaction();
                    }
                }
            }
        }
        /// <summary>
        /// Xóa bài viết trên CSDL
        /// </summary>
        public void xoa_BaiViet()
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        Baiviet bv = e.Baiviets.Where(x => x.Mabv == this.MaBV).FirstOrDefault();
                        File.Delete(@"wwwroot\\uploads\\images\\Blog\\" + bv.Hinhbv);
                        e.Baiviets.Remove(bv);
                        e.SaveChanges();
                        e.Database.CommitTransaction();
                        this.ThongBaoLoi = Tuple.Create<string, byte, int>(null, 0, this.MaBV);
                    }
                    catch (Exception ex)
                    {
                        ShopABC_CSDL.log_errs(ex.Message);
                        e.Database.RollbackTransaction();
                    }
                }
            }
        }
        /// <summary>
        /// Duyệt bài viết bằng cách thay đổi giá trị cột Duyet trên CSDL
        /// </summary>
        /// <param name="mabv">Truyền tham số Mã bài viết</param>
        /// <param name="hd">Truyền tham số Hành động</param>
        /// <returns></returns>
        public static string duyet_BaiViet(int manv, string hd, int? nguoiduyet)
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        Baiviet a = e.Baiviets.Where(x => x.Mabv == manv).FirstOrDefault();
                        switch (hd)
                        {
                            case "duyetbai":
                                a.Duyet = true;
                                a.Ngayduyet = DateTime.Now;
                                a.Nguoiduyet = nguoiduyet;
                                e.SaveChanges();
                                e.Database.CommitTransaction();
                                return "Duyệt bài viết " + manv + " thành công !";
                            case "huybo":
                                File.Delete(@"wwwroot\\uploads\\images\\Blog\\" + a.Hinhbv);
                                e.Baiviets.Remove(a);
                                e.SaveChanges();
                                e.Database.CommitTransaction();
                                return "Đã hủy bài viết " + manv + " !";
                        }
                    }
                    catch (Exception ex)
                    {
                        ShopABC_CSDL.log_errs(ex.Message);
                        e.Database.RollbackTransaction();
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// Thực hiện kiểm tra tệp và thêm tệp vào hệ thống
        /// </summary>
        /// <param name="a">Bài viết</param>
        /// <returns>Đúng/Sai</returns>
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
        /// <summary>
        /// Cập nhật số lần đọc (lượt xem) của bài viết
        /// </summary>
        /// <param name="bvid">Mã bài viết</param>
        public static void capNhat_SoLanDoc(int bvid)
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                try
                {
                    Baiviet n = e.Baiviets.FirstOrDefault(x => x.Mabv == bvid);
                    n.Luotxem++;
                    e.SaveChanges();
                }
                catch (Exception ex)
                {
                    ShopABC_CSDL.log_errs(ex.Message);
                }
            }
        }
    }
}
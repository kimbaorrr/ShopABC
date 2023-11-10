using ShopABC_DB;
using System.ComponentModel.DataAnnotations;
namespace ShopABC.Models
{
    public class ShopABC_ChiTietSanPham
    {
        [DataType(DataType.Text, ErrorMessage = "Giá trị không hợp lệ !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [MaxLength(140, ErrorMessage = "Độ dài không được vượt quá 100 kí tự !")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }
        [DataType(DataType.Currency, ErrorMessage = "Giá trị không hợp lệ !")]
        [Range(1000, double.MaxValue, ErrorMessage = "Giá bán phải >= 1000 đ")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Giá trị không hợp lệ !")]
        [Display(Name = "Giá bán")]
        public int? GiaBan { get; set; }
        [DataType(DataType.Currency, ErrorMessage = "Giá trị không hợp lệ !")]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Giá trị không hợp lệ !")]
        [Range(0, 90, ErrorMessage = "Giá trị phải trong khoảng 0 -> 90% !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Giảm giá")]
        public short? GiamGia { get; set; }
        [MaxLength(50, ErrorMessage = "Độ dài không được vượt quá 50 kí tự !")]
        [Display(Name = "Chất liệu")]
        [DataType(DataType.Text, ErrorMessage = "Giá trị không hợp lệ !")]
        public string ChatLieu { get; set; }
        [MaxLength(50, ErrorMessage = "Độ dài không được vượt quá 50 kí tự !")]
        [DataType(DataType.Text, ErrorMessage = "Giá trị không hợp lệ !")]
        [Display(Name = "Kiểu dáng")]
        public string KieuDang { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Loại")]
        public int MaLoai { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Danh mục")]
        public int MaDM { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Hãng SX")]
        public int MaHSX { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Màu sắc")]
        public string MaMau { get; set; }
        public Tuple<string, byte, int> ThongBaoLoi { get; set; }
        [DataType(DataType.MultilineText, ErrorMessage = "Giá trị không hợp lệ !")]
        [MaxLength(255, ErrorMessage = "Độ dài không được vượt quá 255 kí tự !")]
        [Display(Name = "Mô tả sản phẩm")]
        public string MoTa { get; set; }
        [DataType(DataType.MultilineText, ErrorMessage = "Giá trị không hợp lệ !")]
        [Display(Name = "Nội dung giới thiệu")]
        public string NoiDung { get; set; }
        public bool Duyet { get; set; }
        public List<IFormFile> HinhSP { get; set; }
        public int MaSP { get; set; }
        [Display(Name = "Thuế VAT")]
        [Range(1, 40, ErrorMessage = "Giá trị Thuế VAT không hợp lệ !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        public short ThueVAT { get; set; }
        public int MaNV { get; set; }
        public DateTime? NgayNhap { get; set; }
        private string[] rand_HinhSP { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_ChiTietSanPham()
        {
            this.MaSP = 0;
            this.TenSP = null;
            this.GiaBan = 0;
            this.GiamGia = 0;
            this.ChatLieu = null;
            this.KieuDang = null;
            this.MaLoai = 0;
            this.MaDM = 0;
            this.MaHSX = 0;
            this.MaMau = null;
            this.MoTa = null;
            this.NoiDung = null;
            this.HinhSP = new List<IFormFile>();
            this.Duyet = false;
            this.ThueVAT = 8;
            this.NgayNhap = DateTime.Now;
            this.ThongBaoLoi = new Tuple<string, byte, int>(null, 0, 0);
        }
        /// <summary>
        /// Khởi gán giá trị cho biến từ đối tượng truyền vào
        /// </summary>
        /// <param name="a">Truyền tham số Đối tượng</param>
        public ShopABC_ChiTietSanPham(ShopABC_ChiTietSanPham a)
        {
            this.MaSP = a.MaSP;
            this.TenSP = a.TenSP;
            this.GiaBan = a.GiaBan;
            this.GiamGia = a.GiamGia;
            this.ChatLieu = a.ChatLieu;
            this.KieuDang = a.KieuDang;
            this.MaLoai = a.MaLoai;
            this.MaDM = a.MaDM;
            this.MaHSX = a.MaHSX;
            this.MaMau = a.MaMau;
            this.Duyet = a.Duyet;
            this.MoTa = a.MoTa;
            this.NoiDung = a.NoiDung;
            this.HinhSP = a.HinhSP;
            this.NgayNhap = a.NgayNhap;
            this.ThueVAT = a.ThueVAT;
            this.ThongBaoLoi = a.ThongBaoLoi;
            this.NgayNhap = a.NgayNhap;
        }
        /// <summary>
        /// Thêm một sản phẩm vào CSDL
        /// </summary>
        public void them_SanPham()
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        bool chk_SanPham = ShopABC_chk_DieuKien.chk_SanPham_TonTai(this.TenSP);
                        if (chk_SanPham)
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>("Sản phẩm đã tồn tại trên hệ thống", 1, 0);
                        bool chk_DanhMuc = ShopABC_chk_DieuKien.chk_DanhMuc_TonTai_LoaiSP(this.MaLoai, this.MaDM);
                        if (!chk_DanhMuc)
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>("Danh mục không khớp với Loại sản phẩm đã chọn", 1, 0);
                        if (kiemtra_Tep(this) && string.IsNullOrEmpty(this.ThongBaoLoi.Item1))
                        {
                            Sanpham sp = new Sanpham()
                            {
                                Madm = this.MaDM,
                                Tensp = this.TenSP,
                                Chatlieu = this.ChatLieu,
                                Kieudang = this.KieuDang,
                                Giaban = this.GiaBan,
                                Giamgia = this.GiamGia,
                                Mahsx = this.MaHSX,
                                Hinhsp = string.Join("#", this.rand_HinhSP),
                                Mamau = this.MaMau,
                                Duyet = false,
                                Manv = this.MaNV,
                                Mota = this.MoTa,
                                Noidung = this.NoiDung,
                                Thuevat = this.ThueVAT
                            };
                            e.Sanphams.Add(sp);
                            e.SaveChanges();
                            e.Database.CommitTransaction();
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>("Thêm sản phẩm thành công !", 0, this.MaSP);
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
        /// Cập nhật thông tin sản phẩm trên CSDL
        /// </summary>
        public void sua_SanPham()
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        Sanpham sp = e.Sanphams.Where(x => x.Masp == this.MaSP).FirstOrDefault();
                        if (!sp.Tensp.Equals(this.TenSP))
                        {
                            bool chk_SanPham_TonTai = ShopABC_chk_DieuKien.chk_SanPham_TonTai(this.TenSP);
                            if (chk_SanPham_TonTai)
                                this.ThongBaoLoi = Tuple.Create<string, byte, int>("Tên sản phẩm đã tồn tại. Không thể sửa !", 1, 0);
                        }
                        bool chk_SanPham_DonHang = ShopABC_chk_DieuKien.chk_SanPham_TonTai_DonHang(this.MaSP);
                        if (chk_SanPham_DonHang)
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>("Sản phẩm đã có đơn hàng. Không thể sửa !", 1, 0);
                        bool chk_DanhMuc = ShopABC_chk_DieuKien.chk_DanhMuc_TonTai_LoaiSP(this.MaLoai, this.MaDM);
                        if (!chk_DanhMuc)
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>("Danh mục không khớp với Loại sản phẩm đã chọn !", 1, 0);
                        if (string.IsNullOrEmpty(this.ThongBaoLoi.Item1))
                        {
                            sp.Tensp = this.TenSP;
                            sp.Mota = this.MoTa;
                            sp.Chatlieu = this.ChatLieu;
                            sp.Kieudang = this.KieuDang;
                            sp.Giaban = this.GiaBan;
                            sp.Giamgia = this.GiamGia;
                            sp.Mamau = this.MaMau;
                            sp.Madm = this.MaDM;
                            sp.MadmNavigation.Maloai = this.MaLoai;
                            sp.Noidung = this.NoiDung;
                            sp.Duyet = false;
                            sp.Manv = this.MaNV;
                            sp.Mahsx = this.MaHSX;
                            sp.Ngaynhap = this.NgayNhap;
                            sp.Thuevat = this.ThueVAT;
                            e.SaveChanges();
                            e.Database.CommitTransaction();
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>("Sửa sản phẩm thành công !", 0, 0);
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
        /// Xóa sản phẩm trên CSDL
        /// </summary>
        public void xoa_SanPham()
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        Sanpham sp = e.Sanphams.Where(x => x.Masp == this.MaSP).FirstOrDefault();
                        bool chk_SanPham_DonHang = ShopABC_chk_DieuKien.chk_SanPham_TonTai_DonHang(sp.Masp);
                        if (chk_SanPham_DonHang)
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>("Sản phẩm đã có đơn hàng. Không thể xóa !", 1, 0);
                        if (string.IsNullOrEmpty(this.ThongBaoLoi.Item1))
                        {
                            // Xóa các hình ảnh đã lưu
                            foreach (string i in sp.Hinhsp.Split("#"))
                                File.Delete(@"wwwroot\\uploads\\images\\SanPham\\" + i);
                            e.Sanphams.Remove(sp);
                            e.SaveChanges();
                            e.Database.CommitTransaction();
                            this.ThongBaoLoi = Tuple.Create<string, byte, int>(null, 0, sp.Masp);
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
        /// Duyệt sản phẩm
        /// </summary>
        /// <param name="masp">Mã sản phẩm</param>
        /// <param name="hd">Hành động</param>
        /// <param name="nguoiduyet">Người duyệt (Mã nhân viên)</param>
        /// <returns></returns>
        public static string duyet_SanPham(int masp, string hd, int? nguoiduyet)
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        Sanpham a = e.Sanphams.Where(x => x.Masp == masp).FirstOrDefault();
                        switch (hd)
                        {
                            case "duyet":
                                a.Duyet = true;
                                a.Ngayduyet = DateTime.Now;
                                a.Nguoiduyet = nguoiduyet;
                                e.SaveChanges();
                                e.Database.CommitTransaction();
                                return "Duyệt sản phẩm " + masp + " thành công !";
                            case "huybo":
                                foreach (string i in a.Hinhsp.Split("#"))
                                    File.Delete(@"wwwroot\\uploads\\images\\SanPham\\" + i);
                                e.Sanphams.Remove(a);
                                e.SaveChanges();
                                e.Database.CommitTransaction();
                                return "Đã hủy sản phẩm " + masp + " !";
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
        private bool kiemtra_Tep(ShopABC_ChiTietSanPham a)
        {
            try
            {
                byte dem = 0;
                a.rand_HinhSP = new string[a.HinhSP.Count];
                foreach (IFormFile in_hinhsp in a.HinhSP)
                {
                    if (a.HinhSP.Count == 0)
                    {
                        a.ThongBaoLoi = Tuple.Create<string, byte, int>("Chưa chọn tệp tải lên !", 1, a.MaSP);
                        return false;
                    }
                    if (a.HinhSP.Count > 5)
                    {
                        a.ThongBaoLoi = Tuple.Create<string, byte, int>("Không được tải lên nhiều hơn 5 tệp !", 1, a.MaSP);
                        return false;
                    }
                    if (in_hinhsp.Length <= 0)
                    {
                        a.ThongBaoLoi = Tuple.Create<string, byte, int>("Tệp thứ " + (dem + 1) + " rỗng !", 1, a.MaSP);
                        return false;
                    }
                    if (in_hinhsp.Length >= 10485760)
                    {
                        a.ThongBaoLoi = Tuple.Create<string, byte, int>("Dung lượng tệp thứ " + (dem + 1) + " không được vượt quá 10MB ! !", 1, a.MaSP);
                        return false;
                    }
                    if (!in_hinhsp.ContentType.Contains("image/"))
                    {
                        a.ThongBaoLoi = Tuple.Create<string, byte, int>("Tệp thứ " + (dem + 1) + " không đúng định dạng ! !", 1, a.MaSP);
                        return false;
                    }
                    string randName = "sp-" + DateTime.Now.ToString("ddMMyyyyHHmmssfffff") + ".webp";
                    string today = DateTime.Now.ToString("ddMMyyyy");
                    Directory.CreateDirectory("wwwroot/uploads/images/SanPham/" + today);
                    using (FileStream stream = new FileStream("wwwroot/uploads/images/SanPham/" + today + "/" + randName, FileMode.Create))
                        in_hinhsp.CopyTo(stream);
                    a.rand_HinhSP[dem] = randName;
                    dem++;
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return true;
        }
    }
}
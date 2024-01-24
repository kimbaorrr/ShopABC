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
        public string[] rand_HinhSP { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_ChiTietSanPham()
        {
            MaSP = 0;
            TenSP = string.Empty;
            GiaBan = 0;
            GiamGia = 0;
            ChatLieu = string.Empty;
            KieuDang = string.Empty;
            MaLoai = 0;
            MaDM = 0;
            MaHSX = 0;
            MaMau = string.Empty;
            MoTa = string.Empty;
            NoiDung = string.Empty;
            HinhSP = new List<IFormFile>();
            Duyet = false;
            ThueVAT = 8;
            NgayNhap = DateTime.Now;
            ThongBaoLoi = new Tuple<string, byte, int>(null, 0, 0);
        }
        /// <summary>
        /// Khởi gán giá trị cho biến từ đối tượng truyền vào
        /// </summary>
        /// <param name="a">Truyền tham số Đối tượng</param>
        public ShopABC_ChiTietSanPham(ShopABC_ChiTietSanPham a)
        {
            MaSP = a.MaSP;
            TenSP = a.TenSP;
            GiaBan = a.GiaBan;
            GiamGia = a.GiamGia;
            ChatLieu = a.ChatLieu;
            KieuDang = a.KieuDang;
            MaLoai = a.MaLoai;
            MaDM = a.MaDM;
            MaHSX = a.MaHSX;
            MaMau = a.MaMau;
            Duyet = a.Duyet;
            MoTa = a.MoTa;
            NoiDung = a.NoiDung;
            HinhSP = a.HinhSP;
            NgayNhap = a.NgayNhap;
            ThueVAT = a.ThueVAT;
            ThongBaoLoi = a.ThongBaoLoi;
            NgayNhap = a.NgayNhap;
        }
        /// <summary>
        /// Thêm một sản phẩm vào CSDL
        /// </summary>
        public void them_SanPham()
        {

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
                        Sanpham sp = e.Sanphams.Where(x => x.Masp == MaSP).FirstOrDefault();
                        if (!sp.Tensp.Equals(TenSP))
                        {
                            bool chk_SanPham_TonTai = ShopABC_chk_DieuKien.chk_SanPham_TonTai(TenSP);
                            if (chk_SanPham_TonTai)
                                ThongBaoLoi = Tuple.Create<string, byte, int>("Tên sản phẩm đã tồn tại. Không thể sửa !", 1, 0);
                        }
                        bool chk_SanPham_DonHang = ShopABC_chk_DieuKien.chk_SanPham_TonTai_DonHang(MaSP);
                        if (chk_SanPham_DonHang)
                            ThongBaoLoi = Tuple.Create<string, byte, int>("Sản phẩm đã có đơn hàng. Không thể sửa !", 1, 0);
                        bool chk_DanhMuc = ShopABC_chk_DieuKien.chk_DanhMuc_TonTai_LoaiSP(MaLoai, MaDM);
                        if (!chk_DanhMuc)
                            ThongBaoLoi = Tuple.Create<string, byte, int>("Danh mục không khớp với Loại sản phẩm đã chọn !", 1, 0);
                        if (string.IsNullOrEmpty(ThongBaoLoi.Item1))
                        {
                            sp.Tensp = TenSP;
                            sp.Mota = MoTa;
                            sp.Chatlieu = ChatLieu;
                            sp.Kieudang = KieuDang;
                            sp.Giaban = GiaBan;
                            sp.Giamgia = GiamGia;
                            sp.Mamau = MaMau;
                            sp.Madm = MaDM;
                            sp.MadmNavigation.Maloai = MaLoai;
                            sp.Noidung = NoiDung;
                            sp.Duyet = false;
                            sp.Manv = MaNV;
                            sp.Mahsx = MaHSX;
                            sp.Ngaynhap = NgayNhap;
                            sp.Thuevat = ThueVAT;
                            e.SaveChanges();
                            e.Database.CommitTransaction();
                            ThongBaoLoi = Tuple.Create<string, byte, int>("Sửa sản phẩm thành công !", 0, 0);
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

                    }
                    catch (Exception ex)
                    {
                        ShopABC_CSDL.log_errs(ex.Message);
                        e.Database.RollbackTransaction();
                    }
                }
            }
        }
    }
}
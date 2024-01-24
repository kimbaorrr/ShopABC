using ShopABC_DB;
using System.ComponentModel.DataAnnotations;
namespace ShopABC.Models
{
    public class ShopABC_ThanhToan
    {
        [DataType(DataType.Text)]
        [MaxLength(35, ErrorMessage = "Họ đệm không được vượt quá 35 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Họ đệm")]
        public string HoDem { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(15, ErrorMessage = "Tên không được vượt quá 15 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Tên")]
        public string TenKH { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [DataType(DataType.Text)]
        [MaxLength(35, ErrorMessage = "Địa chỉ Email không được vượt quá 35 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Ghi chú không được vượt quá 255 kí tự !")]
        [Display(Name = "Ghi chú giao hàng")]
        public string GhiChu { get; set; }
        [Display(Name = "Ngày giao dự kiến")]
        public DateTime NgayGiao { get; set; }
        public int TongGT { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_ThanhToan()
        {
            HoDem = null;
            TenKH = null;
            DiaChi = null;
            SDT = null;
            Email = null;
            GhiChu = null;
            TongGT = 0;
            NgayGiao = DateTime.Today.AddDays(5);
        }
        /// <summary>
        /// Khởi gán giá trị cho biến từ đối tượng truyền vào
        /// </summary>
        /// <param name="a">Truyền tham số của Đối tượng</param>
        public ShopABC_ThanhToan(ShopABC_ThanhToan a)
        {
            HoDem = a.HoDem;
            TenKH = a.TenKH;
            DiaChi = a.DiaChi;
            SDT = a.SDT;
            Email = a.Email;
            GhiChu = a.GhiChu;
            TongGT = a.TongGT;
            NgayGiao = a.NgayGiao;
        }
        /// <summary>
        /// Thêm một đơn hàng vào CSDL 
        /// </summary>
        /// <param name="list">Truyền tham số Danh sách chi tiết đơn hàng</param>
        /// <param name="tonggt">Truyền tham số Tổng giá trị tất cả sản phẩm</param>
        public int tao_DonHang(IList<Chitietdonhang> list, int? tonggt)
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        int sohd = 0;
                        if (ShopABC_chk_DieuKien.chk_KhachHang_TonTai(HoDem, TenKH, SDT))
                        {
                            Khachhang kh = ShopABC_KhachHang.get_KhachHang().FirstOrDefault(x => x.Hodem.Equals(HoDem) && x.Tenkh.Equals(TenKH) && x.Sdt.Equals(SDT));
                            sohd = ThucThi(e, kh, list, tonggt);
                        }
                        else
                        {
                            Khachhang kh = new Khachhang()
                            {
                                Hodem = HoDem,
                                Tenkh = TenKH,
                                Email = Email,
                                Sdt = SDT,
                                Diachi = DiaChi
                            };
                            e.Khachhangs.Add(kh);
                            e.SaveChanges();
                            sohd = ThucThi(e, kh, list, tonggt);
                        }
                        e.SaveChanges();
                        e.Database.CurrentTransaction.Commit();
                        return sohd;
                    }
                    catch (Exception ex)
                    {
                        ShopABC_CSDL.log_errs(ex.Message);
                        e.Database.CurrentTransaction.Rollback();
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// Thực thi câu lệnh thêm đơn hàng vào CSDL
        /// </summary>
        /// <param name="e">Đường kết nối</param>
        /// <param name="a">Khách hàng</param>
        /// <param name="ct_donhang">Chi tiết đơn hàng của Khách Hàng</param>
        private int ThucThi(ShopABC_Entities e, Khachhang a, IList<Chitietdonhang> ct_donhang, int? tonggt)
        {
            Donhang dh = new Donhang()
            {
                Makh = a.Makh,
                Tonggt = tonggt,
                Ghichu = GhiChu,
                Matt = 5,
                Ngaygiao = NgayGiao
            };
            e.Donhangs.Add(dh);
            e.SaveChanges();
            foreach (Chitietdonhang x in ct_donhang)
            {
                Chitietdonhang ct = new Chitietdonhang()
                {
                    Sohd = dh.Sohd,
                    Masp = x.Masp,
                    Soluong = x.Soluong,
                    Thanhtien = x.Thanhtien,
                };
                e.Chitietdonhangs.Add(ct);
            }
            return dh.Sohd;
        }
    }
}
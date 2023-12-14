using ShopABC_DB;
using System.ComponentModel.DataAnnotations;
namespace ShopABC.Models
{
    public class ShopABC_TaiKhoan
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Trường này không được bỏ trống !")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 kí tự !")]
        [RegularExpression(@"[a-zA-Z0-9]*$", ErrorMessage = "Tên đăng nhập không được chứa ký tự đặc biệt !")]
        [Display(Name = "Tên đăng nhập")]
        public string TenDN { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Trường này không được bỏ trống !")]
        [MaxLength(255, ErrorMessage = "Mật khẩu không được vượt quá 255 kí tự !")]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
        public string MatKhauCu { get; set; }
        public string NhapLaiMatKhau { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_TaiKhoan()
        {
            TenDN = string.Empty;
            MatKhau = string.Empty;
        }
        /// <summary>
        /// Khởi gán giá trị cho biến khi truyền đối tượng vào
        /// </summary>
        /// <param name="a">Truyền tham số Đối tượng</param>
        public ShopABC_TaiKhoan(ShopABC_TaiKhoan a)
        {
            TenDN = a.TenDN;
            MatKhau = a.MatKhau;
        }
        /// <summary>
        /// Kiểm tra thông tin đăng nhập của nhân viên có trùng khớp ?
        /// </summary>
        /// <returns>Khớp/Không khớp</returns>
        public static string privateKey()
         => Guid.NewGuid().ToString().Replace("-", "");
        public static string salt()
            => File.ReadAllText("salt.txt");
        /// <summary>
        /// Ghi nhật ký truy cập trang Quản trị
        /// </summary>
        /// <param name="diachiip">Truyền tham số Địa chỉ IP</param>
        /// <param name="manv">Truyền tham số Mã nhân viên</param>
        public static void _History(string diachiip, int? manv, string hanhdong, string userAgent)
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                try
                {
                    History a = new History()
                    {
                        Diachiip = diachiip,
                        Thoigian = DateTime.Now,
                        Hanhdong = hanhdong,
                        Manv = manv,
                        UserAgent = userAgent
                    };
                    e.Histories.Add(a);
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
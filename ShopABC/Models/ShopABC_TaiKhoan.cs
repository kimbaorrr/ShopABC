using Org.BouncyCastle.Crypto.Digests;
using ShopABC_DB;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
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
        public int MaNV { get; set; }
        public string ThongBaoLoi { get; set; }
        [Range(000000, 999999, ErrorMessage = "Mã xác thực không hợp lệ !")]
        [DataType(DataType.Text)]
        [Display(Name = "Mã xác thực")]
        public int MaXacMinh { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Giá trị không hợp lệ !")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_TaiKhoan()
        {
            this.MaNV = 0;
            this.TenDN = null;
            this.MatKhau = null;
            this.ThongBaoLoi = null;
        }
        /// <summary>
        /// Khởi gán giá trị cho biến khi truyền đối tượng vào
        /// </summary>
        /// <param name="a">Truyền tham số Đối tượng</param>
        public ShopABC_TaiKhoan(ShopABC_TaiKhoan a)
        {
            this.MaNV = a.MaNV;
            this.TenDN = a.TenDN;
            this.MatKhau = a.MatKhau;
            this.ThongBaoLoi = a.ThongBaoLoi;
        }
        /// <summary>
        /// Kiểm tra thông tin đăng nhập của nhân viên có trùng khớp ?
        /// </summary>
        /// <returns>Khớp/Không khớp</returns>
        public bool dangNhap()
        {
            try
            {
                byte chk_DangNhap = (byte)ShopABC_NhanVien.get_NV_DangNhap().Count(x => x.Tendn.Equals(this.TenDN.ToLower().Trim()) && x.Matkhau.Equals(hash_MatKhau(this.MatKhau)));
                return chk_DangNhap == 1;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// Đổi mật khẩu nhân viên
        /// </summary>
        /// <param name="manv">Truyền tham số Mã nhân viên</param>
        /// <param name="mkcu">Truyền tham số Mật khẩu cũ</param>
        /// <param name="mkmoi">Truyền tham số Mật khẩu mới</param>
        /// <param name="nhaplaimk">Truyền tham số Nhập lại mật khẩu</param>
        /// <returns>Thông báo trạng thái thay đổi</returns>
        public void doiMatKhau()
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        NvDangnhap a = e.NvDangnhaps.Where(x => x.Manv == this.MaNV && x.Tendn.Equals(this.TenDN)).FirstOrDefault();
                        if (!this.MatKhau.Equals(this.NhapLaiMatKhau))
                            this.ThongBaoLoi = "Mật khẩu mới và nhập lại không chính xác !";
                        if (!hash_MatKhau(this.MatKhauCu).Equals(a.Matkhau))
                            this.ThongBaoLoi = "Mật khẩu cũ không chính xác !";
                        a.Matkhau = hash_MatKhau(this.MatKhau);
                        e.SaveChanges();
                        e.Database.CommitTransaction();
                        this.ThongBaoLoi = "Đổi mật khẩu thành công !";
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
        /// Quên mật khẩu
        /// </summary>
        /// <returns></returns>
        public bool quen_MatKhau()
        {
            try
            {
                int a = new Random().Next(111111, 999999);
                using (SmtpClient smtp = new SmtpClient())
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        smtp.EnableSsl = true;
                        smtp.Port = 465;
                        smtp.Credentials = new NetworkCredential("nguyenkimbao.0708@gmail.com", "#Trucquynh0912@!!!");
                        mail.From = new MailAddress("nguyenkimbao.0708@gmail.com", "Nguyễn Kim Bảo");
                        mail.To.Add(this.Email);
                        mail.Subject = "Khôi phục mật khẩu";
                        mail.Priority = MailPriority.High;
                        mail.Body = "Mã xác thực của bạn là: " + a;
                        smtp.Send(mail);
                        this.ThongBaoLoi = "Đã gửi mã xác thực !";
                    }
                }
                return this.MaXacMinh == a;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// Đọc chuỗi ngẫu nhiên từ tệp salt.txt trên ổ đĩa D:\Web
        /// </summary>
        /// <returns>Chuỗi ngẫu nhiên</returns>
        private string salt()
        {
            try
            {
                return File.ReadAllText("salt.txt");
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Kết hợp 3 chuỗi kí tự ngẫu nhiên thành 1 chuỗi duy nhất để xác minh danh tính
        /// </summary>
        /// <returns>Chuỗi ngẫu nhiên</returns>
        public static string privateKey()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        /// Thực hiện băm mật khẩu bằng thuật toán băm SHA3-512 bits trên CSDL
        /// </summary>
        /// <param name="matkhau">Truyền tham số Mật khẩu gốc</param>
        /// <returns>Mật khẩu đã được băm</returns>
        private string hash_MatKhau(string matkhau)
        {
            try
            {
                Sha3Digest hash = new Sha3Digest(512);
                byte[] input = Encoding.ASCII.GetBytes(tron_Khoa(matkhau));
                hash.BlockUpdate(input, 0, input.Length);
                byte[] result = new byte[64];
                hash.DoFinal(result, 0);
                return BitConverter.ToString(result).Replace("-", "").ToLower();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
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
        /// <summary>
        /// Tạo sự phức tạp cho khóa bằng cách trộn khóa
        /// </summary>
        /// <param name="matkhau">Mật khẩu gốc</param>
        /// <returns>Chuỗi mật khẩu và muối sau khi trộn</returns>
        private string tron_Khoa(string matkhau)
        {
            try
            {
                StringBuilder e = new StringBuilder();
                for (byte i = 0; i <= 8; i++)
                    e.Append(matkhau + salt() + "$@");
                return e.ToString();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return string.Empty;
        }
    }
}
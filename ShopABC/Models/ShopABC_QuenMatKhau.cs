using System.Net;
using System.Net.Mail;

namespace ShopABC.Models
{
    public class ShopABC_QuenMatKhau
    {
        public int MaXacMinh { get; set; }
        public string Email { get; set; }
        public string ThongBaoLoi { get; set; }
        public int MaGoc { get; set; }
        public ShopABC_QuenMatKhau()
        {
            MaXacMinh = MaGoc = 0;
            Email = null;
            MaGoc = 0;
        }
        public ShopABC_QuenMatKhau(ShopABC_QuenMatKhau a)
        {
            a.MaXacMinh = MaXacMinh;
            a.Email = Email;
            a.MaGoc = 0;
        }
        public bool quen_MatKhau()
        {
            try
            {
                MaGoc = new Random().Next(111111, 999999);
                return MaXacMinh == MaGoc;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return false;
        }
        public void gui_MaXacMinh(string email)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                using (MailMessage mail = new MailMessage())
                {
                    smtp.EnableSsl = true;
                    smtp.Port = 465;
                    smtp.Credentials = new NetworkCredential("nguyenkimbao.0708@gmail.com", "#Trucquynh0912@!!!");
                    mail.From = new MailAddress("nguyenkimbao.0708@gmail.com", "Nguyễn Kim Bảo");
                    mail.To.Add(email);
                    mail.Subject = "Khôi phục mật khẩu";
                    mail.Priority = MailPriority.High;
                    mail.Body = "Mã xác minh của bạn là: " + MaGoc;
                    smtp.Send(mail);
                }
            }
        }
    }
}

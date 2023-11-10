using ShopABC_DB;
using System.ComponentModel.DataAnnotations;

namespace ShopABC.Models
{
    public class ShopABC_ChiTietBaiViet_BinhLuan
    {
        public int MaBV { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Giá trị không hợp lệ !")]
        [MaxLength(100, ErrorMessage = "Họ tên không được vượt quá 100 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        public string HoTen { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Giá trị không hợp lệ !")]
        [MaxLength(100, ErrorMessage = "Email không được vượt quá 100 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        public string Email { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Giá trị không hợp lệ !")]
        [MaxLength(100, ErrorMessage = "Số điện thoại không được vượt quá 10 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        public string SDT { get; set; }
        [DataType(DataType.MultilineText, ErrorMessage = "Giá trị không hợp lệ !")]
        [MaxLength(100, ErrorMessage = "Nội dung không được vượt quá 255 kí tự !")]
        [Required(ErrorMessage = "Trường này không được để trống !")]
        public string BinhLuan { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_ChiTietBaiViet_BinhLuan()
        {
            this.MaBV = 0;
            this.HoTen = this.Email = this.SDT = this.BinhLuan = string.Empty;
        }
        /// <summary>
        /// Object Contructors
        /// </summary>
        /// <param name="a"></param>
        public ShopABC_ChiTietBaiViet_BinhLuan(ShopABC_ChiTietBaiViet_BinhLuan a)
        {
            this.MaBV = a.MaBV;
            this.BinhLuan = a.BinhLuan;
            this.HoTen = a.HoTen;
            this.Email = a.Email;
        }
        /// <summary>
        /// Thêm một bình luận mới vào CSDL
        /// </summary>
        public void them_BinhLuan()
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        BvBinhluan a = new BvBinhluan()
                        {
                            Mabv = this.MaBV,
                            Hoten = this.HoTen,
                            Email = this.Email,
                            Sdt = this.SDT,
                            Binhluan = this.BinhLuan
                        };
                        e.Add(a);
                        e.SaveChanges();
                        e.Database.CommitTransaction();
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

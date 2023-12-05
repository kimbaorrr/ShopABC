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
        [Display(Name = "Nhân viên")]
        public int MaNV { get; set; }
        [Display(Name = "Hình bài viết")]
        public IFormFile HinhBV { get; set; }
        public int MaBV { get; set; }
        public bool Duyet { get; set; }
        public string rand_HinhBV { get; set; }
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
            this.TieuDe = string.Empty;
            this.MaNV = 0;
            this.NoiDung = string.Empty;
            this.HinhBV = null;
            this.Duyet = false;
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
            this.SoLanDoc = a.SoLanDoc;
            this.IsDraft = a.IsDraft;
            this.IsPublic = a.IsPublic;
        }
    }
}
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
            MaBV = 0;
            TieuDe = string.Empty;
            MaNV = 0;
            NoiDung = string.Empty;
            HinhBV = null;
            Duyet = false;
            SoLanDoc = 0;
            NgayDang = DateTime.Now;
            IsDraft = false;
            IsPublic = false;
        }
        /// <summary>
        /// Khởi gán giá trị cho các biến từ đối tượng truyền vào
        /// </summary>
        /// <param name="a">Truyền tham số của Đối tượng</param>
        public ShopABC_ChiTietBaiViet(ShopABC_ChiTietBaiViet a)
        {
            MaBV = a.MaBV;
            TieuDe = a.TieuDe;
            NoiDung = a.NoiDung;
            MaNV = a.MaNV;
            NgayDang = a.NgayDang;
            HinhBV = a.HinhBV;
            Duyet = a.Duyet;
            SoLanDoc = a.SoLanDoc;
            IsDraft = a.IsDraft;
            IsPublic = a.IsPublic;
        }
    }
}
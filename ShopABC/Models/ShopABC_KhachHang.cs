using ShopABC_DB;
namespace ShopABC.Models
{
    public static class ShopABC_KhachHang
    {
        // Raw Query
        /// <summary>
        /// Lấy danh sách khách hàng có trên CSDL
        /// </summary>
        /// <returns>Danh sách khách hàng ở dạng List</returns>
        public static List<Khachhang> get_KhachHang()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Khachhangs.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Khachhang>().ToList();
        }
        //Custom Query
        /// <summary>
        /// Lấy danh sách nhận xét của khách hàng có trên CSDL
        /// </summary>
        /// <returns>Danh sách nhận xét của KH ở dạng List</returns>
        public static List<KhNhanxet> get_kh_NhanXet_SP()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().KhNhanxets.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<KhNhanxet>().ToList();
        }
        /// <summary>
        /// Lấy tên của khách hàng
        /// </summary>
        /// <param name="makh">Truyền tham số Mã khách hàng</param>
        /// <returns>Tên khách hàng</returns>
        public static string get_TenKhachHang_Theo_MaKH(int makh)
        {
            try
            {
                return get_KhachHang().Where(x => x.Makh == makh).Select(x => x.Hodem + " " + x.Tenkh).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Đọc danh sách khách hàng thân thiết (mua hàng trên 10 lần)
        /// </summary>
        /// <returns>Danh sách khách hàng thân thiết ở dạng List</returns>
        public static IEnumerable<Khachhang> get_KhachHang_ThanThiet()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Khachhangs.Where(x => x.Donhangs.Count >= 10).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Khachhang>();
        }
        /// <summary>
        /// Lấy thông tin chi tiết của một khách hàng
        /// </summary>
        /// <param name="makh">Truyền tham số Mã khách hàng</param>
        /// <returns>Thông tin khách hàng</returns>
        public static Khachhang get_KhachHang_Theo_MaKH(int makh)
        {
            try
            {
                return get_KhachHang().FirstOrDefault(x => x.Makh == makh);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Đếm số lượt đánh giá từ khách hàng của một sản phẩm
        /// </summary>
        /// <param name="masp">Truyền tham số Mã sản phẩm</param>
        /// <returns>Số lượt đánh giá</returns>
        public static int get_Luot_DanhGiaSP(int masp)
        {
            try
            {
                return get_kh_NhanXet_SP().Count(x => x.Masp == masp);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// Lấy giá trị số sao đánh giá của khách hàng cho sản phẩm trên CSDL
        /// </summary>
        /// <param name="masp">Truyền tham số Mã sản phẩm</param>
        /// <returns></returns>
        public static byte? get_Sao_DanhGiaSP(int masp)
        {
            if (get_kh_NhanXet_SP().Where(x => x.Masp == masp).Select(x => x.Dgsao).FirstOrDefault() == null)
                return 5;
            return 0;
        }
        /// <summary>
        /// Lấy ngày mua gần nhất dựa vào Mã khách hàng
        /// </summary>
        /// <param name="makh">Mã khách hàng</param>
        /// <returns>Ngày mua gần nhất</returns>
        public static string get_LanMuaGanNhat(int? makh)
        {
            try
            {
                return ShopABC_DonHang.get_DonHang().Where(x => x.Makh == makh).OrderByDescending(x => x.Ngaymua).Select(x => x.Ngaymua.Value.ToString("dd/MM/yyyy")).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return string.Empty;
        }
        /// <summary>
        /// Lấy số lần mua dựa vào Mã khách hàng
        /// </summary>
        /// <param name="makh">Mã khách hàng</param>
        /// <returns>Số lần mua</returns>
        public static int get_SoLanMua(int? makh)
        {
            try
            {
                return ShopABC_DonHang.get_DonHang().Count(x => x.Makh == makh);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return 0;
        }

    }
}
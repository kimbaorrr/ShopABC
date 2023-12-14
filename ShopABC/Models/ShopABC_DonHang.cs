using ShopABC_DB;
namespace ShopABC.Models
{
    public class ShopABC_DonHang
    {
        public int SoHD { get; set; }
        public string ThongBaoLoi { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_DonHang()
        {
            SoHD = 0;
            ThongBaoLoi = string.Empty;
        }
        /// <summary>
        /// Khởi tạo tham số mới bằng cách truyền vào đối tượng
        /// </summary>
        /// <param name="a"></param>
        public ShopABC_DonHang(ShopABC_DonHang a)
        {
            SoHD = a.SoHD;
            ThongBaoLoi = a.ThongBaoLoi;
        }
        // Raw Query
        /// <summary>
        /// Lấy danh sách đơn hàng trên CSDL
        /// </summary>
        /// <returns>Danh sách đơn hàng ở dạng List</returns>
        public static List<Donhang> get_DonHang()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Donhangs.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Donhang>().ToList();
        }
        /// <summary>
        /// Lấy danh sách chi tiết đơn hàng trên CSDL
        /// </summary>
        /// <returns>Danh sách chi tiết đơn hàng</returns>
        public static List<Chitietdonhang> get_ChiTietDonHang()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Chitietdonhangs.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Chitietdonhang>().ToList();
        }
        // Custom Query
        /// <summary>
        /// Lấy danh sách đơn hàng theo trạng thái của đơn hàng 
        /// </summary>
        /// <param name="matt">Truyền tham số Mã trạng thái</param>
        /// <returns></returns>
        public static IEnumerable<Donhang> get_DonHang_Theo_MaTT(byte matt)
        {
            try
            {
                return get_DonHang().Where(x => x.MattNavigation.Matt == matt).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Donhang>();
        }
        /// <summary>
        /// Hiển thị thông tin của một Đơn hàng
        /// </summary>
        /// <param name="sohd">Truyền tham số Số hóa đơn</param>
        /// <returns>Thông tin đơn hàng</returns>
        public static Donhang get_CTDonHang_Theo_SoHD(int sohd)
        {
            try
            {
                return get_DonHang().FirstOrDefault(x => x.Sohd == sohd);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Lấy danh sách sản phẩm đã đặt mua theo Số HD
        /// </summary>
        /// <param name="sohd">Số hóa đơn</param>
        /// <returns>Danh sách sản phẩm đã mua</returns>
        public static IEnumerable<Chitietdonhang> get_DSSP_CTDonHang_Theo_SoHD(int sohd)
        {
            try
            {
                return get_ChiTietDonHang().Where(x => x.Sohd == sohd).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Chitietdonhang>();
        }
    }
}
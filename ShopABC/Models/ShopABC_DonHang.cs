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
            this.SoHD = 0;
            this.ThongBaoLoi = string.Empty;
        }
        /// <summary>
        /// Khởi tạo tham số mới bằng cách truyền vào đối tượng
        /// </summary>
        /// <param name="a"></param>
        public ShopABC_DonHang(ShopABC_DonHang a)
        {
            this.SoHD = a.SoHD;
            this.ThongBaoLoi = a.ThongBaoLoi;
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
        /// Chuyển đổi trạng thái từ "Chờ giao" => "Đang giao" bằng cách thay đổi giá trị cột "MaTT" trên CSDL
        /// </summary>
        /// <param name="sohd">Truyền tham số Số hóa đơn</param>
        /// <param name="hd">Truyền tham số Hành động</param>
        /// <returns>Thông báo trạng thái của đơn hàng</returns>
        public void xacnhan_GiaoHang(string hd)
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        Donhang a = e.Donhangs.Where(x => x.Sohd == this.SoHD).FirstOrDefault();
                        switch (hd)
                        {
                            case "xacnhan":
                                a.Matt = 2;
                                a.Ngaygiao = DateTime.Now;
                                e.SaveChanges();
                                e.Database.CurrentTransaction.Commit();
                                this.ThongBaoLoi = "Đã xác nhận đơn hàng " + a.Sohd + " !\nĐơn hàng sẽ chuyển sang trạng thái Giao hàng !";
                                break;
                            case "huydon":
                                a.Matt = 1;
                                a.Ngayhuy = DateTime.Now;
                                e.SaveChanges();
                                e.Database.CurrentTransaction.Commit();
                                this.ThongBaoLoi = "Đã hủy đơn hàng " + a.Sohd + " !";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ShopABC_CSDL.log_errs(ex.Message);
                        e.Database.CurrentTransaction.Rollback();
                    }
                }
            }
        }
        /// <summary>
        /// Chuyển đổi trạng thái từ "Đang giao" => "Đã giao" bằng cách thay đổi giá trị cột "MaTT" trên CSDL
        /// </summary>
        /// <param name="sohd">Truyền tham số Số hóa đơn</param>
        /// <param name="hd">Truyền tham số Hành động</param>
        /// <param name="lydo">Truyền tham số Lý do giao không thành công</param>
        /// <returns>Thông báo trạng thái của đơn hàng</returns>
        public void xacnhan_DaGiao(string hd)
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                using (e.Database.BeginTransaction())
                {
                    try
                    {
                        Donhang a = e.Donhangs.Where(x => x.Sohd == this.SoHD).FirstOrDefault();
                        switch (hd)
                        {
                            case "dagiaohang":
                                a.Matt = 6;
                                a.Ngaynhan = DateTime.Now;
                                e.SaveChanges();
                                e.Database.CurrentTransaction.Commit();
                                this.ThongBaoLoi = "Đơn hàng " + a.Sohd + " đã giao thành công !";
                                break;
                            case "khongthegiao":
                                a.Matt = 7;
                                a.Lydo = "Không xác định";
                                e.SaveChanges();
                                e.Database.CurrentTransaction.Commit();
                                this.ThongBaoLoi = "Xác nhận đơn hàng " + a.Sohd + " giao không thành công !\nLý do: " + a.Lydo;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ShopABC_CSDL.log_errs(ex.Message);
                        e.Database.CurrentTransaction.Rollback();
                    }
                }
            }
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
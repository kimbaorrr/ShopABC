using ShopABC_DB;
namespace ShopABC.Models
{
    public class ShopABC_GioHang
    {
        public SortedList<int, Chitietdonhang> SanPham_DaChon { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ShopABC_GioHang()
        {
            SanPham_DaChon = new SortedList<int, Chitietdonhang>();
        }
        /// <summary>
        /// Khởi gán giá trị cho biến từ Đối tượng truyền vào
        /// </summary>
        /// <param name="a">Truyền tham số Đối tượng</param>
        public ShopABC_GioHang(ShopABC_GioHang a)
        {
            SanPham_DaChon = a.SanPham_DaChon;
        }
        /// <summary>
        /// Kiểm tra xem giỏ hàng có rỗng không ?
        /// </summary>
        /// <returns>Rỗng/Không rỗng</returns>
        public bool kt_GioHang_Rong()
            => SanPham_DaChon.Keys.Count == 0;
        /// <summary>
        /// Thêm một sản phẩm vào giỏ hàng.
        /// Nếu đã tồn tại thì tăng Số lượng.
        /// </summary>
        /// <param name="masp">Truyền tham số Mã sản phẩm</param>
        /// <param name="kichco">Truyền tham số Kích cỡ</param>
        public void them_SanPham_GioHang(int masp, int soluong)
        {
            try
            {
                if (SanPham_DaChon.Keys.Contains(masp))
                {
                    Chitietdonhang x = SanPham_DaChon[masp];
                    x.Soluong += soluong;
                    x.Thanhtien = calc_GiaBan_Sau_GiamGia(masp) * x.Soluong;
                    SanPham_DaChon[masp] = x;
                }
                else
                {
                    Chitietdonhang c = new Chitietdonhang();
                    c.Masp = masp;
                    c.Soluong = soluong;
                    c.Thanhtien = calc_GiaBan_Sau_GiamGia(masp);
                    SanPham_DaChon.Add(masp, c);
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
        }
        /// <summary>
        /// Xóa một sản phẩm đã có trong giỏ hàng
        /// </summary>
        /// <param name="masp">Truyền tham số Mã sản phẩm</param>
        public void xoa_SanPham_GioHang(int masp)
        {
            try
            {
                if (SanPham_DaChon.Keys.Contains(masp))
                    SanPham_DaChon.Remove(masp);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
        }
        /// <summary>
        /// Xóa tất cả sản phẩm khỏi giỏ hàng
        /// </summary>
        public void xoa_TatCaSanPham_GioHang()
        {
            try
            {
                if (!kt_GioHang_Rong())
                    SanPham_DaChon.Clear();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
        }
        /// <summary>
        /// Giảm số lượng của một sản phẩm
        /// </summary>
        /// <param name="masp">Truyền tham số Mã sản phẩm</param>
        public void giam_SoLuong(int masp)
        {
            try
            {
                if (SanPham_DaChon.Keys.Contains(masp))
                {
                    Chitietdonhang a = SanPham_DaChon[masp];
                    if (a.Soluong > 1)
                    {
                        a.Soluong--;
                        a.Thanhtien = calc_GiaBan_Sau_GiamGia(masp) * a.Soluong;
                        SanPham_DaChon[masp] = a;
                    }
                    else
                        xoa_SanPham_GioHang(masp);
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
        }
        /// <summary>
        /// Tính giá bán sau khi giảm
        /// </summary>
        /// <param name="a">a[0]: Giá bán | a[1]: Giảm giá</param>
        /// <returns>Thành tiền</returns>
        public static int? calc_GiaBan_Sau_GiamGia(int masp)
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Sanphams.Where(x => x.Masp == masp).Select(x => x.Giaban * (100 - x.Thuevat) / 100 * (100 - x.Giamgia) / 100).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// Tính tổng tiền cần phải thanh toán cho tất cả sản phẩm có trong giỏ hàng
        /// </summary>
        /// <returns>Tổng tiền của tất cả sản phẩm</returns>
        public int calc_TongThanhToan()
        {
            int? thanhTien = 0;
            if (!kt_GioHang_Rong())
            {
                foreach (Chitietdonhang i in SanPham_DaChon.Values)
                {
                    thanhTien += i.Thanhtien;
                }
                return (int)thanhTien;
            }
            return 0;
        }
    }
}
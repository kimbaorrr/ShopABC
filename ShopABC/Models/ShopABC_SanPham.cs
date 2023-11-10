using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopABC_DB;
namespace ShopABC.Models
{
    public static class ShopABC_SanPham
    {
        /// <summary>
        /// Lấy danh sách sản phẩm từ CSDL
        /// </summary>
        /// <returns>Danh sách sản phẩm ở dạng List</returns>
        public static List<Sanpham> get_SanPham()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Sanphams.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Sanpham>().ToList();
        }
        /// <summary>
        /// Lấy danh sách danh mục sản phẩm từ CSDL
        /// </summary>
        /// <returns>Danh sách danh mục sản phẩm ở dạng List</returns>
        public static List<Danhmucsp> get_DanhMucSP()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Danhmucsps.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Danhmucsp>().ToList();
        }
        /// <summary>
        /// Lấy danh sách loại sản phẩm từ CSDL
        /// </summary>
        /// <returns>Danh sách loại sản phẩm ở dạng List</returns>
        public static List<Loaisp> get_LoaiSP()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Loaisps.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Loaisp>().ToList();
        }
        /// <summary>
        /// Lấy danh sách hãng sản xuất từ CSDL
        /// </summary>
        /// <returns>Danh sách hãng sản xuất ở dạng List</returns>
        public static List<Hangsx> get_HangSX()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Hangsxes.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Hangsx>().ToList();
        }
        /// <summary>
        /// Lấy danh sách màu sắc từ CSDL
        /// </summary>
        /// <returns>Danh sách màu sắc ở dạng List</returns>
        public static List<Mausac> get_MauSac()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Mausacs.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Mausac>().ToList();
        }
        // Custom Query
        /// <summary>
        /// Lấy ra danh mục sản phẩm cụ thể theo Mã Loại
        /// </summary>
        /// <param name="maloai">Truyền tham số Mã loại</param>
        /// <returns>Danh mục sản phẩm theo Mã loại</returns>
        public static IEnumerable<Danhmucsp> get_DanhMuc_Theo_MaLoai(int maloai)
        {
            try
            {
                return get_DanhMucSP().Where(x => x.Maloai == maloai).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Danhmucsp>();
        }
        /// <summary>
        /// Lấy ra thông tin sản phẩm cụ thể theo Mã Sản phẩm
        /// </summary>
        /// <param name="masp">Truyền tham số Mã sản phẩm</param>
        /// <returns>Thông tin sản phẩm</returns>
        public static Sanpham get_SanPham_Theo_MaSP(int masp)
        {
            try
            {
                return get_SanPham().FirstOrDefault(x => x.Masp == masp);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Đếm lượt mua sản phẩm từ danh sách đơn hàng (chỉ các đơn đã được "Giao thành công")
        /// </summary>
        /// <returns>Số lượt mua sản phẩm</returns>
        public static int get_LuotMuaSP()
        {
            try
            {
                return ShopABC_DonHang.get_DonHang().Count(x => x.Matt == 6);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return -1;
        }
        /// <summary>
        /// Đếm tổng sản phẩm từ danh sách sản phẩm (chỉ các sản phẩm đã duyệt)
        /// </summary>
        /// <returns>Tổng số sản phẩm</returns>
        public static int get_TongSP()
        {
            try
            {
                return get_SanPham().Count;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return -1;
        }
        /// <summary>
        /// Lấy ra màu sắc của sản phẩm theo Mã sản phẩm
        /// </summary>
        /// <param name="masp">Truyền tham số Mã sản phẩm</param>
        /// <returns>Danh sách màu sắc thuộc sản phẩm</returns>
        public static IEnumerable<Mausac> get_MauSac_Theo_MaSP(int masp)
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Sanphams.Where(x => x.Masp == masp).Select(x => x.MamauNavigation).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Mausac>();
        }
        /// <summary>
        /// Lấy ra danh sách sản phẩm yêu thích từ Store Procedure "sp_ds_SanPham_public" trên CSDL
        /// </summary>
        /// <returns>Danh sách sản phẩm yêu thích</returns>
        public static IEnumerable<Sanpham> get_SanPham_MayBeLike()
        {
            try
            {
                return get_SanPham().Take(4).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Sanpham>();
        }
        /// <summary>
        /// In ra doanh thu theo tuần
        /// </summary>
        /// <returns>JSON</returns>
        public static string get_DoanhThu_Theo_Tuan()
        {
            try
            {
                return JsonConvert.SerializeObject(ShopABC_CSDL.ketNoi().Doanhthus.FromSql($"SELECT * FROM fn_get_doanhthu_theo_tuan()").ToList());
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Lấy ra danh sách sản phẩm giảm giá sốc từ Store Procedure "sp_ds_SanPham_public" trên CSDL
        /// </summary>
        /// <returns>Danh sách sản phẩm giảm giá sốc</returns>
        public static IEnumerable<Sanpham> get_SanPham_GiamGiaSoc()
        {
            try
            {
                return get_SanPham().OrderByDescending(x => x.Giamgia).Take(4).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Sanpham>();
        }
        /// <summary>
        /// In ra 4 sản phẩm mới nhập trong vòng 2 ngày trở lại đây
        /// </summary>
        /// <returns>Danh sách sản phẩm mới nhập</returns>
        public static IEnumerable<Sanpham> get_SanPham_MoiNhap()
        {
            try
            {
                return get_SanPham().OrderByDescending(x => x.Ngaynhap).Take(4).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Sanpham>();
        }
        /// <summary>
        /// Lấy ra danh sách sản phẩm chưa duyệt có cột "Duyet" = 0 trên CSDL
        /// </summary>
        /// <returns>Danh sách sản phẩm chưa duyệt</returns>
        public static IEnumerable<Sanpham> get_SanPham_ChuaDuyet()
        {
            try
            {
                return get_SanPham().Where(x => x.Duyet == false).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Sanpham>();
        }
        /// <summary>
        /// Thực hiện lọc sản phẩm ứng với từng tham số truyền 
        /// </summary>
        /// <param name="hsx">Truyền tham số Hãng sản xuất</param>
        /// <param name="loai">Truyền tham số Loại sản phẩm</param>
        /// <param name="giaban">Truyền tham số Giá bán</param>
        /// <param name="search">Truyền tham số Tìm kiếm</param>
        /// <returns>Danh sách sản phẩm đã được lọc</returns>
        public static IEnumerable<Sanpham> loc_SanPham(string loai, string hsx, string search, string giaban, string mausac)
        {
            try
            {
                IEnumerable<Sanpham> ds = get_SanPham();
                if (loai != null)
                    ds = ds.Where(x => x.MadmNavigation.MaloaiNavigation.Tenloai.Equals(loai));
                if (hsx != null)
                    ds = ds.Where(x => x.MahsxNavigation.Tenhsx.Equals(hsx));
                if (search != null)
                    ds = ds.Where(x => x.Tensp.Contains(search.ToLower()));
                if (mausac != null)
                    ds = ds.Where(x => x.MamauNavigation.Tenmau.Equals(mausac));
                switch (giaban)
                {
                    case "100000-500000":
                        ds = ds.Where(x => ShopABC_GioHang.calc_GiaBan_Sau_GiamGia(x.Masp) >= 100000 && ShopABC_GioHang.calc_GiaBan_Sau_GiamGia(x.Masp) <= 500000);
                        break;
                    case "500000-1000000":
                        ds = ds.Where(x => ShopABC_GioHang.calc_GiaBan_Sau_GiamGia(x.Masp) >= 500000 && ShopABC_GioHang.calc_GiaBan_Sau_GiamGia(x.Masp) <= 1000000);
                        break;
                    case "gt1000000":
                        ds = ds.Where(x => ShopABC_GioHang.calc_GiaBan_Sau_GiamGia(x.Masp) > 1000000);
                        break;
                    default:
                        break;
                }
                return ds.AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Sanpham>();
        }
        /// <summary>
        /// Lấy tên sản phẩm theo mã sản phẩm
        /// </summary>
        /// <param name="masp">Mã sản phẩm</param>
        /// <returns>Tên sản phẩm</returns>
        public static string get_TenSP_Theo_MaSP(int masp)
        {
            try
            {
                return get_SanPham().Where(x => x.Masp == masp).Select(x => x.Tensp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
    }
}
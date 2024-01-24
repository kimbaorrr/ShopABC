using ShopABC_DB;
namespace ShopABC.Models
{
    public class ShopABC_BaiViet
    {
        // Raw Query 
        /// <summary>
        /// Lấy danh sách bài viết từ CSDL
        /// </summary>
        /// <returns>Danh sách bài viết ở dạng List</returns>
        public static List<Baiviet> get_BaiViet()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Baiviets.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Baiviet>().ToList();
        }
        // Custom Query
        /// <summary>
        /// Lấy thông tin bài viết cụ thể bằng Mã bài viết
        /// </summary>
        /// <param name="mabv">Truyền tham số Mã bài viết</param>
        /// <returns></returns>
        public static Baiviet get_BaiViet_Theo_ID(int mabv)
        {
            try
            {
                return get_BaiViet().FirstOrDefault(x => x.Mabv == mabv);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Đếm số bài viết (kể cả chưa duyệt) có trên CSDL
        /// </summary>
        /// <returns>Tổng số bài viết</returns>
        public static int get_TongBaiViet()
        {
            try
            {
                // Đếm số bài viết trong bảng Bài viết
                return get_BaiViet().Count;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// Đếm tổng số bài viết của từng Nhân viên (Người đăng)
        /// </summary>
        /// <param name="manv">Truyền tham số Mã nhân viên</param>
        /// <returns>Tổng số bài viết</returns>
        public static int get_TongBaiViet_Theo_ID(int manv)
        {
            try
            {
                return get_BaiViet().Count(x => x.Manv == manv);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// Lấy danh sách bài viết của một Nhân viên 
        /// </summary>
        /// <param name="manv">Truyền tham số Mã nhân viên</param>
        /// <returns>Danh sách bài viết ở dạng List</returns>
        public static IEnumerable<Baiviet> get_BaiViet_Theo_MaNV(int manv)
        {
            try
            {
                return get_BaiViet().Where(x => x.Manv == manv).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Baiviet>();
        }
        /// <summary>
        /// Lấy danh sách bài viết chưa duyệt trên CSDL (Duyet = false)
        /// </summary>
        /// <returns>Danh sách bài viết ở dạng List</returns>
        public static IEnumerable<Baiviet> get_BaiViet_ChuaDuyet()
        {
            try
            {
                return get_BaiViet().Where(x => x.Duyet == false && x.Draft == false).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Baiviet>();
        }
        /// <summary>
        /// Lấy danh sách bài viết đã duyệt (Duyet = true)
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Baiviet> get_BaiViet_DaDuyet()
        {
            try
            {
                return get_BaiViet().Where(x => x.Duyet == true && x.Draft == false).AsEnumerable();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Baiviet>();
        }
        /// <summary>
        /// Lấy tất cả bài viết (không bao gồm Nháp)
        /// </summary>
        /// <returns>Tất cả bài viết</returns>
        public static IEnumerable<Baiviet> get_BaiViet_Ko_Nhap()
        {
            try
            {
                return get_BaiViet().Where(x => x.Draft == false);
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Baiviet>();
        }
        /// <summary>
        /// Cập nhật số lần đọc (lượt xem) của bài viết
        /// </summary>
        /// <param name="bvid">Mã bài viết</param>
        public static void capNhat_SoLanDoc(int bvid)
        {
            using (ShopABC_Entities e = ShopABC_CSDL.ketNoi())
            {
                try
                {
                    Baiviet n = e.Baiviets.FirstOrDefault(x => x.Mabv == bvid);
                    n.Luotxem++;
                    e.SaveChanges();
                }
                catch (Exception ex)
                {
                    ShopABC_CSDL.log_errs(ex.Message);
                }
            }
        }
    }
}
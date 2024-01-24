using ShopABC_DB;
namespace ShopABC.Models
{
    public static class ShopABC_NhanVien
    {
        // Raw Query
        /// <summary>
        /// Lấy danh sách nhân viên có trên CSDL
        /// </summary>
        /// <returns>Danh sách nhân viên ở dạng List</returns>
        public static List<Nhanvien> get_NhanVien()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Nhanviens.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Nhanvien>().ToList();
        }
        /// <summary>
        /// Lấy danh sách thông tin đăng nhập có trên CSDL
        /// </summary>
        /// <returns>Danh sách đăng nhập ở dạng List</returns>
        public static List<NvDangnhap> get_NV_DangNhap()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().NvDangnhaps.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<NvDangnhap>().ToList();
        }
        /// <summary>
        /// Lấy danh sách phân quyền chức năng theo chức vụ nhân viên có trên CSDL
        /// </summary>
        /// <returns>Danh sách phân quyền ở dạng List</returns>
        public static List<Phanquyen> get_PhanQuyen()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Phanquyens.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Phanquyen>().ToList();
        }
        /// <summary>
        /// Lấy nhật ký truy cập từ CSDL
        /// </summary>
        /// <returns>Nhật ký truy cập ở dạng List</returns>
        public static List<History> get_NhatKyTruyCap()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Histories.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<History>().ToList();
        }
        // Custom Query
        /// <summary>
        /// Lấy Mã nhân viên dựa vào Tên đăng nhập
        /// </summary>
        /// <param name="tendn">Truyền tham số Tên đăng nhập</param>
        /// <returns>Mã nhân viên</returns>
        public static int get_MaNV(string tendn)
        {
            try
            {
                return get_NV_DangNhap().Where(x => x.Tendn.Equals(tendn)).Select(x => x.Manv).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return -1;
        }
        /// <summary>
        /// Lấy Tên nhân viên dựa vào Mã nhân viên
        /// </summary>
        /// <param name="manv">Truyền tham số Mã nhân viên</param>
        /// <returns>Tên nhân viên</returns>
        public static string get_Ten(int manv)
        {
            try
            {
                return get_NhanVien().Where(x => x.Manv == manv).Select(x => x.Tennv).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Lấy Họ tên nhân viên dựa vào Mã nhân viên
        /// </summary>
        /// <param name="manv">Truyền tham số Mã nhân viên</param>
        /// <returns>Họ tên nhân viên</returns>
        public static string get_HoTen(int manv)
        {
            try
            {
                return get_NhanVien().Where(x => x.Manv == manv).Select(x => x.Hodem + " " + x.Tennv).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Lấy ảnh nhân viên dựa vào Mã nhân viên
        /// </summary>
        /// <param name="manv">Truyền tham số Mã nhân viên</param>
        /// <returns>Liên kết ảnh nhân viên</returns>
        public static string get_HinhAnh(int manv)
        {
            try
            {
                return get_NhanVien().Where(x => x.Manv == manv).Select(x => x.Hinhnv).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Lấy danh sách nhân viên cho tag Select
        /// </summary>
        /// <returns>Danh sách nhân viên ở dạng List</returns>
        public static IEnumerable<Nhanvien> get_NhanVien_SelectList()
        {
            return get_NhanVien().Select(x => new Nhanvien { Hodem = string.Format("{0} {1}", x.Hodem, x.Tennv) }).AsEnumerable();
        }
        public static Phanquyen get_PhanQuyen_NhanVien(int? manv)
        {
            try
            {
                return get_NhanVien().Where(x => x.Manv == manv).Select(x => x.MacvNavigation.Phanquyen).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
    }
}
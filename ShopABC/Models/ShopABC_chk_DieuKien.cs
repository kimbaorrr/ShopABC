namespace ShopABC.Models
{
    public static class ShopABC_chk_DieuKien
    {
        /// <summary>
        /// Kiểm tra tên sản phẩm đã tồn tại trên CSDL hay chưa ?
        /// </summary>
        /// <param name="tensp">Tên sản phẩm</param>
        /// <returns>Có/Không</returns>
        public static bool chk_SanPham_TonTai(string tensp)
        {
            try
            {
                if (ShopABC_SanPham.get_SanPham().FirstOrDefault(x => x.Tensp.Equals(tensp)) != null)
                    return true;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra danh mục có khớp với loại sản phẩm đã chọn ?
        /// </summary>
        /// <param name="maloai">Truyền tham số Mã loại</param>
        /// <param name="madm">Truyền tham số Mã danh mục</param>
        /// <returns>Đúng/Sai</returns>
        public static bool chk_DanhMuc_TonTai_LoaiSP(int maloai, int madm)
        {
            try
            {
                if (ShopABC_SanPham.get_DanhMucSP().FirstOrDefault(x => x.Madm == madm && x.Maloai == maloai) != null)
                    return true;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra sản phẩm đã có đơn hàng hay chưa ?
        /// </summary>
        /// <param name="masp">Truyền tham số Mã sản phẩm</param>
        /// <returns></returns>
        public static bool chk_SanPham_TonTai_DonHang(int masp)
        {
            try
            {
                if (ShopABC_DonHang.get_ChiTietDonHang().FirstOrDefault(x => x.Masp == masp) != null)
                    return true;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra khách hàng đã có thông tin trên CSDL hay chưa ? 
        /// </summary>
        /// <param name="hodem">Truyền tham số Họ đệm</param>
        /// <param name="tenkh">Truyền tham số Tên khách hàng</param>
        /// <returns>Có/Không</returns>
        public static bool chk_KhachHang_TonTai(string hodem, string tenkh, string sdt)
        {
            try
            {
                if (ShopABC_KhachHang.get_KhachHang().FirstOrDefault(x => x.Hodem.Equals(hodem) && x.Tenkh.Equals(tenkh) && x.Sdt.Equals(sdt)) != null)
                    return true;
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return false;
        }
    }
}
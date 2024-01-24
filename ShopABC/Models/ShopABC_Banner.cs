using ShopABC_DB;
namespace ShopABC.Models
{
    public static class ShopABC_Banner
    {
        // Raw Query
        /// <summary>
        /// Lấy danh sách bài viết của Banner trên trang chủ (Trang công khai)
        /// </summary>
        /// <returns>Danh sách Banner</returns>
        public static List<BannerTrangchu> get_Banner()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().BannerTrangchus.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<BannerTrangchu>().ToList();
        }
    }
}
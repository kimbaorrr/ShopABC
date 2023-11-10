using ShopABC_DB;
namespace ShopABC.Models
{
    public static class ShopABC_ThongTinCty
    {
        // Raw Query
        /// <summary>
        /// Lấy danh sách thông tin công ty
        /// </summary>
        /// <returns>Thông tin công ty</returns>
        public static List<ThongtinCty> get_ThongTinCty()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().ThongtinCties.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<ThongtinCty>().ToList();
        }
        /// <summary>
        /// Lấy danh sách FAQs
        /// </summary>
        /// <returns>FAQs</returns>
        public static List<Faq> get_FAQs()
        {
            try
            {
                return ShopABC_CSDL.ketNoi().Faqs.ToList();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Enumerable.Empty<Faq>().ToList();
        }
        // Custom Query
        public static ThongtinCty get_ThongTin_Theo_TieuDe(string tieude)
        {
            try
            {
                return get_ThongTinCty().FirstOrDefault(x => x.Tieude.Equals(tieude));
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return null;
        }
    }
}

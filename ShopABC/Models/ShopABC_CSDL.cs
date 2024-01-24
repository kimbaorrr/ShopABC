using ShopABC_DB;
namespace ShopABC.Models
{
    public static class ShopABC_CSDL
    {
        /// <summary>
        /// Khởi tạo kết nối tới CSDL
        /// </summary>
        /// <returns></returns>
        public static ShopABC_Entities ketNoi()
        {
            try
            {
                ShopABC_Entities e = new ShopABC_Entities();
                return e;
            }
            catch (Exception ex)
            {
                log_errs(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Ghi nhận các lỗi vào tệp ShopABC_Errors.txt tại ổ đĩa D:\
        /// </summary>
        /// <param name="message">Truyền tham số Thông tin lỗi</param>
        public static void log_errs(string message)
        {
            File.AppendAllText("ShopABC_Errors.txt", "\n" + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") + "\t" + message);
        }
    }
}
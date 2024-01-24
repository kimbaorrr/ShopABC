using Newtonsoft.Json;
namespace ShopABC.Models
{
    public static class ShopABC_Tools
    {
        /// <summary>
        /// Gán đối tượng cho Session bằng cách Convert sang JSON
        /// </summary>
        /// <param name="session">Phiên hiện tại</param>
        /// <param name="key">Khóa</param>
        /// <param name="value">Giá trị</param>
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// Get đối tượng từ Session theo Khóa
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của đối tượng</typeparam>
        /// <param name="session">Phiên hiện tại</param>
        /// <param name="key">Khóa</param>
        /// <returns>Đối tượng ---</returns>
        public static T GetObject<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value);
        }
        /// <summary>
        /// Lấy hình ảnh thông qua CDN
        /// </summary>
        /// <param name="hinh">Tên hình</param>
        /// <param name="chon">Tùy chọn
        /// 0- For No-Image
        /// 1- For Bài viết
        /// 2- For Sản phẩm
        /// </param>
        /// <returns></returns>
        public static string get_Images_over_CDN(string hinh, byte chon)
        {
            string path = "/uploads/images/";
            switch (chon)
            {
                case 1: // Link of Bài viết
                    return path + "Blog/" + hinh;
                case 2: // Link of Sản phẩm
                    return path + "SanPham/" + hinh;
                case 0: // Link of No-Image
                    return path + hinh;
                case 3:
                    return path + "Banner/" + hinh;
                default:
                    break;
            }
            return string.Empty;
        }
        public static void del_Image(string image_path)
        {
            File.Delete($@"wwwroot\\uploads\\images\\{image_path}");
        }
    }
}

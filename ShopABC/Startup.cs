using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace ShopABC
{
    public class Startup
    {
        public IConfiguration ConfigRoot { get; }
        public WebApplication UngDungWeb { get; set; }
        public IServiceCollection DichVuWeb { get; set; }
        public Startup(IConfiguration configuration)
        { ConfigRoot = configuration; }
        /// <summary>
        /// Cấu hình khởi chạy các dịch vụ cần thiết
        /// </summary>
        public void cauHinh_DichVu()
        {
            this.DichVuWeb.AddDistributedMemoryCache();
            this.DichVuWeb.AddSession(options =>
            {
                options.Cookie.Name = "Male_Fashion";
                options.Cookie.MaxAge = TimeSpan.FromMinutes(120);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            this.DichVuWeb.AddRazorPages();
            this.DichVuWeb.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.RequireHeaderSymmetry = false;
                options.ForwardLimit = null;
                options.KnownProxies.Add(IPAddress.Loopback);
            });
        }
        /// <summary>
        /// Cấu hình cho ứng dụng Web
        /// </summary>
        public void cauHinh_Chung()
        {

            this.UngDungWeb.UseStaticFiles();
            this.UngDungWeb.UseRouting();
            this.UngDungWeb.UseAuthorization();
            this.UngDungWeb.UseSession();
            this.UngDungWeb.MapDefaultControllerRoute();
            this.UngDungWeb.Run();
        }
    }
}

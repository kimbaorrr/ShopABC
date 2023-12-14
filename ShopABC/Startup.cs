using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ShopABC_DB;
using Microsoft.Extensions.Options;

namespace ShopABC
{
    public class Startup
    {
        public IConfiguration ConfigRoot { get; }
        public WebApplication app { get; set; }
        public IServiceCollection services { get; set; }
        public Startup(IConfiguration configuration)
        { ConfigRoot = configuration; }
        /// <summary>
        /// Cấu hình khởi chạy các dịch vụ cần thiết
        /// </summary>
        public void cauHinh_DichVu()
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "Male_Fashion";
                options.Cookie.MaxAge = TimeSpan.FromMinutes(120);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services.AddRazorPages();
            services.Configure<ForwardedHeadersOptions>(options =>
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

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.MapDefaultControllerRoute();
            app.Run();
        }
    }
}

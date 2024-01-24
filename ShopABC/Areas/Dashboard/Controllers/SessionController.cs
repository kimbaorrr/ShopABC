using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopABC.Models;

namespace ShopABC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SessionController : Controller
    {
        // GET: Dashboard/Session
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession my_sess = HttpContext.Session;
            if (my_sess.GetString("tendn") == null || my_sess.GetInt32("manv") == null || my_sess.GetString("pkey") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index", area = "Dashboard" }));
            }
        }
        public void set_ThongBao(string thongbao, byte matt)
        {
            ViewBag.ThongBao = thongbao;
            switch (matt)
            {
                case 0:
                    ViewBag.TrangThai = "success";
                    break;
                case 1:
                    ViewBag.TrangThai = "warning";
                    break;
                case 2:
                    ViewBag.TrangThai = "danger";
                    break;
                default:
                    break;
            }
        }
        public void log_History(string message)
        {
            if (string.IsNullOrEmpty(get_Connection().RemoteIpAddress.ToString()))
                ShopABC_TaiKhoan._History(get_Connection().RemoteIpAddress.ToString(), get_MaNV_Session(), message, HttpContext.Request.Headers["User-Agent"]);
            else
                ShopABC_TaiKhoan._History(get_Connection().LocalIpAddress.ToString(), get_MaNV_Session(), message, HttpContext.Request.Headers["User-Agent"]);
        }
        public ISession get_Session()
            => HttpContext.Session;
        public ConnectionInfo get_Connection()
            => HttpContext.Connection;
        public int get_MaNV_Session()
            => (int)get_Session().GetInt32("manv");
        public string get_pkey_Session()
            => get_Session().GetString("pkey");
        public string get_tendn_Session()
            => get_Session().GetString("tendn");
        public string get_IP_Addr()
            => HttpContext.Connection.RemoteIpAddress.ToString();
        public string get_User_Agent()
            => Request.Headers["User-Agent"].ToString();
    }
}
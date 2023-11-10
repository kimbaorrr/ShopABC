using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopABC.Models;
namespace ShopABC.Controllers
{
    public class SessionController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession my_sess = this.HttpContext.Session;
            if (my_sess.GetString("GioHang") == null || my_sess.GetString("kh-pkey") == null || my_sess.GetInt32("SoLanDN") == null)
            {
                ShopABC_Tools.SetObject(my_sess, "GioHang", new ShopABC_GioHang());
                my_sess.SetString("kh-pkey", ShopABC_TaiKhoan.privateKey());
                my_sess.SetInt32("SoLanDN", 0);
            }
        }
        public ISession get_Session() => this.HttpContext.Session;
    }
}

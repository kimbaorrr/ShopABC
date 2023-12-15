using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopABC.Models;
namespace ShopABC.Controllers
{
    public class SessionController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ISession sess = HttpContext.Session;
            if (string.IsNullOrEmpty(sess.GetString("GioHang")) || string.IsNullOrEmpty(sess.GetString("kh-pkey")))
            {
                ShopABC_Tools.SetObject(sess, "GioHang", new ShopABC_GioHang());
                sess.SetString("kh-pkey", ShopABC_TaiKhoan.privateKey());
            }
        }
        public ISession get_Session() => HttpContext.Session;
    }
}

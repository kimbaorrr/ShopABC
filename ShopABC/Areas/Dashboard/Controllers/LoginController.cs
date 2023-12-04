using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;

namespace ShopABC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class LoginController : Controller
    {
        // GET: Dashboard/Login
        [HttpGet]
        [Route("admin/dang-nhap-he-thong")]
        public IActionResult Index()
            => View();
        [HttpGet]
        [Route("admin/quen-mat-khau")]
        public IActionResult QuenMatKhau()
            => View();
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(ShopABC_TaiKhoan a)
        {
            try
            {
                if (ModelState.IsValid && a.dangNhap())
                {
                    ISession my_sess = get_Session();
                    my_sess.SetString("pkey", ShopABC_TaiKhoan.privateKey());
                    my_sess.SetString("tendn", a.TenDN);
                    my_sess.SetInt32("manv", ShopABC_NhanVien.get_MaNV(a.TenDN));
                    return RedirectToAction("Index", "Home", new { Area = "Dashboard" });
                }
                ViewBag.ThongBao = "Thông tin đăng nhập không chính xác !";
                ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult QuenMatKhau(ShopABC_QuenMatKhau a)
            => View();
        /*
        private bool check_SoLanDN()
        {
            if (get_Session().GetInt32("SoLanDN") >= 5)
            {
                get_Session().SetObject("Retries_Time", DateTime.Now);
                return true;
            }
            return false;
        }
        
        private bool khoiPhuc_SoLanDN()
        {
            DateTime a = get_Session().GetObject<DateTime>("Retries_Time");
            if (DateTime.Now.Minute - a.Minute >= 15)
                return true;
            return false;
        }
        */
        public ISession get_Session() => HttpContext.Session;
    }
}
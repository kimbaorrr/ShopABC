using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;

namespace ShopABC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class LoginController : Controller
    {
        // GET: Dashboard/Login
        [HttpGet]
        [Route("admin/dang-nhap-he-thong/{date?}")]
        public IActionResult Index(string date)
        {
            try
            {
                if (date.Equals(DateTime.Today.Date.ToString("ddMMyyyy")))
                    /*
                    if (check_SoLanDN())
                    {
                        if (khoiPhuc_SoLanDN() == false)
                            ViewBag.ThongBao = "Vượt quá số lần đăng nhập !";
                    }
                    */
                    return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        [HttpGet]
        [Route("admin/quen-mat-khau")]
        public IActionResult QuenMatKhau()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(ShopABC_TaiKhoan a)
        {
            try
            {
                if (ModelState.IsValid && a.dangNhap())
                {
                    ISession my_sess = this.HttpContext.Session;
                    my_sess.SetString("pkey", ShopABC_TaiKhoan.privateKey());
                    my_sess.SetString("tendn", a.TenDN);
                    my_sess.SetInt32("manv", ShopABC_NhanVien.get_MaNV(a.TenDN));
                    return RedirectToAction("Index", "Home", new { Area = "Dashboard" });
                }
                ViewBag.ThongBao = "Thông tin đăng nhập không chính xác !";
                ModelState.Clear();
                get_Session().SetInt32("SoLanDN", (int)get_Session().GetInt32("SoLanDN") + 1);
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
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return View();
        }
        [HttpPost]
        [Route("admin/gui-ma-xac-minh")]
        public void GuiMaXacMinh(string Email)
        {
            new ShopABC_QuenMatKhau().gui_MaXacMinh(Email);
        }
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
        public ISession get_Session() => this.HttpContext.Session;
    }
}
using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;
namespace ShopABC.Controllers
{
    public class GioHangController : SessionController
    {
        // GET: GioHang
        [HttpGet]
        [Route("gio-hang")]
        public IActionResult Index()
        {
            ViewData["GioHangCuaToi"] = ShopABC_Tools.GetObject<ShopABC_GioHang>(get_Session(), "GioHang");
            return View();
        }
        [HttpGet]
        [Route("thanh-toan-truc-tuyen")]
        public IActionResult ThanhToan(string pkey, string ttid)
        {
            try
            {
                ShopABC_GioHang x = ShopABC_Tools.GetObject<ShopABC_GioHang>(get_Session(), "GioHang");
                if (!x.kt_GioHang_Rong() && get_Session().GetString("kh-pkey").Equals(pkey) && get_Session().GetString("tt_id").Equals(ttid))
                    ViewData["GioHangCuaToi"] = x;
                return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("404");
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Route("them-vao-gio")]
        public IActionResult ThemVaoGio(int spid, int soluong = 1, string returnURL = null)
        {
            try
            {
                ShopABC_GioHang a = ShopABC_Tools.GetObject<ShopABC_GioHang>(get_Session(), "GioHang");
                a.them_SanPham_GioHang(spid, soluong);
                ShopABC_Tools.SetObject(get_Session(), "GioHang", a);
                return Ok(Redirect(returnURL));
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult CapNhat_TongThanhToan()
        {
            ShopABC_GioHang a = ShopABC_Tools.GetObject<ShopABC_GioHang>(get_Session(), "GioHang");
            return Ok(a.calc_TongThanhToan());;
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Route("xoa-khoi-gio")]
        public IActionResult XoaKhoiGio(int spid)
        {

            try
            {
                ShopABC_GioHang a = ShopABC_Tools.GetObject<ShopABC_GioHang>(get_Session(), "GioHang");
                a.xoa_SanPham_GioHang(spid);
                ShopABC_Tools.SetObject(get_Session(), "GioHang", a);
                return Ok();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return NotFound();
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Route("xoa-tat-ca")]
        public IActionResult XoaTatCaSanPham()
        {
            try
            {
                ShopABC_GioHang a = ShopABC_Tools.GetObject<ShopABC_GioHang>(get_Session(), "GioHang");
                a.xoa_TatCaSanPham_GioHang();
                ShopABC_Tools.SetObject(get_Session(), "GioHang", a);
                return Ok();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return NotFound();
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Route("giam-so-luong")]
        public IActionResult GiamSoLuong(int spid)
        {
            try
            {
                ShopABC_GioHang a = ShopABC_Tools.GetObject<ShopABC_GioHang>(get_Session(), "GioHang");
                a.giam_SoLuong(spid);
                ShopABC_Tools.SetObject(get_Session(), "GioHang", a);
                return Ok();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("thanh-toan-thanh-cong")]
        public IActionResult ThanhToanThanhCong(string pkey, string ttid, string ngaygd, int t)
        {
            try
            {
                if (pkey.Equals(get_Session().GetString("kh-pkey")) && ttid.Equals(get_Session().GetString("tt_id")) && ngaygd.Equals(DateTime.Today.ToString("ddMMyyyy")) && t != -1)
                {
                    ViewBag.HoaDon = t;
                    get_Session().SetString("tt_id", string.Empty);
                    ShopABC_Tools.SetObject(get_Session(), "GioHang", new ShopABC_GioHang());
                    return View();
                }
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        // POST Methods
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult ThanhToan(ShopABC_ThanhToan a)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ShopABC_GioHang b = ShopABC_Tools.GetObject<ShopABC_GioHang>(get_Session(), "GioHang");
                    int sohd = a.tao_DonHang(b.SanPham_DaChon.Values, b.calc_TongThanhToan());
                    ModelState.Clear();
                    return RedirectToAction("ThanhToanThanhCong", "GioHang", new { pkey = get_Session().GetString("kh-pkey"), ttid = get_Session().GetString("tt_id"), ngaygd = DateTime.Today.ToString("ddMMyyyy"), t = sohd });
                }
                return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
    }
}

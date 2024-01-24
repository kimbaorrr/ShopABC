using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;
using X.PagedList;
namespace ShopABC.Controllers
{
    public class SanPhamController : SessionController
    {
        // GET: SanPham
        [HttpGet]
        [Route("danh-sach-san-pham/{page?}")]
        public IActionResult Index(int page = 1)
        {

            try
            {
                ThucThi(page: page);
                return View();
            }
            catch (Exception ex)
            {
                ShopABC_CSDL.log_errs(ex.Message);
            }
            return Redirect("~/404");
        }
        [HttpGet]
        [Route("san-pham/{spid?}")]
        public IActionResult ChiTiet(int spid)
        {

            ViewData["SanPhamCanXem"] = ShopABC_SanPham.get_SanPham_Theo_MaSP(spid);
            return View();
        }
        [HttpGet]
        [Route("danh-sach-san-pham/tim-kiem/{search?}/{page?}")]
        public IActionResult TimKiem(string search = null, int page = 1)
        {
            ThucThi(search: search, page: page);
            return View("Index");
        }
        [HttpGet]
        [Route("danh-sach-san-pham/theo-loai/{loai?}/{page?}")]
        public IActionResult LocSanPham_TheoLoai(string loai = null, int page = 1)
        {
            ThucThi(loai: loai, page: page);
            return View("Index");
        }
        [HttpGet]
        [Route("danh-sach-san-pham/theo-hsx/{hsx?}/{page?}")]
        public IActionResult LocSanPham_TheoHSX(string hsx = null, int page = 1)
        {
            ThucThi(hsx: hsx, page: page);
            return View("Index");
        }
        [HttpGet]
        [Route("danh-sach-san-pham/theo-gia-ban/{giaban?}/{page?}")]
        public IActionResult LocSanPham_TheoGiaBan(string giaban = null, int page = 1)
        {
            ThucThi(giaban: giaban, page: page);
            return View("Index");
        }
        [HttpGet]
        [Route("danh-sach-san-pham/theo-mau/{color?}/{page?}")]
        public IActionResult LocSanPham_TheoMau(string color = null, int page = 1)
        {
            ThucThi(mausac: color, page: page);
            return View("Index");
        }
        private void ThucThi(string loai = null, string hsx = null, string search = null, string giaban = null, int page = 1, string mausac = null)
        {

            // Giá trị: page => trang hiện tại | 6 => tổng số đối tượng trong 1 trang
            ViewData["list_SanPham"] = ShopABC_SanPham.loc_SanPham(loai: loai, hsx: hsx, search: search, giaban: giaban, mausac: mausac).ToPagedList(page, 9);
            ViewBag.Here = page;
            ViewBag.Redirect = ControllerContext.RouteData.Values["action"];
        }
    }
}
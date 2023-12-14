using Microsoft.AspNetCore.Mvc;
using ShopABC.Models;
using X.PagedList;
namespace ShopABC.Controllers
{
    public class BlogController : SessionController
    {
        // GET: Blog
        [HttpGet]
        [Route("blog/{page?}")]
        public IActionResult Index(int page = 1)
        {
            ViewData["list_blogs"] = ShopABC_BaiViet.get_BaiViet_DaDuyet().ToPagedList(page, 6);
            ViewBag.Here = page;
            ViewBag.Redirect = ControllerContext.RouteData.Values["action"];
            return View();
        }
        [HttpGet]
        [Route("blog/d/{bid?}")]
        public IActionResult Xem_Blog(int bid, int? p = 0)
        {

            if (p == 1)
                ViewBag.PhanHoi = "=> Chúng tôi đã ghi nhận phản hồi. Cám ơn bạn !";
            ViewData["ChiTiet_Blog"] = ShopABC_BaiViet.get_BaiViet_Theo_ID(bid);
            ShopABC_BaiViet.capNhat_SoLanDoc(bid);
            ViewBag.ThisBlog = bid;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Xem_Blog(ShopABC_ChiTietBaiViet_BinhLuan a)
        {
            int cache = 0;
            if (ModelState.IsValid)
            {
                a.them_BinhLuan();
                cache = a.MaBV;
            }
            return RedirectToAction("Xem_Blog", "Blog", new { bid = cache, p = 1 });
        }
    }
}
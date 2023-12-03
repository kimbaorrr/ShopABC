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
            // dinhTuyen_Public();
            // dinhTuyen_Private();
            this.UngDungWeb.Run();
        }
        /// <summary>
        /// Định tuyến cho trang Công khai
        /// </summary>
        /// <param name="app">Ứng dụng web</param>
        private void dinhTuyen_Public()
        {
            this.UngDungWeb.UseEndpoints(endpoints =>
            {

                // Định tuyến: SanPham
                endpoints.MapControllerRoute(
                    name: "r_sanpham",
                    pattern: "danh-sach-san-pham/{page?}",
                    defaults: new { controller = "SanPham", action = "Index" }
                );
                // Định tuyến: LocSanPham_TheoLoai
                endpoints.MapControllerRoute(
                    name: "r_sanpham_theo_loai",
                    pattern: "danh-sach-san-pham/theo-loai/{loai?}/{page?}",
                    defaults: new { controller = "SanPham", action = "LocSanPham_TheoLoai" }
                );
                // Định tuyến: LocSanPham_TheoHSX
                endpoints.MapControllerRoute(
                    name: "r_sanpham_theo_hsx",
                    pattern: "danh-sach-san-pham/theo-hsx/{hsx?}/{page?}",
                    defaults: new { controller = "SanPham", action = "LocSanPham_TheoHSX" }
                );
                // Định tuyến: LocSanPham_TheoGiaBan
                endpoints.MapControllerRoute(
                    name: "r_sanpham_theo_giaban",
                    pattern: "danh-sach-san-pham/theo-gia-ban/{giaban?}/{page?}",
                    defaults: new { controller = "SanPham", action = "LocSanPham_TheoGiaBan" }
                );
                // Định tuyến: LocSanPham_TheoMau
                endpoints.MapControllerRoute(
                    name: "r_sanpham_theo_mau",
                    pattern: "danh-sach-san-pham/theo-mau/{color?}/{page?}",
                    defaults: new { controller = "SanPham", action = "LocSanPham_TheoMau" }
                );
                // Định tuyến: TimKiem_SanPham
                endpoints.MapControllerRoute(
                    name: "r_sanpham_timkiem",
                    pattern: "danh-sach-san-pham/tim-kiem/{search?}/{page?}",
                    defaults: new { controller = "SanPham", action = "TimKiem" }
                );
                // Định tuyến: ChiTietSanPham
                endpoints.MapControllerRoute(
                    name: "r_chitietsanpham",
                    pattern: "san-pham/{p?}/{spid?}",
                    defaults: new { controller = "SanPham", action = "ChiTiet" }
                );
                // Định tuyến: VeChungToi
                endpoints.MapControllerRoute(
                    name: "r_vechungtoi",
                    pattern: "ve-chung-toi",
                    defaults: new { controller = "VeChungToi", action = "Index" }
                );
                // Định tuyến: LienHe
                endpoints.MapControllerRoute(
                    name: "r_lienhe",
                    pattern: "thong-tin-lien-he",
                    defaults: new { controller = "LienHe", action = "Index" }
                );
                // Định tuyến: FAQs
                endpoints.MapControllerRoute(
                    name: "r_faqs",
                    pattern: "cau-hoi-thuong-gap",
                    defaults: new { controller = "FAQs", action = "Index" }
                );
                // Định tuyến: ChinhSach_DoiHang
                endpoints.MapControllerRoute(
                    name: "r_chinhsach_doihang",
                    pattern: "chinh-sach-doi-hang",
                    defaults: new { controller = "ChinhSach", action = "DoiHang" }
                );
                // Định tuyến: ChinhSach_BaoHanh
                endpoints.MapControllerRoute(
                    name: "r_chinhsach_baohanh",
                    pattern: "chinh-sach-bao-hanh",
                    defaults: new { controller = "ChinhSach", action = "BaoHanh" }
                );
                // Định tuyến: ChinhSach_BaoMat
                endpoints.MapControllerRoute(
                    name: "r_chinhsach_baomat",
                    pattern: "chinh-sach-bao-mat",
                    defaults: new { controller = "ChinhSach", action = "BaoMat" }
                );
                // Định tuyến: ChinhSach_HoanTien
                endpoints.MapControllerRoute(
                    name: "r_chinhsach_hoantien",
                    pattern: "chinh-sach-hoan-tien",
                    defaults: new { controller = "ChinhSach", action = "HoanTien" }
                );
                // Định tuyến: ThongTin_TuyenDung
                endpoints.MapControllerRoute(
                    name: "r_thongtin_tuyendung",
                    pattern: "thong-tin-tuyen-dung",
                    defaults: new { controller = "ChinhSach", action = "TuyenDung" }
                );
                // Định tuyến cho Giỏ hàng
                // Định tuyến: GioHang_Home
                endpoints.MapControllerRoute(
                   name: "r_giohang_home",
                   pattern: "gio-hang",
                   defaults: new { controller = "GioHang", action = "Index" }
                );
                // Định tuyến: GioHang_ThanhToan
                endpoints.MapControllerRoute(
                   name: "r_giohang_thanhtoan",
                   pattern: "thanh-toan-truc-tuyen/{pkey?}/{ttid?}",
                   defaults: new { controller = "GioHang", action = "ThanhToan" }
                );
                // Định tuyến: GioHang_ThemVaoGio
                endpoints.MapControllerRoute(
                   name: "r_giohang_themvaogio",
                   pattern: "gio-hang/them-vao-gio/{spid?}/{pkey?}",
                   defaults: new { controller = "GioHang", action = "ThemVaoGio" }
                );
                // Định tuyến: GioHang_XoaKhoiGio
                endpoints.MapControllerRoute(
                   name: "r_giohang_xoakhoigio",
                   pattern: "gio-hang/xoa-khoi-gio/{spid?}/{pkey?}",
                   defaults: new { controller = "GioHang", action = "Index" }
                );
                // Định tuyến: GioHang_XoaTatCaSanPham
                endpoints.MapControllerRoute(
                   name: "r_giohang_xoatatca",
                   pattern: "gio-hang/xoa-tat-ca",
                   defaults: new { controller = "GioHang", action = "XoaTatCaSanPham" }
                );
                // Định tuyến: GioHang_GiamSoLuong
                endpoints.MapControllerRoute(
                   name: "r_giohang_giamsoluong",
                   pattern: "gio-hang/giam-so-luong/{spid?}/{pkey?}",
                   defaults: new { controller = "GioHang", action = "GiamSoLuong" }
                );
                // Định tuyến: GioHang_ThanhToanThanhCong
                endpoints.MapControllerRoute(
                   name: "r_giohang_thanhtoan_tc",
                   pattern: "thanh-toan-truc-tuyen/{ttid?}/{pkey?}/{ngaygd?}/{t?}/thanh-cong",
                   defaults: new { controller = "GioHang", action = "ThanhToanThanhCong" }
                );
                // Định tuyến: Mặc định
                endpoints.MapDefaultControllerRoute();
            });
        }
        /// <summary>
        /// Định tuyến cho trang Quản trị
        /// </summary>
        /// <param name="app">Ứng dụng web</param>
        private void dinhTuyen_Private()
        {
            this.UngDungWeb.UseEndpoints(endpoints =>
            {
                // Định tuyến: DoanhThu
                endpoints.MapAreaControllerRoute(
                    name: "r_doanhthu",
                    areaName: "Dashboard",
                    pattern: "admin/doanh-thu-theo-tuan",
                    defaults: new { controller = "Home", action = "DoanhThu" }
                );

                // Định tuyến: Login
                endpoints.MapAreaControllerRoute(
                    name: "r_dangnhap",
                    areaName: "Dashboard",
                    pattern: "admin/dang-nhap-he-thong/{date?}",
                    defaults: new { controller = "Login", action = "Index" }
                );

                // Định tuyến: Login-QuenMatKhau
                endpoints.MapAreaControllerRoute(
                    name: "r_quenmatkhau",
                    areaName: "Dashboard",
                    pattern: "admin/quen-mat-khau",
                    defaults: new { controller = "Login", action = "QuenMatKhau" }
                );

                // Định tuyến: Login-GuiMaXacMinh
                endpoints.MapAreaControllerRoute(
                    name: "r_quenmatkhau",
                    areaName: "Dashboard",
                    pattern: "admin/gui-ma-xac-minh",
                    defaults: new { controller = "Login", action = "GuiMaXacMinh" }
                );

                // Định tuyến: TaiKhoan-Logout
                endpoints.MapAreaControllerRoute(
                    name: "r_dangxuat",
                    areaName: "Dashboard",
                    pattern: "admin/dang-xuat",
                    defaults: new { controller = "TaiKhoan", action = "Logout" }
                );

                // Định tuyến: TaiKhoan-HoSoCaNhan
                endpoints.MapAreaControllerRoute(
                    name: "r_hosocanhan",
                    areaName: "Dashboard",
                    pattern: "admin/ho-so-ca-nhan",
                    defaults: new { controller = "TaiKhoan", action = "HoSoCaNhan" }
                );

                // Định tuyến: TaiKhoan-PhanQuyen
                endpoints.MapAreaControllerRoute(
                    name: "r_dangxuat",
                    areaName: "Dashboard",
                    pattern: "admin/phan-quyen-truy-cap",
                    defaults: new { controller = "TaiKhoan", action = "PhanQuyenTruyCap" }
                );

                // Định tuyến: ThemSanPham
                endpoints.MapAreaControllerRoute(
                    name: "r_sanpham_them",
                    areaName: "Dashboard",
                    pattern: "admin/them-san-pham",
                    defaults: new { controller = "SanPham", action = "ThemSanPham" }
                );

                // Định tuyến: DSSanPham
                endpoints.MapAreaControllerRoute(
                    name: "r_sanpham_danhsach",
                    areaName: "Dashboard",
                    pattern: "admin/danh-sach-san-pham",
                    defaults: new { controller = "SanPham", action = "DanhSachSanPham" }
                );

                // Định tuyến: DuyetSanPham
                endpoints.MapAreaControllerRoute(
                    name: "r_sanpham_duyet",
                    areaName: "Dashboard",
                    pattern: "admin/duyet-san-pham",
                    defaults: new { controller = "SanPham", action = "DuyetSanPham" }
                );

                // Định tuyến: ChiTietSanPham
                endpoints.MapAreaControllerRoute(
                    name: "r_sanpham_chitiet",
                    areaName: "Dashboard",
                    pattern: "admin/san-pham/{p?}/{spid?}/{pkey?}",
                    defaults: new { controller = "SanPham", action = "ChiTietSanPham" }
                );

                // Định tuyến: DanhSachKhachHang
                endpoints.MapAreaControllerRoute(
                    name: "r_khachhang",
                    areaName: "Dashboard",
                    pattern: "admin/danh-sach-khach-hang",
                    defaults: new { controller = "KhachHang", action = "DanhSachKhachHang" }
                );

                // Định tuyến: KhachHang_ThanThiet
                endpoints.MapAreaControllerRoute(
                    name: "r_khachhang_thanthiet",
                    areaName: "Dashboard",
                    pattern: "admin/khach-hang-than-thiet",
                    defaults: new { controller = "KhachHang", action = "KhachHangThanThiet" }
                );

                // Định tuyến: HeThong_TrangThai
                endpoints.MapAreaControllerRoute(
                    name: "r_trangthai",
                    areaName: "Dashboard",
                    pattern: "admin/trang-thai-he-thong",
                    defaults: new { controller = "HeThong", action = "TrangThai" }
                );

                // Định tuyến: HeThong_NhatKy
                endpoints.MapAreaControllerRoute(
                    name: "r_nhatky",
                    areaName: "Dashboard",
                    pattern: "admin/nhat-ky-truy-cap",
                    defaults: new { controller = "HeThong", action = "NhatKyTruyCap" }
                );

                // Định tuyến: TaiKhoan_DoiMatKhau
                endpoints.MapAreaControllerRoute(
                    name: "r_doimatkhau",
                    areaName: "Dashboard",
                    pattern: "admin/doi-mat-khau",
                    defaults: new { controller = "TaiKhoan", action = "DoiMatKhau" }
                );

                // Định tuyến: DonHang_DanhSach
                endpoints.MapAreaControllerRoute(
                    name: "r_donhang_danhsach",
                    areaName: "Dashboard",
                    pattern: "admin/danh-sach-don-hang",
                    defaults: new { controller = "DonHang", action = "DanhSachDonHang" }
                );

                // Định tuyến: DonHang_DaHuy
                endpoints.MapAreaControllerRoute(
                    name: "r_donhang_danhsach_dahuy",
                    areaName: "Dashboard",
                    pattern: "admin/don-hang-da-huy",
                    defaults: new { controller = "DonHang", action = "DaHuy" }
                );

                // Định tuyến: DonHang_ChoGiao
                endpoints.MapAreaControllerRoute(
                    name: "r_donhang_danhsach_chogiao",
                    areaName: "Dashboard",
                    pattern: "admin/don-hang-cho-giao",
                    defaults: new { controller = "DonHang", action = "ChoGiaoHang" }
                );

                // Định tuyến: DonHang_DangGiao
                endpoints.MapAreaControllerRoute(
                    name: "r_donhang_danhsach_danggiao",
                    areaName: "Dashboard",
                    pattern: "admin/don-hang-dang-giao",
                    defaults: new { controller = "DonHang", action = "DangGiao" }
                );

                // Định tuyến: DonHang_GiaoThanhCong
                endpoints.MapAreaControllerRoute(
                    name: "r_donhang_danhsach_thanhcong",
                    areaName: "Dashboard",
                    pattern: "admin/don-hang-giao-thanh-cong",
                    defaults: new { controller = "DonHang", action = "GiaoThanhCong" }
                );

                // Định tuyến: DonHang_ChiTiet
                endpoints.MapAreaControllerRoute(
                    name: "r_donhang_danhsach_chitiet",
                    areaName: "Dashboard",
                    pattern: "admin/don-hang/{hdid?}/{pkey?}",
                    defaults: new { controller = "DonHang", action = "ChiTietDonHang" }
                );

                // Định tuyến: DonHang_BatDauGiao
                endpoints.MapAreaControllerRoute(
                    name: "r_donhang_batdaugiao",
                    areaName: "Dashboard",
                    pattern: "admin/bat-dau-giao-hang",
                    defaults: new { controller = "DonHang", action = "XacNhan_GiaoHang" }
                );

                // Định tuyến: DonHang_DaGiao
                endpoints.MapAreaControllerRoute(
                    name: "r_donhang_dagiao",
                    areaName: "Dashboard",
                    pattern: "admin/xac-nhan-da-giao-hang",
                    defaults: new { controller = "DonHang", action = "XacNhan_DaGiao" }
                );

                // Định tuyến: DangBaiViet
                endpoints.MapAreaControllerRoute(
                    name: "r_dangbaiviet",
                    areaName: "Dashboard",
                    pattern: "admin/dang-bai-viet",
                    defaults: new { controller = "BaiViet", action = "DangBaiViet" }
                );

                // Định tuyến: DuyetBaiViet
                endpoints.MapAreaControllerRoute(
                    name: "r_duyetbaiviet",
                    areaName: "Dashboard",
                    pattern: "admin/duyet-bai-viet",
                    defaults: new { controller = "BaiViet", action = "DuyetBaiViet" }
                );

                // Định tuyến: BaiVietCuaToi
                endpoints.MapAreaControllerRoute(
                    name: "r_baivietcuatoi",
                    areaName: "Dashboard",
                    pattern: "admin/bai-viet-cua-toi",
                    defaults: new { controller = "BaiViet", action = "BaiVietCuaToi" }
                );

                // Định tuyến: TatCaBaiViet
                endpoints.MapAreaControllerRoute(
                    name: "r_tatcabaiviet",
                    areaName: "Dashboard",
                    pattern: "admin/tat-ca-bai-viet",
                    defaults: new { controller = "BaiViet", action = "TatCaBaiViet" }
                );

                // Định tuyến: ChiTietBaiViet
                endpoints.MapAreaControllerRoute(
                    name: "r_chitietbaiviet",
                    areaName: "Dashboard",
                    pattern: "admin/bai-viet/{p?}/{bvid?}/{pkey?}",
                    defaults: new { controller = "BaiViet", action = "ChiTietBaiViet" }
                );

                // Định tuyến: Home
                endpoints.MapAreaControllerRoute(
                    name: "r_home",
                    areaName: "Dashboard",
                    pattern: "admin",
                    defaults: new { controller = "Home", action = "Index" }
                );

                // Định tuyến: Mặc định của All Areas
                endpoints.MapControllerRoute(
                    name: "Default_Areas",
                    pattern: "{area:exists}/{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );

            });
        }
    }
}

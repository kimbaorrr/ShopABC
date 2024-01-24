namespace ShopABC_DB;

public partial class Chitietdonhang
{
    public int Sohd { get; set; }

    public int Masp { get; set; }

    public int? Soluong { get; set; }

    public int? Thanhtien { get; set; }

    public virtual Sanpham MaspNavigation { get; set; }

    public virtual Donhang SohdNavigation { get; set; }
}

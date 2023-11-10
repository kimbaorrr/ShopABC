namespace ShopABC_DB;

public partial class KhNhanxet
{
    public int Makh { get; set; }

    public int Masp { get; set; }

    public string Nhanxet { get; set; }

    public int? Dgsao { get; set; }

    public virtual Khachhang MakhNavigation { get; set; }

    public virtual Sanpham MaspNavigation { get; set; }
}

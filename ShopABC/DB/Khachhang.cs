namespace ShopABC_DB;

public partial class Khachhang
{
    public int Makh { get; set; }

    public string Hodem { get; set; }

    public string Tenkh { get; set; }

    public string Diachi { get; set; }

    public string Email { get; set; }

    public string Sdt { get; set; }

    public virtual ICollection<Donhang> Donhangs { get; } = new List<Donhang>();

    public virtual ICollection<KhNhanxet> KhNhanxets { get; } = new List<KhNhanxet>();
}

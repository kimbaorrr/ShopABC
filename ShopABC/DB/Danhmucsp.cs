namespace ShopABC_DB;

public partial class Danhmucsp
{
    public int Madm { get; set; }

    public string Tendm { get; set; }

    public int Maloai { get; set; }

    public virtual Loaisp MaloaiNavigation { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; } = new List<Sanpham>();
}

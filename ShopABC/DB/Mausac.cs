namespace ShopABC_DB;

public partial class Mausac
{
    public string Mamau { get; set; }

    public string Tenmau { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; } = new List<Sanpham>();
}

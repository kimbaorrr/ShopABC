namespace ShopABC_DB;

public partial class Loaisp
{
    public int Maloai { get; set; }

    public string Tenloai { get; set; }

    public virtual ICollection<Danhmucsp> Danhmucsps { get; } = new List<Danhmucsp>();
}

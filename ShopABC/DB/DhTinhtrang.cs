namespace ShopABC_DB;

public partial class DhTinhtrang
{
    public short Matt { get; set; }

    public string Tentt { get; set; }

    public virtual ICollection<Donhang> Donhangs { get; } = new List<Donhang>();
}

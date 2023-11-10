namespace ShopABC_DB;

public partial class Hangsx
{
    public int Mahsx { get; set; }

    public string Tenhsx { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; } = new List<Sanpham>();
}

namespace ShopABC_DB;

public partial class BvBinhluan
{
    public int Mabv { get; set; }

    public string Hoten { get; set; }

    public string Email { get; set; }

    public string Sdt { get; set; }

    public string Binhluan { get; set; }

    public DateTime? Thoigian { get; set; }

    public int FId { get; set; }

    public virtual Baiviet MabvNavigation { get; set; }
}

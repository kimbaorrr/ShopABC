namespace ShopABC_DB;

public partial class Donhang
{
    public int Sohd { get; set; }

    public int Makh { get; set; }

    public DateTime? Ngaymua { get; set; }

    public int? Tonggt { get; set; }

    public string Ghichu { get; set; }

    public short? Matt { get; set; }

    public DateTime? Ngaygiao { get; set; }

    public DateTime? Ngaynhan { get; set; }

    public string Lydo { get; set; }

    public DateTime? Ngayhuy { get; set; }

    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; } = new List<Chitietdonhang>();

    public virtual Khachhang MakhNavigation { get; set; }

    public virtual DhTinhtrang MattNavigation { get; set; }
}

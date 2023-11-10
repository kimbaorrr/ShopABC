namespace ShopABC_DB;

public partial class Chucvu
{
    public short Macv { get; set; }

    public string Tencv { get; set; }

    public virtual ICollection<Nhanvien> Nhanviens { get; } = new List<Nhanvien>();

    public virtual Phanquyen Phanquyen { get; set; }
}

namespace ShopABC_DB;

public partial class Nhanvien
{
    public int Manv { get; set; }

    public string Hodem { get; set; }

    public string Tennv { get; set; }

    public DateOnly? Ngaysinh { get; set; }

    public bool? Gioitinh { get; set; }

    public string Diachi { get; set; }

    public string Cccd { get; set; }

    public string Email { get; set; }

    public string Sdt { get; set; }

    public string Hinhnv { get; set; }

    public short? Macv { get; set; }

    public virtual ICollection<Baiviet> BaivietManvNavigations { get; } = new List<Baiviet>();

    public virtual ICollection<Baiviet> BaivietNguoiduyetNavigations { get; } = new List<Baiviet>();

    public virtual ICollection<History> Histories { get; } = new List<History>();

    public virtual Chucvu MacvNavigation { get; set; }

    public virtual NvDangnhap NvDangnhap { get; set; }

    public virtual ICollection<Sanpham> SanphamManvNavigations { get; } = new List<Sanpham>();

    public virtual ICollection<Sanpham> SanphamNguoiduyetNavigations { get; } = new List<Sanpham>();
}

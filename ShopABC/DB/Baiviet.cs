namespace ShopABC_DB;

public partial class Baiviet
{
    public int Mabv { get; set; }

    public DateTime? Ngaydang { get; set; }

    public int Manv { get; set; }

    public bool? Duyet { get; set; }

    public string Hinhbv { get; set; }

    public string Tieude { get; set; }

    public string Noidung { get; set; }

    public int? Luotxem { get; set; }

    public DateTime? Ngayduyet { get; set; }

    public bool? Draft { get; set; }

    public int? Nguoiduyet { get; set; }

    public virtual ICollection<BvBinhluan> BvBinhluans { get; } = new List<BvBinhluan>();

    public virtual Nhanvien ManvNavigation { get; set; }

    public virtual Nhanvien NguoiduyetNavigation { get; set; }
}

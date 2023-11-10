namespace ShopABC_DB;

public partial class Sanpham
{
    public int Masp { get; set; }

    public int Madm { get; set; }

    public string Tensp { get; set; }

    public int Mahsx { get; set; }

    public string Chatlieu { get; set; }

    public int? Giaban { get; set; }

    public short Thuevat { get; set; }

    public short? Giamgia { get; set; }

    public bool? Duyet { get; set; }

    public string Hinhsp { get; set; }

    public string Kieudang { get; set; }

    public DateTime? Ngaynhap { get; set; }

    public string Mota { get; set; }

    public string Noidung { get; set; }

    public int? Manv { get; set; }

    public DateTime? Ngayduyet { get; set; }

    public int? Nguoiduyet { get; set; }

    public string Mamau { get; set; }

    public string Donvitinh { get; set; }

    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; } = new List<Chitietdonhang>();

    public virtual ICollection<KhNhanxet> KhNhanxets { get; } = new List<KhNhanxet>();

    public virtual Danhmucsp MadmNavigation { get; set; }

    public virtual Hangsx MahsxNavigation { get; set; }

    public virtual Mausac MamauNavigation { get; set; }

    public virtual Nhanvien ManvNavigation { get; set; }

    public virtual Nhanvien NguoiduyetNavigation { get; set; }
}

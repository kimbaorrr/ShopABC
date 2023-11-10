namespace ShopABC_DB;

public partial class Phanquyen
{
    public short Macv { get; set; }

    public bool? DuyetSanpham { get; set; }

    public bool? DuyetBaiviet { get; set; }

    public bool? XacnhanDonhang { get; set; }

    public bool? Phanquyentruycap { get; set; }

    public bool? Nhatkytruycap { get; set; }

    public bool? XemBaivietTatcabaiviet { get; set; }

    public bool? ThemBaiviet { get; set; }

    public bool? ThemSanpham { get; set; }

    public bool? XemSanpham { get; set; }

    public bool? XemSanphamDssp { get; set; }

    public bool? XemBaiviet { get; set; }

    public bool? XemBangdieukhien { get; set; }

    public bool? XemDonhang { get; set; }

    public bool? XemKhachhang { get; set; }

    public virtual Chucvu MacvNavigation { get; set; }
}

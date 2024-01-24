namespace ShopABC_DB;

public partial class NvDangnhap
{
    public int Manv { get; set; }

    public string Tendn { get; set; }

    public string Matkhau { get; set; }

    public virtual Nhanvien ManvNavigation { get; set; }
}

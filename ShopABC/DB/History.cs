namespace ShopABC_DB;

public partial class History
{
    public int Id { get; set; }

    public string Diachiip { get; set; }

    public DateTime? Thoigian { get; set; }

    public string Hanhdong { get; set; }

    public int? Manv { get; set; }

    public string UserAgent { get; set; }

    public virtual Nhanvien ManvNavigation { get; set; }
}

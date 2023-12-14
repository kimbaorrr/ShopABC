using Microsoft.EntityFrameworkCore;

namespace ShopABC_DB;

public partial class ShopABC_Entities : DbContext
{
    public ShopABC_Entities()
    {
    }

    public ShopABC_Entities(DbContextOptions<ShopABC_Entities> options)
        : base(options)
    {
    }

    public virtual DbSet<Baiviet> Baiviets { get; set; }

    public virtual DbSet<BannerTrangchu> BannerTrangchus { get; set; }

    public virtual DbSet<BvBinhluan> BvBinhluans { get; set; }

    public virtual DbSet<Chitietdonhang> Chitietdonhangs { get; set; }

    public virtual DbSet<Chucvu> Chucvus { get; set; }

    public virtual DbSet<Danhmucsp> Danhmucsps { get; set; }

    public virtual DbSet<DhTinhtrang> DhTinhtrangs { get; set; }

    public virtual DbSet<Donhang> Donhangs { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Hangsx> Hangsxes { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<KhNhanxet> KhNhanxets { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Loaisp> Loaisps { get; set; }

    public virtual DbSet<Mausac> Mausacs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<NvDangnhap> NvDangnhaps { get; set; }

    public virtual DbSet<Phanquyen> Phanquyens { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<ThongtinCty> ThongtinCties { get; set; }

    public virtual DbSet<Doanhthu> Doanhthus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baiviet>(entity =>
        {
            entity.HasKey(e => e.Mabv).HasName("baiviet_pkey");

            entity.ToTable("baiviet");

            entity.HasIndex(e => e.Manv, "fki_a");

            entity.HasIndex(e => e.Nguoiduyet, "fki_baiviet_nguoiduyet_fkey");

            entity.Property(e => e.Mabv).HasColumnName("mabv");
            entity.Property(e => e.Draft)
                .HasDefaultValueSql("false")
                .HasColumnName("draft");
            entity.Property(e => e.Duyet)
                .HasDefaultValueSql("false")
                .HasColumnName("duyet");
            entity.Property(e => e.Hinhbv).HasColumnName("hinhbv");
            entity.Property(e => e.Luotxem)
                .HasDefaultValueSql("0")
                .HasColumnName("luotxem");
            entity.Property(e => e.Manv).HasColumnName("manv");
            entity.Property(e => e.Ngaydang)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaydang");
            entity.Property(e => e.Ngayduyet)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayduyet");
            entity.Property(e => e.Nguoiduyet).HasColumnName("nguoiduyet");
            entity.Property(e => e.Noidung).HasColumnName("noidung");
            entity.Property(e => e.Tieude)
                .HasMaxLength(255)
                .HasColumnName("tieude");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.BaivietManvNavigations)
                .HasForeignKey(d => d.Manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("baiviet_manv_fkey");

            entity.HasOne(d => d.NguoiduyetNavigation).WithMany(p => p.BaivietNguoiduyetNavigations)
                .HasForeignKey(d => d.Nguoiduyet)
                .HasConstraintName("baiviet_nguoiduyet_fkey");
        });

        modelBuilder.Entity<BannerTrangchu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banner_trangchu_pkey");

            entity.ToTable("banner_trangchu");

            entity.HasIndex(e => e.Tenbanner, "uq_tenbanner").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Gtngan)
                .HasMaxLength(500)
                .HasColumnName("gtngan");
            entity.Property(e => e.Hinhgt)
                .HasMaxLength(100)
                .HasColumnName("hinhgt");
            entity.Property(e => e.Ngaydang)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaydang");
            entity.Property(e => e.Tenbanner)
                .HasMaxLength(140)
                .HasColumnName("tenbanner");
        });

        modelBuilder.Entity<BvBinhluan>(entity =>
        {
            entity.HasKey(e => e.FId).HasName("pkey_binhluan");

            entity.ToTable("bv_binhluan");

            entity.Property(e => e.FId).HasColumnName("f_id");
            entity.Property(e => e.Binhluan)
                .HasMaxLength(255)
                .HasColumnName("binhluan");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("hoten");
            entity.Property(e => e.Mabv).HasColumnName("mabv");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("sdt");
            entity.Property(e => e.Thoigian)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("thoigian");

            entity.HasOne(d => d.MabvNavigation).WithMany(p => p.BvBinhluans)
                .HasForeignKey(d => d.Mabv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkey_baiviet_mabv");
        });

        modelBuilder.Entity<Chitietdonhang>(entity =>
        {
            entity.HasKey(e => new { e.Sohd, e.Masp }).HasName("chitietdonhang_pkey");

            entity.ToTable("chitietdonhang");

            entity.Property(e => e.Sohd).HasColumnName("sohd");
            entity.Property(e => e.Masp).HasColumnName("masp");
            entity.Property(e => e.Soluong)
                .HasDefaultValueSql("0")
                .HasColumnName("soluong");
            entity.Property(e => e.Thanhtien)
                .HasDefaultValueSql("0")
                .HasColumnName("thanhtien");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Chitietdonhangs)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("chitietdonhang_masp_fkey");

            entity.HasOne(d => d.SohdNavigation).WithMany(p => p.Chitietdonhangs)
                .HasForeignKey(d => d.Sohd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("chitietdonhang_sohd_fkey");
        });

        modelBuilder.Entity<Chucvu>(entity =>
        {
            entity.HasKey(e => e.Macv).HasName("chucvu_pkey");

            entity.ToTable("chucvu");

            entity.HasIndex(e => e.Tencv, "chucvu_tencv_key").IsUnique();

            entity.Property(e => e.Macv).HasColumnName("macv");
            entity.Property(e => e.Tencv)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("tencv");
        });

        modelBuilder.Entity<Danhmucsp>(entity =>
        {
            entity.HasKey(e => e.Madm).HasName("danhmucsp_pkey");

            entity.ToTable("danhmucsp");

            entity.HasIndex(e => e.Tendm, "danhmucsp_tendm_key").IsUnique();

            entity.Property(e => e.Madm).HasColumnName("madm");
            entity.Property(e => e.Maloai).HasColumnName("maloai");
            entity.Property(e => e.Tendm)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("tendm");

            entity.HasOne(d => d.MaloaiNavigation).WithMany(p => p.Danhmucsps)
                .HasForeignKey(d => d.Maloai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("danhmucsp_maloai_fkey");
        });

        modelBuilder.Entity<DhTinhtrang>(entity =>
        {
            entity.HasKey(e => e.Matt).HasName("dh_tinhtrang_pkey");

            entity.ToTable("dh_tinhtrang");

            entity.HasIndex(e => e.Tentt, "dh_tinhtrang_tentt_key").IsUnique();

            entity.Property(e => e.Matt).HasColumnName("matt");
            entity.Property(e => e.Tentt)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("tentt");
        });

        modelBuilder.Entity<Donhang>(entity =>
        {
            entity.HasKey(e => e.Sohd).HasName("donhang_pkey");

            entity.ToTable("donhang");

            entity.Property(e => e.Sohd).HasColumnName("sohd");
            entity.Property(e => e.Ghichu)
                .HasMaxLength(100)
                .HasColumnName("ghichu");
            entity.Property(e => e.Lydo)
                .HasMaxLength(255)
                .HasDefaultValueSql("'Không xác định'::character varying")
                .HasColumnName("lydo");
            entity.Property(e => e.Makh).HasColumnName("makh");
            entity.Property(e => e.Matt).HasColumnName("matt");
            entity.Property(e => e.Ngaygiao)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaygiao");
            entity.Property(e => e.Ngayhuy)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("ngayhuy");
            entity.Property(e => e.Ngaymua)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaymua");
            entity.Property(e => e.Ngaynhan)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaynhan");
            entity.Property(e => e.Tonggt)
                .HasDefaultValueSql("0")
                .HasColumnName("tonggt");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Donhangs)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("donhang_makh_fkey");

            entity.HasOne(d => d.MattNavigation).WithMany(p => p.Donhangs)
                .HasForeignKey(d => d.Matt)
                .HasConstraintName("donhang_matt_fkey");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.Cauhoi).HasName("pkey_cauhoi");

            entity.ToTable("faq");

            entity.Property(e => e.Cauhoi)
                .HasMaxLength(100)
                .HasColumnName("cauhoi");
            entity.Property(e => e.Ngaydang)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaydang");
            entity.Property(e => e.Traloi)
                .HasMaxLength(255)
                .HasColumnName("traloi");
        });

        modelBuilder.Entity<Hangsx>(entity =>
        {
            entity.HasKey(e => e.Mahsx).HasName("hangsx_pkey");

            entity.ToTable("hangsx");

            entity.HasIndex(e => e.Tenhsx, "hangsx_tenhsx_key").IsUnique();

            entity.Property(e => e.Mahsx).HasColumnName("mahsx");
            entity.Property(e => e.Tenhsx)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("tenhsx");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("his_pkey");

            entity.ToTable("history");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('his_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Diachiip)
                .HasMaxLength(100)
                .HasDefaultValueSql("'::1'::character varying")
                .HasColumnName("diachiip");
            entity.Property(e => e.Hanhdong)
                .HasMaxLength(100)
                .HasColumnName("hanhdong");
            entity.Property(e => e.Manv).HasColumnName("manv");
            entity.Property(e => e.Thoigian)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("thoigian");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("his_manv_fkey");
        });

        modelBuilder.Entity<KhNhanxet>(entity =>
        {
            entity.HasKey(e => new { e.Makh, e.Masp }).HasName("kh_nhanxet_pkey");

            entity.ToTable("kh_nhanxet");

            entity.Property(e => e.Makh).HasColumnName("makh");
            entity.Property(e => e.Masp).HasColumnName("masp");
            entity.Property(e => e.Dgsao)
                .HasDefaultValueSql("5")
                .HasColumnName("dgsao");
            entity.Property(e => e.Nhanxet)
                .HasMaxLength(255)
                .HasColumnName("nhanxet");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.KhNhanxets)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kh_nhanxet_makh_fkey");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.KhNhanxets)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kh_nhanxet_masp_fkey");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Makh).HasName("khachhang_pkey");

            entity.ToTable("khachhang");

            entity.Property(e => e.Makh).HasColumnName("makh");
            entity.Property(e => e.Diachi)
                .HasMaxLength(80)
                .HasColumnName("diachi");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.Hodem)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("hodem");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("sdt");
            entity.Property(e => e.Tenkh)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("tenkh");
        });

        modelBuilder.Entity<Loaisp>(entity =>
        {
            entity.HasKey(e => e.Maloai).HasName("loaisp_pkey");

            entity.ToTable("loaisp");

            entity.HasIndex(e => e.Tenloai, "loaisp_tenloai_key").IsUnique();

            entity.Property(e => e.Maloai).HasColumnName("maloai");
            entity.Property(e => e.Tenloai)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("tenloai");
        });

        modelBuilder.Entity<Mausac>(entity =>
        {
            entity.HasKey(e => e.Mamau).HasName("mausac_pkey");

            entity.ToTable("mausac");

            entity.HasIndex(e => e.Tenmau, "uq_tenmau").IsUnique();

            entity.Property(e => e.Mamau)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("mamau");
            entity.Property(e => e.Tenmau)
                .HasMaxLength(80)
                .HasColumnName("tenmau");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manv).HasName("nhanvien_pkey");

            entity.ToTable("nhanvien");

            entity.HasIndex(e => e.Cccd, "nhanvien_cccd_key").IsUnique();

            entity.Property(e => e.Manv).HasColumnName("manv");
            entity.Property(e => e.Cccd)
                .IsRequired()
                .HasMaxLength(12)
                .IsFixedLength()
                .HasColumnName("cccd");
            entity.Property(e => e.Diachi)
                .HasMaxLength(80)
                .HasColumnName("diachi");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.Gioitinh)
                .HasDefaultValueSql("false")
                .HasColumnName("gioitinh");
            entity.Property(e => e.Hinhnv)
                .HasMaxLength(100)
                .HasColumnName("hinhnv");
            entity.Property(e => e.Hodem)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("hodem");
            entity.Property(e => e.Macv).HasColumnName("macv");
            entity.Property(e => e.Ngaysinh).HasColumnName("ngaysinh");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("sdt");
            entity.Property(e => e.Tennv)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("tennv");

            entity.HasOne(d => d.MacvNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.Macv)
                .HasConstraintName("nhanvien_macv_fkey");
        });

        modelBuilder.Entity<NvDangnhap>(entity =>
        {
            entity.HasKey(e => e.Manv).HasName("nv_dangnhap_pkey");

            entity.ToTable("nv_dangnhap");

            entity.HasIndex(e => e.Tendn, "nv_dangnhap_tendn_key").IsUnique();

            entity.Property(e => e.Manv)
                .ValueGeneratedNever()
                .HasColumnName("manv");
            entity.Property(e => e.Matkhau)
                .IsRequired()
                .HasColumnName("matkhau");
            entity.Property(e => e.Tendn)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("tendn");

            entity.HasOne(d => d.ManvNavigation).WithOne(p => p.NvDangnhap)
                .HasForeignKey<NvDangnhap>(d => d.Manv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nv_dangnhap_manv_fkey");
        });

        modelBuilder.Entity<Phanquyen>(entity =>
        {
            entity.HasKey(e => e.Macv).HasName("phanquyen_pkey");

            entity.ToTable("phanquyen");

            entity.Property(e => e.Macv)
                .ValueGeneratedNever()
                .HasColumnName("macv");
            entity.Property(e => e.DuyetBaiviet)
                .HasDefaultValueSql("false")
                .HasColumnName("duyet_baiviet");
            entity.Property(e => e.DuyetSanpham)
                .HasDefaultValueSql("false")
                .HasColumnName("duyet_sanpham");
            entity.Property(e => e.Nhatkytruycap)
                .HasDefaultValueSql("false")
                .HasColumnName("nhatkytruycap");
            entity.Property(e => e.Phanquyentruycap)
                .HasDefaultValueSql("false")
                .HasColumnName("phanquyentruycap");
            entity.Property(e => e.ThemBaiviet)
                .HasDefaultValueSql("false")
                .HasColumnName("them_baiviet");
            entity.Property(e => e.ThemSanpham)
                .HasDefaultValueSql("false")
                .HasColumnName("them_sanpham");
            entity.Property(e => e.XacnhanDonhang)
                .HasDefaultValueSql("false")
                .HasColumnName("xacnhan_donhang");
            entity.Property(e => e.XemBaiviet)
                .HasDefaultValueSql("false")
                .HasColumnName("xem_baiviet");
            entity.Property(e => e.XemBaivietTatcabaiviet)
                .HasDefaultValueSql("false")
                .HasColumnName("xem_baiviet_tatcabaiviet");
            entity.Property(e => e.XemBangdieukhien)
                .HasDefaultValueSql("false")
                .HasColumnName("xem_bangdieukhien");
            entity.Property(e => e.XemDonhang)
                .HasDefaultValueSql("false")
                .HasColumnName("xem_donhang");
            entity.Property(e => e.XemKhachhang)
                .HasDefaultValueSql("false")
                .HasColumnName("xem_khachhang");
            entity.Property(e => e.XemSanpham)
                .HasDefaultValueSql("false")
                .HasColumnName("xem_sanpham");
            entity.Property(e => e.XemSanphamDssp)
                .HasDefaultValueSql("false")
                .HasColumnName("xem_sanpham_dssp");

            entity.HasOne(d => d.MacvNavigation).WithOne(p => p.Phanquyen)
                .HasForeignKey<Phanquyen>(d => d.Macv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("phanquyen_macv_fkey");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.Masp).HasName("sanpham_pkey");

            entity.ToTable("sanpham");

            entity.HasIndex(e => e.Mamau, "fki_fk_mamau");

            entity.HasIndex(e => e.Nguoiduyet, "fki_sanpham_nguoiduyet_fkey");

            entity.HasIndex(e => e.Tensp, "sanpham_tensp_key").IsUnique();

            entity.Property(e => e.Masp).HasColumnName("masp");
            entity.Property(e => e.Chatlieu)
                .HasMaxLength(50)
                .HasColumnName("chatlieu");
            entity.Property(e => e.Donvitinh)
                .HasMaxLength(10)
                .HasDefaultValueSql("'cái'::character varying")
                .HasColumnName("donvitinh");
            entity.Property(e => e.Duyet)
                .HasDefaultValueSql("false")
                .HasColumnName("duyet");
            entity.Property(e => e.Giaban).HasColumnName("giaban");
            entity.Property(e => e.Giamgia)
                .HasDefaultValueSql("0")
                .HasColumnName("giamgia");
            entity.Property(e => e.Hinhsp).HasColumnName("hinhsp");
            entity.Property(e => e.Kieudang)
                .HasMaxLength(50)
                .HasColumnName("kieudang");
            entity.Property(e => e.Madm).HasColumnName("madm");
            entity.Property(e => e.Mahsx).HasColumnName("mahsx");
            entity.Property(e => e.Mamau)
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("mamau");
            entity.Property(e => e.Manv).HasColumnName("manv");
            entity.Property(e => e.Mota)
                .HasMaxLength(255)
                .HasColumnName("mota");
            entity.Property(e => e.Ngayduyet)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngayduyet");
            entity.Property(e => e.Ngaynhap)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaynhap");
            entity.Property(e => e.Nguoiduyet).HasColumnName("nguoiduyet");
            entity.Property(e => e.Noidung).HasColumnName("noidung");
            entity.Property(e => e.Tensp)
                .HasMaxLength(255)
                .HasColumnName("tensp");
            entity.Property(e => e.Thuevat).HasColumnName("thuevat");

            entity.HasOne(d => d.MadmNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Madm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sanpham_madm_fkey");

            entity.HasOne(d => d.MahsxNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Mahsx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sanpham_mahsx_fkey");

            entity.HasOne(d => d.MamauNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Mamau)
                .HasConstraintName("fk_mamau");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.SanphamManvNavigations)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("sanpham_manv_fkey");

            entity.HasOne(d => d.NguoiduyetNavigation).WithMany(p => p.SanphamNguoiduyetNavigations)
                .HasForeignKey(d => d.Nguoiduyet)
                .HasConstraintName("sanpham_nguoiduyet_fkey");
        });

        modelBuilder.Entity<Doanhthu>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<ThongtinCty>(entity =>
        {
            entity.HasKey(e => e.Tieude).HasName("pkey_tieude");

            entity.ToTable("thongtin_cty");

            entity.Property(e => e.Tieude)
                .HasMaxLength(255)
                .HasColumnName("tieude");
            entity.Property(e => e.Ngaydang)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ngaydang");
            entity.Property(e => e.Noidung).HasColumnName("noidung");
        });
        modelBuilder.HasSequence<int>("baiviet_mabv_seq").StartsAt(10000000L);
        modelBuilder.HasSequence<int>("banner_trangchu_id_seq").StartsAt(2L);
        modelBuilder.HasSequence<short>("chucvu_macv_seq");
        modelBuilder.HasSequence<int>("donhang_sohd_seq").StartsAt(20000000L);
        modelBuilder.HasSequence<int>("his_id_seq");
        modelBuilder.HasSequence<int>("khachhang_makh_seq").HasMin(18000000L);
        modelBuilder.HasSequence<int>("sanpham_masp_seq").StartsAt(18000000L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

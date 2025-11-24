using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    // DbContext
    public class QuanLyTranhThuContext : DbContext
    {
        public QuanLyTranhThuContext() : base("name=QuanLyTranhThuConnection")
        {
            // Tắt LazyLoading và Proxy để tránh lỗi
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<QuanNhan> QuanNhans { get; set; }
        public virtual DbSet<DonVi> DonVis { get; set; }
        public virtual DbSet<DonTranhThu> DonTranhThus { get; set; }
        public virtual DbSet<DotPhep> DotPheps { get; set; }
        public virtual DbSet<GiayPhep> GiayPheps { get; set; }
        public virtual DbSet<QuaTrinh_DuyetDon> QuaTrinh_DuyetDons { get; set; }
        public virtual DbSet<TheoDoiNghiPhep> TheoDoiNghiPheps { get; set; }
        public virtual DbSet<PhanQuyen_XuLy> PhanQuyen_XuLys { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<CapBac> CapBacs { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<LoaiPhep> LoaiPheps { get; set; }
        public virtual DbSet<VungMien> VungMiens { get; set; }
        public virtual DbSet<HanhDong_XuLy> HanhDong_XuLys { get; set; }
        public virtual DbSet<CapDonVi> CapDonVis { get; set; }
        public virtual DbSet<PhanBo> PhanBos { get; set; }
        public virtual DbSet<QuyenHeThong> QuyenHeThongs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonVi>()
                .HasOptional(d => d.DonViCha_Navigation)
                .WithMany()
                .HasForeignKey(d => d.DonViCha)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhanBo>()
                .HasKey(p => new { p.MaDot, p.MaDonVi });

            modelBuilder.Entity<PhanQuyen_XuLy>()
                .HasKey(p => new { p.MaQN, p.MaDonVi });
        }
    }

    // ===================================================
    // ENTITY CLASSES
    // ===================================================

    [Table("QuanNhan")]
    public class QuanNhan
    {
        [Key]
        [StringLength(10)]
        public string MaQN { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(5)]
        public string MaCapBac { get; set; }

        [StringLength(10)]
        public string MaChucVu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDonVi { get; set; }

        [StringLength(20)]
        public string SoDienThoai { get; set; }
    }

    [Table("DonVi")]
    public class DonVi
    {
        [Key]
        [StringLength(10)]
        public string MaDonVi { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDonVi { get; set; }

        [Required]
        [StringLength(5)]
        [Column(TypeName = "char")]
        public string MaCapDonVi { get; set; }

        [StringLength(10)]
        public string DonViCha { get; set; }

        public virtual DonVi DonViCha_Navigation { get; set; }
    }

    [Table("CapBac")]
    public class CapBac
    {
        [Key]
        [StringLength(5)]
        public string MaCapBac { get; set; }

        [StringLength(20)]
        public string TenCapBac { get; set; }
    }

    [Table("ChucVu")]
    public class ChucVu
    {
        [Key]
        [StringLength(10)]
        public string MaChucVu { get; set; }

        [StringLength(30)]
        public string TenChucVu { get; set; }
    }

    [Table("CapDonVi")]
    public class CapDonVi
    {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "char")]
        public string MaCap { get; set; }

        [StringLength(20)]
        public string TenCap { get; set; }
    }
    [Table("DonTranhThu")]
    public class DonTranhThu
    {
        [Key]
        [StringLength(20)]
        public string MaDon { get; set; }

        [Required]
        [StringLength(10)]
        public string MaQN { get; set; }

        [StringLength(20)]
        public string CapBac { get; set; }

        [StringLength(20)]
        public string ChucVu { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDot { get; set; }

        [StringLength(200)]
        public string LyDo { get; set; }

        [Required]
        public DateTime TuNgay { get; set; }

        [Required]
        public DateTime DenNgay { get; set; }

        [StringLength(200)]
        public string NoiNghi { get; set; }

        public byte? MaVungMien { get; set; }

        [Required]
        [StringLength(10)]
        public string NguoiTao { get; set; }

        public byte BuocHienTai { get; set; } = 1;

        public DateTime NgayNop { get; set; } = DateTime.Now;
    }

    [Table("DotPhep")]
    public class DotPhep
    {
        [Key]
        [StringLength(10)]
        public string MaDot { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLoaiPhep { get; set; }

        [Required]
        public DateTime TuNgay { get; set; }

        [Required]
        public DateTime DenNgay { get; set; }

        [Required]
        [StringLength(10)]
        public string NguoiTao { get; set; }

        public bool TrangThai { get; set; } = true;

        [StringLength(100)]
        public string GhiChu { get; set; }
    }

    [Table("LoaiPhep")]
    public class LoaiPhep
    {
        [Key]
        [StringLength(10)]
        public string MaLoai { get; set; }

        [StringLength(30)]
        public string TenLoai { get; set; }
    }

    [Table("VungMien")]
    public class VungMien
    {
        [Key]
        public byte MaVungMien { get; set; }

        [StringLength(20)]
        public string TenVungMien { get; set; }
    }

    [Table("GiayPhep")]
    public class GiayPhep
    {
        [Key]
        [StringLength(10)]
        public string SoGiay { get; set; }

        [Required]
        [StringLength(20)]
        public string MaDon { get; set; }

        [Required]
        public DateTime NgayCap { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNguoiKy { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }
    }

    [Table("QuaTrinh_DuyetDon")]
    public class QuaTrinh_DuyetDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaXuLy { get; set; }

        [Required]
        [StringLength(20)]
        public string MaDon { get; set; }

        [Required]
        public int BuocXuLy { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNguoiXuLy { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHanhDong { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        public DateTime ThoiGian { get; set; } = DateTime.Now;

        [StringLength(10)]
        public string MaNguoiNhanTiep { get; set; }
    }

    [Table("HanhDong_XuLy")]
    public class HanhDong_XuLy
    {
        [Key]
        [StringLength(10)]
        public string MaHanhDong { get; set; }

        [StringLength(30)]
        public string TenHanhDong { get; set; }
    }
    [Table("TheoDoiNghiPhep")]
    public class TheoDoiNghiPhep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string SoGiay { get; set; }

        public DateTime? NgayDiPhep { get; set; }

        public DateTime? NgayTraPhep { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        [StringLength(10)]
        public string MaNguoiXacNhan { get; set; }
    }

    [Table("PhanQuyen_XuLy")]
    public class PhanQuyen_XuLy
    {
        [Key, Column(Order = 0)]
        [StringLength(10)]
        public string MaQN { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string MaDonVi { get; set; }

        public int UuTien { get; set; } = 1;
    }

    [Table("PhanBo")]
    public class PhanBo
    {
        [Key, Column(Order = 0)]
        [StringLength(10)]
        public string MaDot { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string MaDonVi { get; set; }

        public int? ChiTieuToiDa { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }
    }

    // QUAN TRỌNG: NguoiDung và QuyenHeThong
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        [StringLength(10)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(10)]
        public string MaQN { get; set; }

        [Required]
        [StringLength(256)]
        public string MatKhauHash { get; set; }

        [Column("IDQuyenHT")]
        public byte IDQuyenHT { get; set; }
    }

    [Table("QuyenHeThong")]
    public class QuyenHeThong
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte ID { get; set; }

        [StringLength(10)]
        public string TenQuyen { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DonTranhThuDTO
    {
        public string MaDon { get; set; }
        public string MaQN { get; set; }
        public string HoTenQN { get; set; }
        public string CapBac { get; set; }
        public string ChucVu { get; set; }
        public string MaDot { get; set; }
        public string TenDot { get; set; }
        public string LyDo { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string NoiNghi { get; set; }
        public byte? MaVungMien { get; set; }
        public string TenVungMien { get; set; }
        public string NguoiTao { get; set; }
        public byte BuocHienTai { get; set; }
        public DateTime NgayNop { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public int SoNgayNghi => (DenNgay - TuNgay).Days + 1;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DotPhepDTO
    {
        public string MaDot { get; set; }
        public string MaLoaiPhep { get; set; }
        public string TenLoaiPhep { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string NguoiTao { get; set; }
        public string TenNguoiTao { get; set; }
        public bool TrangThai { get; set; }
        public string TrangThaiText => TrangThai ? "Đang mở" : "Đã đóng";
        public string GhiChu { get; set; }
    }
}

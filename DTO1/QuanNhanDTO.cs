using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QuanNhanDTO
    {
        public string MaQN { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string MaCapBac { get; set; }
        public string TenCapBac { get; set; }
        public string MaChucVu { get; set; }
        public string TenChucVu { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string SoDienThoai { get; set; }
    }
}

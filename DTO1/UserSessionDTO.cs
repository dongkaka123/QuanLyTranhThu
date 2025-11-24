using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserSessionDTO
    {
        public string TenDangNhap { get; set; }
        public string MaQN { get; set; }
        public string HoTen { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public byte IDQuyenHT { get; set; }
        public string TenQuyen { get; set; }
        public bool IsAdmin => IDQuyenHT == 1;
    }
}

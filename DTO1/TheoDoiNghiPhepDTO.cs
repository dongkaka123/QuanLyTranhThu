using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TheoDoiNghiPhepDTO
    {
        public int ID { get; set; }
        public string SoGiay { get; set; }
        public string HoTenQN { get; set; }
        public DateTime? NgayDiPhep { get; set; }
        public DateTime? NgayTraPhep { get; set; }
        public string GhiChu { get; set; }
        public string MaNguoiXacNhan { get; set; }
        public string TenNguoiXacNhan { get; set; }
        public string TrangThai
        {
            get
            {
                if (NgayDiPhep == null) return "Chưa đi phép";
                if (NgayTraPhep == null) return "Đang đi phép";
                return "Đã trả phép";
            }
        }
    }
}

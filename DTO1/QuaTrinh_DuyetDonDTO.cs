using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class QuaTrinh_DuyetDonDTO
    {
        public int MaXuLy { get; set; }
        public string MaDon { get; set; }
        public int BuocXuLy { get; set; }
        public string MaNguoiXuLy { get; set; }
        public string TenNguoiXuLy { get; set; }
        public string MaHanhDong { get; set; }
        public string TenHanhDong { get; set; }
        public string GhiChu { get; set; }
        public DateTime ThoiGian { get; set; }
        public string MaNguoiNhanTiep { get; set; }
        public string TenNguoiNhanTiep { get; set; }
    }
}

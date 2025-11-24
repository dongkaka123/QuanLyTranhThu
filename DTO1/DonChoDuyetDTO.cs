using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DonChoDuyetDTO
    {
        public string MaDon { get; set; }
        public string HoTenQN { get; set; }
        public string DonVi { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public int SoNgay { get; set; }
        public DateTime NgayNop { get; set; }
        public byte BuocHienTai { get; set; }
        public string LyDo { get; set; }
    }
}

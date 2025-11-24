using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongKeDTO
    {
        public string TenDonVi { get; set; }
        public int TongSoDon { get; set; }
        public int DonChoDuyet { get; set; }
        public int DonDaDuyet { get; set; }
        public int DonTuChoi { get; set; }
        public int SoNguoiDiPhep { get; set; }
        public int ChiTieuToiDa { get; set; }
        public int ConLai => ChiTieuToiDa - SoNguoiDiPhep;
    }
}

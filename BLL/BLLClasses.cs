// ===================================================
// File: BLL/BLLClasses.cs - TOÀN BỘ
// ===================================================
using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using DAL;

namespace BLL
{
    // BLL cho NguoiDung
    public class NguoiDungBLL
    {
        private NguoiDungDAL dal = new NguoiDungDAL();

        public (bool success, UserSessionDTO user, string message) Login(string tenDangNhap, string matKhau)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
                    return (false, null, "Vui lòng nhập đầy đủ thông tin");

                var user = dal.Login(tenDangNhap, matKhau);

                if (user != null)
                    return (true, user, "Đăng nhập thành công");

                return (false, null, "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            catch (Exception ex)
            {
                return (false, null, $"Lỗi: {ex.Message}");
            }
        }

        public (bool success, string message) ChangePassword(string tenDangNhap,
            string matKhauCu, string matKhauMoi, string xacNhanMatKhau)
        {
            try
            {
                if (matKhauMoi != xacNhanMatKhau)
                    return (false, "Mật khẩu mới và xác nhận không khớp");

                if (matKhauMoi.Length < 6)
                    return (false, "Mật khẩu mới phải có ít nhất 6 ký tự");

                if (dal.ChangePassword(tenDangNhap, matKhauCu, matKhauMoi))
                    return (true, "Đổi mật khẩu thành công");

                return (false, "Mật khẩu cũ không đúng");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }
    }

    // BLL cho QuanNhan
    public class QuanNhanBLL
    {
        private QuanNhanDAL dal = new QuanNhanDAL();

        public List<QuanNhanDTO> GetAll()
        {
            return dal.GetAll();
        }

        public QuanNhanDTO GetByMaQN(string maQN)
        {
            return dal.GetByMaQN(maQN);
        }

        public List<QuanNhanDTO> GetByDonVi(string maDonVi)
        {
            return dal.GetByDonVi(maDonVi);
        }
    }

    // BLL cho DonTranhThu
    public class DonTranhThuBLL
    {
        private DonTranhThuDAL dal = new DonTranhThuDAL();
        private QuaTrinh_DuyetDonDAL qtDAL = new QuaTrinh_DuyetDonDAL();

        public List<DonTranhThuDTO> GetAll()
        {
            return dal.GetAll();
        }

        public List<DonTranhThuDTO> GetByMaQN(string maQN)
        {
            return dal.GetByMaQN(maQN);
        }

        public List<DonChoDuyetDTO> GetDonChoDuyet(string maDonVi)
        {
            return dal.GetDonChoDuyet(maDonVi);
        }

        public (bool success, string message) TaoDon(DonTranhThuDTO dto, string nguoiTao)
        {
            try
            {
                if (dto.TuNgay >= dto.DenNgay)
                    return (false, "Ngày kết thúc phải sau ngày bắt đầu");

                if (dto.TuNgay < DateTime.Now.Date)
                    return (false, "Không thể tạo đơn cho ngày trong quá khứ");

                string maDon = dal.GenerateMaDon(dto.MaDonVi);

                var don = new DonTranhThu
                {
                    MaDon = maDon,
                    MaQN = dto.MaQN,
                    CapBac = dto.CapBac,
                    ChucVu = dto.ChucVu,
                    MaDot = dto.MaDot,
                    LyDo = dto.LyDo,
                    TuNgay = dto.TuNgay,
                    DenNgay = dto.DenNgay,
                    NoiNghi = dto.NoiNghi,
                    MaVungMien = dto.MaVungMien,
                    NguoiTao = nguoiTao,
                    BuocHienTai = 1,
                    NgayNop = DateTime.Now
                };

                if (dal.Insert(don))
                {
                    var quaTrinh = new QuaTrinh_DuyetDon
                    {
                        MaDon = maDon,
                        BuocXuLy = 1,
                        MaNguoiXuLy = nguoiTao,
                        MaHanhDong = "DC",
                        GhiChu = "Tạo đơn mới",
                        ThoiGian = DateTime.Now
                    };
                    qtDAL.Insert(quaTrinh);

                    return (true, $"Tạo đơn thành công. Mã đơn: {maDon}");
                }

                return (false, "Không thể tạo đơn. Vui lòng thử lại");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool success, string message) DuyetDon(string maDon, string maNguoiDuyet,
            string maHanhDong, string ghiChu, string maNguoiNhanTiep = null)
        {
            try
            {
                var don = dal.GetByMaDon(maDon);
                if (don == null)
                    return (false, "Không tìm thấy đơn");

                if (maHanhDong == "DD")
                    don.BuocHienTai += 1;
                else if (maHanhDong == "TC")
                    don.BuocHienTai = 0;

                dal.Update(don);

                var quaTrinh = new QuaTrinh_DuyetDon
                {
                    MaDon = maDon,
                    BuocXuLy = don.BuocHienTai,
                    MaNguoiXuLy = maNguoiDuyet,
                    MaHanhDong = maHanhDong,
                    GhiChu = ghiChu,
                    ThoiGian = DateTime.Now,
                    MaNguoiNhanTiep = maNguoiNhanTiep
                };

                qtDAL.Insert(quaTrinh);

                string message = maHanhDong == "DD" ? "Duyệt đơn thành công" : "Từ chối đơn thành công";
                return (true, message);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool success, string message) XoaDon(string maDon)
        {
            try
            {
                if (dal.Delete(maDon))
                    return (true, "Xóa đơn thành công");
                return (false, "Không thể xóa đơn");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }
    }

    // BLL cho DotPhep
    public class DotPhepBLL
    {
        private DotPhepDAL dal = new DotPhepDAL();

        public List<DotPhepDTO> GetAll()
        {
            return dal.GetAll();
        }

        public List<DotPhepDTO> GetDotDangMo()
        {
            return dal.GetDotDangMo();
        }

        public (bool success, string message) TaoDot(DotPhepDTO dto, string nguoiTao)
        {
            try
            {
                if (dto.TuNgay >= dto.DenNgay)
                    return (false, "Ngày kết thúc phải sau ngày bắt đầu");

                var dot = new DotPhep
                {
                    MaDot = dto.MaDot,
                    MaLoaiPhep = dto.MaLoaiPhep,
                    TuNgay = dto.TuNgay,
                    DenNgay = dto.DenNgay,
                    NguoiTao = nguoiTao,
                    TrangThai = dto.TrangThai,
                    GhiChu = dto.GhiChu
                };

                if (dal.Insert(dot))
                    return (true, "Tạo đợt phép thành công");

                return (false, "Không thể tạo đợt phép");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool success, string message) CapNhatDot(DotPhepDTO dto)
        {
            try
            {
                var dot = dal.GetByMaDot(dto.MaDot);
                if (dot == null)
                    return (false, "Không tìm thấy đợt phép");

                dot.TuNgay = dto.TuNgay;
                dot.DenNgay = dto.DenNgay;
                dot.TrangThai = dto.TrangThai;
                dot.GhiChu = dto.GhiChu;

                if (dal.Update(dot))
                    return (true, "Cập nhật đợt phép thành công");

                return (false, "Không thể cập nhật đợt phép");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool success, string message) DongDot(string maDot)
        {
            try
            {
                var dot = dal.GetByMaDot(maDot);
                if (dot == null)
                    return (false, "Không tìm thấy đợt phép");

                dot.TrangThai = false;

                if (dal.Update(dot))
                    return (true, "Đóng đợt phép thành công");

                return (false, "Không thể đóng đợt phép");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }
    }

    // BLL cho QuaTrinh_DuyetDon
    public class QuaTrinh_DuyetDonBLL
    {
        private QuaTrinh_DuyetDonDAL dal = new QuaTrinh_DuyetDonDAL();

        public List<QuaTrinh_DuyetDonDTO> GetByMaDon(string maDon)
        {
            return dal.GetByMaDon(maDon);
        }
    }

    // BLL cho GiayPhep
    public class GiayPhepBLL
    {
        private GiayPhepDAL dal = new GiayPhepDAL();

        public List<GiayPhepDTO> GetAll()
        {
            return dal.GetAll();
        }

        public (bool success, string message, string soGiay) CapGiayPhep(string maDon,
            string maNguoiKy, string ghiChu)
        {
            try
            {
                string soGiay = dal.GenerateSoGiay();

                var gp = new GiayPhep
                {
                    SoGiay = soGiay,
                    MaDon = maDon,
                    NgayCap = DateTime.Now,
                    MaNguoiKy = maNguoiKy,
                    GhiChu = ghiChu
                };

                if (dal.Insert(gp))
                    return (true, "Cấp giấy phép thành công", soGiay);

                return (false, "Không thể cấp giấy phép", null);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", null);
            }
        }
    }

    // BLL cho TheoDoiNghiPhep
    public class TheoDoiNghiPhepBLL
    {
        private TheoDoiNghiPhepDAL dal = new TheoDoiNghiPhepDAL();

        public List<TheoDoiNghiPhepDTO> GetAll()
        {
            return dal.GetAll();
        }

        public (bool success, string message) XacNhanDiPhep(string soGiay, string maNguoiXacNhan)
        {
            try
            {
                if (dal.CapNhatDiPhep(soGiay, maNguoiXacNhan))
                    return (true, "Xác nhận đi phép thành công");

                return (false, "Không thể xác nhận đi phép");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public (bool success, string message) XacNhanTraPhep(int id, string ghiChu)
        {
            try
            {
                if (dal.CapNhatTraPhep(id, ghiChu))
                    return (true, "Xác nhận trả phép thành công");

                return (false, "Không thể xác nhận trả phép");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }
    }

    // BLL cho danh mục
    public class DanhMucBLL
    {
        private DanhMucDAL dal = new DanhMucDAL();

        public List<CapBac> GetCapBacs() => dal.GetCapBacs();
        public List<ChucVu> GetChucVus() => dal.GetChucVus();
        public List<LoaiPhep> GetLoaiPheps() => dal.GetLoaiPheps();
        public List<VungMien> GetVungMiens() => dal.GetVungMiens();
        public List<HanhDong_XuLy> GetHanhDongXuLys() => dal.GetHanhDongXuLys();
        public List<DonVi> GetDonVis() => dal.GetDonVis();
    }

    // BLL cho thống kê
    public class ThongKeBLL
    {
        public List<ThongKeDTO> ThongKeTheoDonVi(string maDot)
        {
            return new List<ThongKeDTO>();
        }

        public List<BaoCaoDTO> BaoCaoTheoThang(int thang, int nam)
        {
            return new List<BaoCaoDTO>();
        }
    }
}
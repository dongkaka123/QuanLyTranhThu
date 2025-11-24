// ===================================================
// File: DAL/DALClasses.cs - PHẦN 1
// ===================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DTO;

namespace DAL
{
    // DAL cho NguoiDung - QUAN TRỌNG
    public class NguoiDungDAL
    {
        private QuanLyTranhThuContext db = new QuanLyTranhThuContext();

        public UserSessionDTO Login(string tenDangNhap, string matKhau)
        {
            try
            {
                // Query không dùng Include để tránh lỗi
                var nguoiDung = db.NguoiDungs
                    .FirstOrDefault(n => n.TenDangNhap == tenDangNhap && n.MatKhauHash == matKhau);

                if (nguoiDung == null)
                    return null;

                // Lấy thông tin từng phần riêng biệt
                var quanNhan = db.QuanNhans.Find(nguoiDung.MaQN);
                if (quanNhan == null)
                    return null;

                var donVi = db.DonVis.Find(quanNhan.MaDonVi);
                var quyen = db.QuyenHeThongs.Find(nguoiDung.IDQuyenHT);

                return new UserSessionDTO
                {
                    TenDangNhap = nguoiDung.TenDangNhap,
                    MaQN = nguoiDung.MaQN,
                    HoTen = quanNhan.HoTen,
                    MaDonVi = quanNhan.MaDonVi,
                    TenDonVi = donVi?.TenDonVi ?? "N/A",
                    IDQuyenHT = nguoiDung.IDQuyenHT,
                    TenQuyen = quyen?.TenQuyen ?? "User"
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login DAL error: {ex.Message}");
                throw;
            }
        }

        public bool ChangePassword(string tenDangNhap, string matKhauCu, string matKhauMoi)
        {
            try
            {
                var user = db.NguoiDungs
                    .FirstOrDefault(n => n.TenDangNhap == tenDangNhap && n.MatKhauHash == matKhauCu);

                if (user != null)
                {
                    user.MatKhauHash = matKhauMoi;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    // DAL cho QuanNhan
    public class QuanNhanDAL
    {
        private QuanLyTranhThuContext db = new QuanLyTranhThuContext();

        public List<QuanNhanDTO> GetAll()
        {
            try
            {
                var list = new List<QuanNhanDTO>();
                var quanNhans = db.QuanNhans.ToList();

                foreach (var qn in quanNhans)
                {
                    var donVi = db.DonVis.Find(qn.MaDonVi);
                    var capBac = db.CapBacs.Find(qn.MaCapBac);
                    var chucVu = db.ChucVus.Find(qn.MaChucVu);

                    list.Add(new QuanNhanDTO
                    {
                        MaQN = qn.MaQN,
                        HoTen = qn.HoTen,
                        NgaySinh = qn.NgaySinh,
                        MaCapBac = qn.MaCapBac,
                        TenCapBac = capBac?.TenCapBac,
                        MaChucVu = qn.MaChucVu,
                        TenChucVu = chucVu?.TenChucVu,
                        MaDonVi = qn.MaDonVi,
                        TenDonVi = donVi?.TenDonVi,
                        SoDienThoai = qn.SoDienThoai
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAll QuanNhan error: {ex.Message}");
                return new List<QuanNhanDTO>();
            }
        }

        public QuanNhanDTO GetByMaQN(string maQN)
        {
            try
            {
                var qn = db.QuanNhans.Find(maQN);
                if (qn == null) return null;

                var donVi = db.DonVis.Find(qn.MaDonVi);
                var capBac = db.CapBacs.Find(qn.MaCapBac);
                var chucVu = db.ChucVus.Find(qn.MaChucVu);

                return new QuanNhanDTO
                {
                    MaQN = qn.MaQN,
                    HoTen = qn.HoTen,
                    NgaySinh = qn.NgaySinh,
                    MaCapBac = qn.MaCapBac,
                    TenCapBac = capBac?.TenCapBac,
                    MaChucVu = qn.MaChucVu,
                    TenChucVu = chucVu?.TenChucVu,
                    MaDonVi = qn.MaDonVi,
                    TenDonVi = donVi?.TenDonVi,
                    SoDienThoai = qn.SoDienThoai
                };
            }
            catch
            {
                return null;
            }
        }

        public List<QuanNhanDTO> GetByDonVi(string maDonVi)
        {
            try
            {
                var list = new List<QuanNhanDTO>();
                var quanNhans = db.QuanNhans.Where(q => q.MaDonVi == maDonVi).ToList();

                foreach (var qn in quanNhans)
                {
                    var capBac = db.CapBacs.Find(qn.MaCapBac);
                    var chucVu = db.ChucVus.Find(qn.MaChucVu);

                    list.Add(new QuanNhanDTO
                    {
                        MaQN = qn.MaQN,
                        HoTen = qn.HoTen,
                        TenCapBac = capBac?.TenCapBac,
                        TenChucVu = chucVu?.TenChucVu
                    });
                }

                return list;
            }
            catch
            {
                return new List<QuanNhanDTO>();
            }
        }
    }
    public class DonTranhThuDAL
    {
        private QuanLyTranhThuContext db = new QuanLyTranhThuContext();

        public List<DonTranhThuDTO> GetAll()
        {
            try
            {
                var list = new List<DonTranhThuDTO>();
                var dons = db.DonTranhThus.ToList();

                foreach (var don in dons)
                {
                    var qn = db.QuanNhans.Find(don.MaQN);
                    var vungMien = don.MaVungMien.HasValue ? db.VungMiens.Find(don.MaVungMien.Value) : null;

                    list.Add(new DonTranhThuDTO
                    {
                        MaDon = don.MaDon,
                        MaQN = don.MaQN,
                        HoTenQN = qn?.HoTen,
                        CapBac = don.CapBac,
                        ChucVu = don.ChucVu,
                        MaDot = don.MaDot,
                        LyDo = don.LyDo,
                        TuNgay = don.TuNgay,
                        DenNgay = don.DenNgay,
                        NoiNghi = don.NoiNghi,
                        MaVungMien = don.MaVungMien,
                        TenVungMien = vungMien?.TenVungMien,
                        NguoiTao = don.NguoiTao,
                        BuocHienTai = don.BuocHienTai,
                        NgayNop = don.NgayNop,
                        MaDonVi = qn?.MaDonVi
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAll DonTranhThu error: {ex.Message}");
                return new List<DonTranhThuDTO>();
            }
        }

        public List<DonTranhThuDTO> GetByMaQN(string maQN)
        {
            return GetAll().Where(d => d.MaQN == maQN).ToList();
        }

        public List<DonChoDuyetDTO> GetDonChoDuyet(string maDonVi)
        {
            try
            {
                var list = new List<DonChoDuyetDTO>();
                var dons = db.DonTranhThus.Where(d => d.BuocHienTai > 0).ToList();

                foreach (var don in dons)
                {
                    var qn = db.QuanNhans.Find(don.MaQN);
                    if (qn != null && qn.MaDonVi == maDonVi)
                    {
                        var donVi = db.DonVis.Find(qn.MaDonVi);
                        list.Add(new DonChoDuyetDTO
                        {
                            MaDon = don.MaDon,
                            HoTenQN = qn.HoTen,
                            DonVi = donVi?.TenDonVi,
                            TuNgay = don.TuNgay,
                            DenNgay = don.DenNgay,
                            SoNgay = (don.DenNgay - don.TuNgay).Days + 1,
                            NgayNop = don.NgayNop,
                            BuocHienTai = don.BuocHienTai,
                            LyDo = don.LyDo
                        });
                    }
                }

                return list;
            }
            catch
            {
                return new List<DonChoDuyetDTO>();
            }
        }

        public bool Insert(DonTranhThu don)
        {
            try
            {
                db.DonTranhThus.Add(don);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(DonTranhThu don)
        {
            try
            {
                var existing = db.DonTranhThus.Find(don.MaDon);
                if (existing != null)
                {
                    db.Entry(existing).CurrentValues.SetValues(don);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(string maDon)
        {
            try
            {
                var don = db.DonTranhThus.Find(maDon);
                if (don != null)
                {
                    db.DonTranhThus.Remove(don);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public DonTranhThu GetByMaDon(string maDon)
        {
            return db.DonTranhThus.Find(maDon);
        }

        public string GenerateMaDon(string maDonVi)
        {
            var lastDon = db.DonTranhThus
                .Where(d => d.MaDon.StartsWith("D"))
                .OrderByDescending(d => d.MaDon)
                .FirstOrDefault();

            int nextNumber = 1;
            if (lastDon != null)
            {
                string lastNumber = lastDon.MaDon.Substring(lastDon.MaDon.Length - 3);
                int.TryParse(lastNumber, out nextNumber);
                nextNumber++;
            }

            return $"D{DateTime.Now.ToString("yy")}01{maDonVi}{nextNumber:000}";
        }
    }

    // DAL cho DotPhep
    public class DotPhepDAL
    {
        private QuanLyTranhThuContext db = new QuanLyTranhThuContext();

        public List<DotPhepDTO> GetAll()
        {
            try
            {
                var list = new List<DotPhepDTO>();
                var dots = db.DotPheps.ToList();

                foreach (var dot in dots)
                {
                    var loaiPhep = db.LoaiPheps.Find(dot.MaLoaiPhep);

                    list.Add(new DotPhepDTO
                    {
                        MaDot = dot.MaDot,
                        MaLoaiPhep = dot.MaLoaiPhep,
                        TenLoaiPhep = loaiPhep?.TenLoai,
                        TuNgay = dot.TuNgay,
                        DenNgay = dot.DenNgay,
                        NguoiTao = dot.NguoiTao,
                        TrangThai = dot.TrangThai,
                        GhiChu = dot.GhiChu
                    });
                }

                return list;
            }
            catch
            {
                return new List<DotPhepDTO>();
            }
        }

        public List<DotPhepDTO> GetDotDangMo()
        {
            return GetAll().Where(d => d.TrangThai == true).ToList();
        }

        public bool Insert(DotPhep dot)
        {
            try
            {
                db.DotPheps.Add(dot);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(DotPhep dot)
        {
            try
            {
                var existing = db.DotPheps.Find(dot.MaDot);
                if (existing != null)
                {
                    db.Entry(existing).CurrentValues.SetValues(dot);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public DotPhep GetByMaDot(string maDot)
        {
            return db.DotPheps.Find(maDot);
        }
    }
    public class QuaTrinh_DuyetDonDAL
    {
        private QuanLyTranhThuContext db = new QuanLyTranhThuContext();

        public List<QuaTrinh_DuyetDonDTO> GetByMaDon(string maDon)
        {
            try
            {
                var list = new List<QuaTrinh_DuyetDonDTO>();
                var quaTrinhs = db.QuaTrinh_DuyetDons
                    .Where(q => q.MaDon == maDon)
                    .OrderBy(q => q.ThoiGian)
                    .ToList();

                foreach (var qt in quaTrinhs)
                {
                    var nguoiXuLy = db.QuanNhans.Find(qt.MaNguoiXuLy);
                    var hanhDong = db.HanhDong_XuLys.Find(qt.MaHanhDong);

                    list.Add(new QuaTrinh_DuyetDonDTO
                    {
                        MaXuLy = qt.MaXuLy,
                        MaDon = qt.MaDon,
                        BuocXuLy = qt.BuocXuLy,
                        MaNguoiXuLy = qt.MaNguoiXuLy,
                        TenNguoiXuLy = nguoiXuLy?.HoTen,
                        MaHanhDong = qt.MaHanhDong,
                        TenHanhDong = hanhDong?.TenHanhDong,
                        GhiChu = qt.GhiChu,
                        ThoiGian = qt.ThoiGian,
                        MaNguoiNhanTiep = qt.MaNguoiNhanTiep
                    });
                }

                return list;
            }
            catch
            {
                return new List<QuaTrinh_DuyetDonDTO>();
            }
        }

        public bool Insert(QuaTrinh_DuyetDon qt)
        {
            try
            {
                db.QuaTrinh_DuyetDons.Add(qt);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    // DAL cho GiayPhep
    public class GiayPhepDAL
    {
        private QuanLyTranhThuContext db = new QuanLyTranhThuContext();

        public List<GiayPhepDTO> GetAll()
        {
            try
            {
                var list = new List<GiayPhepDTO>();
                var giayPheps = db.GiayPheps.ToList();

                foreach (var gp in giayPheps)
                {
                    var don = db.DonTranhThus.Find(gp.MaDon);
                    var nguoiKy = db.QuanNhans.Find(gp.MaNguoiKy);
                    var quanNhan = don != null ? db.QuanNhans.Find(don.MaQN) : null;

                    list.Add(new GiayPhepDTO
                    {
                        SoGiay = gp.SoGiay,
                        MaDon = gp.MaDon,
                        HoTenQN = quanNhan?.HoTen,
                        NgayCap = gp.NgayCap,
                        MaNguoiKy = gp.MaNguoiKy,
                        TenNguoiKy = nguoiKy?.HoTen,
                        GhiChu = gp.GhiChu
                    });
                }

                return list;
            }
            catch
            {
                return new List<GiayPhepDTO>();
            }
        }

        public bool Insert(GiayPhep gp)
        {
            try
            {
                db.GiayPheps.Add(gp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GenerateSoGiay()
        {
            var lastGP = db.GiayPheps
                .OrderByDescending(g => g.SoGiay)
                .FirstOrDefault();

            int nextNumber = 1;
            if (lastGP != null && int.TryParse(lastGP.SoGiay, out int num))
            {
                nextNumber = num + 1;
            }

            return nextNumber.ToString("0000");
        }
    }

    // DAL cho TheoDoiNghiPhep
    public class TheoDoiNghiPhepDAL
    {
        private QuanLyTranhThuContext db = new QuanLyTranhThuContext();

        public List<TheoDoiNghiPhepDTO> GetAll()
        {
            try
            {
                var list = new List<TheoDoiNghiPhepDTO>();
                var theoDoiList = db.TheoDoiNghiPheps.ToList();

                foreach (var td in theoDoiList)
                {
                    var giayPhep = db.GiayPheps.Find(td.SoGiay);
                    var don = giayPhep != null ? db.DonTranhThus.Find(giayPhep.MaDon) : null;
                    var quanNhan = don != null ? db.QuanNhans.Find(don.MaQN) : null;

                    list.Add(new TheoDoiNghiPhepDTO
                    {
                        ID = td.ID,
                        SoGiay = td.SoGiay,
                        HoTenQN = quanNhan?.HoTen,
                        NgayDiPhep = td.NgayDiPhep,
                        NgayTraPhep = td.NgayTraPhep,
                        GhiChu = td.GhiChu,
                        MaNguoiXacNhan = td.MaNguoiXacNhan
                    });
                }

                return list;
            }
            catch
            {
                return new List<TheoDoiNghiPhepDTO>();
            }
        }

        public bool CapNhatDiPhep(string soGiay, string maNguoiXacNhan)
        {
            try
            {
                var record = new TheoDoiNghiPhep
                {
                    SoGiay = soGiay,
                    NgayDiPhep = DateTime.Now,
                    MaNguoiXacNhan = maNguoiXacNhan
                };
                db.TheoDoiNghiPheps.Add(record);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CapNhatTraPhep(int id, string ghiChu)
        {
            try
            {
                var record = db.TheoDoiNghiPheps.Find(id);
                if (record != null)
                {
                    record.NgayTraPhep = DateTime.Now;
                    record.GhiChu = ghiChu;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    // DAL cho danh mục
    public class DanhMucDAL
    {
        private QuanLyTranhThuContext db = new QuanLyTranhThuContext();

        public List<CapBac> GetCapBacs() => db.CapBacs.ToList();
        public List<ChucVu> GetChucVus() => db.ChucVus.ToList();
        public List<LoaiPhep> GetLoaiPheps() => db.LoaiPheps.ToList();
        public List<VungMien> GetVungMiens() => db.VungMiens.ToList();
        public List<HanhDong_XuLy> GetHanhDongXuLys() => db.HanhDong_XuLys.ToList();
        public List<DonVi> GetDonVis() => db.DonVis.ToList();
    }
}
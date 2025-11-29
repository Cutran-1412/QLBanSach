using Microsoft.AspNetCore.Mvc;
using QLBanSach.Data.ChiTietDonHangRepository;
using QLBanSach.Data.ChiTietGioHangRepository;
using QLBanSach.Data.DonHangRepository;
using QLBanSach.Data.GioHangRepository;
using QLBanSach.Data.NguoiDungRepository;
using QLBanSach.Data.SachRepository;
using QLBanSach.Data.TheLoaiRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class AdminController : Controller
    {
        private readonly IGioHangRepository _gioHangRepository;
        private readonly IChiTietGioHangRepository _chiTietGioHangRepository;
        private readonly IDonHangRepository _donHangRepository;
        private readonly IChiTietDonHangRepository _chiTietDonHangRepository;
        private readonly ISachRepository _sachRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly ITheLoaiRepository _theLoaiRepository;

        public AdminController(IGioHangRepository gioHangRepository, IChiTietGioHangRepository chiTietGioHangRepository, IDonHangRepository donHangRepository, IChiTietDonHangRepository chiTietDonHangRepository, ISachRepository sachRepository, INguoiDungRepository nguoiDungRepository, ITheLoaiRepository theLoaiRepository)
        {
            _gioHangRepository = gioHangRepository;
            _chiTietGioHangRepository = chiTietGioHangRepository;
            _donHangRepository = donHangRepository;
            _chiTietDonHangRepository = chiTietDonHangRepository;
            _sachRepository = sachRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _theLoaiRepository = theLoaiRepository;
        }
        private void DemNguoi()
        {
            var NguoiDung = _nguoiDungRepository.GetAll();
            int admin = 0;
            int nguoidung = 0;
            foreach(var Nguoi in NguoiDung)
            {
                if (Nguoi.VaiTro)
                {
                    admin++;
                }
                else
                {
                    nguoidung++;
                }
            }
            ViewBag.TongNguoi = admin + nguoidung;
            ViewBag.Admin = admin;
            ViewBag.Nguoi = nguoidung;
        }
        private void DemSach()
        {
            var sach = _sachRepository.GetAll();
            int soluongdausach = sach.Count();
            int soluongsach = sach.Sum(s => s.SoLuong);
            ViewBag.SLDauSach = soluongdausach;
            ViewBag.SLSach = soluongsach;
        }
        private void DemDonHang()
        {
            var donhang = _donHangRepository.GetAll() ?? new List<DonHang>();

            ViewBag.DaDat = donhang.Count(d => d.TrangThai == "Đã đặt hàng");
            ViewBag.DangGiao = donhang.Count(d => d.TrangThai == "Đang giao hàng");
            ViewBag.DaNhan = donhang.Count(d => d.TrangThai == "Đã nhận hàng");
            ViewBag.Huy = donhang.Count(d => d.TrangThai == "Đã hủy");
            ViewBag.TongDon = donhang.Count();
            ViewBag.DaDat ??= 0;
            ViewBag.DangGiao ??= 0;
            ViewBag.DaNhan ??= 0;
            ViewBag.Huy ??= 0;
            ViewBag.TongDon ??= 0;
        }
        private void TinhTongDoanhThu()
        {
            var donhang = _donHangRepository.GetAll() ?? new List<DonHang>();

            var donDaNhan = donhang
                .Where(d => d.TrangThai == "Đã nhận hàng")
                .ToList();
            decimal tongDoanhThu = donDaNhan.Sum(d => d.TongTien);

            ViewBag.TongDoanhThu = tongDoanhThu;
        }
        private void DonHangHomNayDaDat()
        {
            var today = DateTime.Today;

            var donhang = _donHangRepository.GetAll() ?? new List<DonHang>();

            int daDatHomNay = donhang
                .Where(d => d.TrangThai == "Đã đặt hàng"
                         && d.NgayDat.Date == today)
                .Count();

            ViewBag.DaDatHomNay = daDatHomNay;
        }
        private void DonMoiNhat()
        {
            ViewBag.Top3Don = _donHangRepository.GetTop3DonMoiNhat();
        }
        public IActionResult Index()
        {
            DemNguoi();
            DemSach();
            DemDonHang();
            TinhTongDoanhThu();
            DonHangHomNayDaDat();
            DonMoiNhat();
            return View();
        }
    }
}

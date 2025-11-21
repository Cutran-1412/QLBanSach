using Microsoft.AspNetCore.Mvc;
using QLBanSach.Data.ChiTietDonHangRepository;
using QLBanSach.Data.ChiTietGioHangRepository;
using QLBanSach.Data.DonHangRepository;
using QLBanSach.Data.GioHangRepository;
using QLBanSach.Data.SachRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class DonHangController : Controller
    {
        private readonly IGioHangRepository _gioHangRepository;
        private readonly IChiTietGioHangRepository _chiTietGioHangRepository;
        private readonly IDonHangRepository _donHangRepository;
        private readonly IChiTietDonHangRepository _chiTietDonHangRepository;
        private readonly ISacRepository _sacRepository;

        public DonHangController(IGioHangRepository gioHangRepository, IChiTietGioHangRepository chiTietGioHangRepository, IDonHangRepository donHangRepository, IChiTietDonHangRepository chiTietDonHangRepository, ISacRepository sacRepository)
        {
            _gioHangRepository = gioHangRepository;
            _chiTietGioHangRepository = chiTietGioHangRepository;
            _donHangRepository = donHangRepository;
            _chiTietDonHangRepository = chiTietDonHangRepository;
            _sacRepository = sacRepository;
        }
        private string GenerateMaDonHang()
        {
            var lastOrder = _donHangRepository.GetAll()
                .OrderByDescending(d => d.MaDonHang)
                .FirstOrDefault();
            if (lastOrder == null)
                return "DH00001";
            string lastCode = lastOrder.MaDonHang.Replace("DH", "");
            int number = int.Parse(lastCode) + 1;
            return "DH" + number.ToString("D5");
        }
        private string GenerateMaChiTietDonHang()
        {
            var lastDetail = _chiTietDonHangRepository.GetAll()
                .OrderByDescending(c => c.MaChiTiet)
                .FirstOrDefault();

            if (lastDetail == null)
                return "CTDH000001";

            string lastCode = lastDetail.MaChiTiet.Replace("CTDH", "");

            int number = int.Parse(lastCode) + 1;

            return "CTDH" + number.ToString("D6");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DatHang(string maGioHang)
        {
            var DonHang = new DonHang();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DatHang(DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                //_donHangRepo.Add(donHang);
                //_donHangRepo.Save();
                //return RedirectToAction("Index", "Home"); // hoặc trang xác nhận
            }
            return View(donHang);
        }
    }
}

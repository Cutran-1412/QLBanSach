using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data.ChiTietDonHangRepository;
using QLBanSach.Data.ChiTietGioHangRepository;
using QLBanSach.Data.DonHangRepository;
using QLBanSach.Data.GioHangRepository;
using QLBanSach.Data.SachRepository;
using QLBanSach.Models;
using System.Security.Claims;

namespace QLBanSach.Controllers
{
    public class GioHangController : Controller
    {
        private readonly IGioHangRepository _gioHangRepository;
        private readonly IChiTietGioHangRepository _chiTietGioHangRepository;
        private readonly IDonHangRepository _donHangRepository;
        private readonly IChiTietDonHangRepository _chiTietDonHangRepository;
        private readonly ISacRepository _sachRepository;

        public GioHangController(IGioHangRepository gioHangRepository, IChiTietGioHangRepository chiTietGioHangRepository, IDonHangRepository donHangRepository, IChiTietDonHangRepository chiTietDonHangRepository, ISacRepository sachRepository)
        {
            _gioHangRepository = gioHangRepository;
            _chiTietGioHangRepository = chiTietGioHangRepository;
            _donHangRepository = donHangRepository;
            _chiTietDonHangRepository = chiTietDonHangRepository;
            _sachRepository = sachRepository;
        }

        private string GenerateMaGioHang()
        {
            var all = _gioHangRepository.GetData();
            if (all == null || !all.Any())
                return "GH001";
            int maxNumber = all
                .Select(x =>
                {
                    var code = x.MaGioHang?.Trim();
                    if (string.IsNullOrEmpty(code) || code.Length < 3)
                        return 0;
                    var numPart = code.Substring(2);
                    return int.TryParse(numPart, out int num) ? num : 0;
                })
                .Max();
            maxNumber++;
            return "GH" + maxNumber.ToString("D3");
        }
        public IActionResult Index(string id)
        {
            GioHang giohang = _gioHangRepository.GetByIDND(id);
            if (giohang == null)
            {
                giohang = new GioHang
                {
                    MaGioHang = GenerateMaGioHang(),
                    MaNguoiDung = id,
                    NgayTao = DateTime.Now,
                };
                _gioHangRepository.Add(giohang);
                _gioHangRepository.Save();
            }
            ViewBag.GioHang = giohang;
            var chitietgiohang = _chiTietGioHangRepository.GetByIDGioHang(giohang.MaGioHang).Select(ct => new ChiTietGioHang
            {
                MaChiTiet = ct.MaChiTiet,
                MaGioHang = ct.MaGioHang,
                SoLuong = ct.SoLuong,
                MaSach = ct.MaSach,
                Sach = _sachRepository.GetById(ct.MaSach)
            }).ToList();
            return View(chitietgiohang);
        }
        private string GenerateMaChiTietGioHang()
        {
            var all = _chiTietGioHangRepository.GetAll();
            if (all == null || !all.Any())
                return "CTDH00001";
            int maxNumber = all
                .Select(x =>
                {
                    var code = x.MaChiTiet?.Trim();

                    if (string.IsNullOrEmpty(code) || code.Length < 5)
                        return 0;

                    var numPart = code.Substring(4);

                    return int.TryParse(numPart, out int num) ? num : 0;
                })
                .Max();
            maxNumber++;
            return "CTDH" + maxNumber.ToString("D5");
        }
        public IActionResult InsertGioHang(string id, string masach)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Message"] = "Vui lòng đăng nhập để thêm vào giỏ hàng";
                TempData["MesseType"] = "error";
                return RedirectToAction("Login", "Account");
            }
            GioHang giohang = _gioHangRepository.GetByIDND(id);
            if (giohang == null)
            {
                giohang = new GioHang
                {
                    MaGioHang = GenerateMaGioHang(),
                    MaNguoiDung = id,
                    NgayTao = DateTime.Now,
                };
                _gioHangRepository.Add(giohang);
                _gioHangRepository.Save();
            }
            var checksach = _chiTietGioHangRepository.CheckSach(masach);
            if (checksach != null)
            {
                TempData["Message"] = "Sản phẩm đã có trong giỏ hàng";
                TempData["MesseType"] = "error";
                return RedirectToAction("Index", "Home");
            }
            var sach = _sachRepository.GetById(masach);
            if (sach.SoLuong == 0)
            {
                TempData["Message"] = "Sản phẩm tạm Hết hàng";
                TempData["MesseType"] = "error";
                return RedirectToAction("Index", "Home");
            }

            var chiTiet = new ChiTietGioHang
            {
                MaChiTiet = GenerateMaChiTietGioHang(),
                MaGioHang = giohang.MaGioHang,
                MaSach = masach,
                SoLuong = 1
            };
            _chiTietGioHangRepository.Add(chiTiet);
            _chiTietGioHangRepository.Save();
            TempData["Message"] = "Thêm sản phẩn thành công";
            TempData["MesseType"] = "success";
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateSoLuong(string maChiTiet, int soLuong)
        {
            try
            {
                _chiTietGioHangRepository.CapNhatSoLuong(maChiTiet, soLuong);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RemoveChiTiet(string maChiTiet)
        {
            try
            {
                var chiTiet = _chiTietGioHangRepository.GetById(maChiTiet);
                if (chiTiet != null)
                {
                    _chiTietGioHangRepository.Delete(chiTiet);
                    _chiTietGioHangRepository.Save();
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    } 
}

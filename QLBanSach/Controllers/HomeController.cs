using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QLBanSach.Data.SachRepository;
using QLBanSach.Data.TheLoaiRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISacRepository _sacRepository;
        private readonly ITheLoaiRepository _theLoaiRepository;

        public HomeController(ISacRepository sacRepository, ITheLoaiRepository theLoaiRepository)
        {
            _theLoaiRepository = theLoaiRepository;
            _sacRepository = sacRepository;
        }


        public IActionResult Index(string? MaTheLoai, string? TenSach)
        {
            var sach = _sacRepository.GetAll();
            var theloai = _theLoaiRepository.GetAll();
            if (MaTheLoai != null)
            {
                sach = _sacRepository.GetByTl(MaTheLoai);
            }
            if (TenSach != null)
            {
                sach = _sacRepository.GetByName(TenSach);
            }
            ViewBag.TheLoai = theloai;
            ViewBag.Sach = sach;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ChiTietSach(string masach)
        {
            var sach = _sacRepository.GetById(masach);
            return View(sach);
        }
    }
}

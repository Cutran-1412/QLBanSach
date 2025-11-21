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


        public IActionResult Index(string? MaTheLoai)
        {
            ViewBag.Sach= _sacRepository.GetAll();
            ViewBag.TheLoai = _theLoaiRepository.GetAll();
            if (MaTheLoai != null)
            {
                ViewBag.Sach = _sacRepository.GetByTl(MaTheLoai);
                ViewBag.TheLoai = _theLoaiRepository.GetAll();
            }
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

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QLBanSach.Data.ChiTietDonHangRepository;
using QLBanSach.Data.SachRepository;
using QLBanSach.Data.TheLoaiRepository;
using QLBanSach.Models;
using X.PagedList;

namespace QLBanSach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISachRepository _sacRepository;
        private readonly ITheLoaiRepository _theLoaiRepository;

        public HomeController(ISachRepository sacRepository, ITheLoaiRepository theLoaiRepository)
        {
            _sacRepository = sacRepository;
            _theLoaiRepository = theLoaiRepository;
        }

        public IActionResult Index(string? MaTheLoai,string? TenSach,int? GiaTu,int? GiaDen,string? TacGia,string? NhaXuatBan,string? SapXep,int page = 1)
        {
            var sach = _sacRepository.GetAll().AsQueryable();
            var theloai = _theLoaiRepository.GetAll();
            if (!string.IsNullOrEmpty(MaTheLoai))
                sach = sach.Where(x => x.MaTheLoai == MaTheLoai);
            if (!string.IsNullOrEmpty(TenSach))
                sach = sach.Where(x => x.TenSach.Contains(TenSach));
            if (GiaTu.HasValue)
                sach = sach.Where(x => x.Gia >= GiaTu.Value);
            if (GiaDen.HasValue)
                sach = sach.Where(x => x.Gia <= GiaDen.Value);
            if (!string.IsNullOrEmpty(TacGia))
                sach = sach.Where(x => x.TacGia.Contains(TacGia));
            if (!string.IsNullOrEmpty(NhaXuatBan))
                sach = sach.Where(x => x.NhaXuatBan.Contains(NhaXuatBan));
            if (!string.IsNullOrEmpty(SapXep))
            {
                sach = SapXep.ToLower() switch
                {
                    "asc" => sach.OrderBy(x => x.Gia),
                    "desc" => sach.OrderByDescending(x => x.Gia),
                    _ => sach
                };
            }
            int pageSize = 8;
            var pagedSach = sach.ToPagedList(page, pageSize);
            ViewBag.TheLoai = theloai;
            ViewBag.Search = new
            {
                MaTheLoai,
                TenSach,
                GiaTu,
                GiaDen,
                TacGia,
                NhaXuatBan,
                SapXep
            };

            return View(pagedSach);
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

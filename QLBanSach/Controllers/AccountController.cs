using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data.NguoiDungRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class AccountController : Controller
    {
        private readonly INguoiDungRepository _nguoiDungRepository;

        public AccountController(INguoiDungRepository nguoiDungRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var Check = _nguoiDungRepository.LogIn(username, password);

            if (Check)
            {
                var nguoiDung = _nguoiDungRepository.GetByName(username);
                HttpContext.Session.SetString("UserName", nguoiDung.HoTen);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string GenerateMaNguoiDung()
        {
            var lastUser = _nguoiDungRepository.GetAll()
                .OrderByDescending(u => u.MaNguoiDung)
                .FirstOrDefault();

            if (lastUser == null)
                return "ND01";

            int number = int.Parse(lastUser.MaNguoiDung.Substring(2));
            number++;

            return "ND" + number.ToString("D3");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NguoiDung user)
        {
            var existingUser = _nguoiDungRepository.GetAll().FirstOrDefault(u =>
                u.TaiKhoan == user.TaiKhoan);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "Tên đăng nhập đã được sử dụng!");
                return View(user);
            }

            user.MaNguoiDung = GenerateMaNguoiDung();
            _nguoiDungRepository.Add(user);
            _nguoiDungRepository.Save();

            TempData["Message"] = "Đăng ký thành công!";
            return RedirectToAction("Login");
        }
    }
}

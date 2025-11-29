using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data.DonHangRepository;
using QLBanSach.Data.GioHangRepository;
using QLBanSach.Data.NguoiDungRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class AccountController : Controller
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IDonHangRepository _donHangRepository;

        public AccountController(INguoiDungRepository nguoiDungRepository, IDonHangRepository donHangRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _donHangRepository = donHangRepository;
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
                HttpContext.Session.SetString("UserId",nguoiDung.MaNguoiDung);
                HttpContext.Session.SetString("VaiTro", nguoiDung.VaiTro.ToString());
                TempData["Message"] = "Xin chào "+nguoiDung.HoTen;
                TempData["MesseType"] = "success";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Bạn đã đăng xuất";
            TempData["MesseType"] = "error";
            return RedirectToAction("Login");
        }

        private string GenerateMaNguoiDung()
        {
            var lastUser = _nguoiDungRepository.GetAll()
            .Where(u => u.MaNguoiDung != null && u.MaNguoiDung.StartsWith("ND"))
            .Select(u =>
            {
                int num;
                return int.TryParse(u.MaNguoiDung.Substring(2), out num) ? num : 0;
            })
            .OrderByDescending(num => num)
            .FirstOrDefault();
            int nextNumber = lastUser + 1;
            return $"ND{nextNumber:00}";
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
                TempData["Message"] = "Tài khoản trùng ";
                TempData["MessageType"] = "warning";
                return View(user);
            }
            if (_nguoiDungRepository.EmailExists(user.Email))
            {
                TempData["Message"] = "Email bị trùng ";
                TempData["MessageType"] = "warning";
                return View(user);
            }
            TempData["Message"] = "Đăng ký thành công!";
            TempData["MessageType"] = "success";
            user.MaNguoiDung = GenerateMaNguoiDung();
            _nguoiDungRepository.Add(user);
            _nguoiDungRepository.Save();
            return RedirectToAction("Login");
        }
        public IActionResult Details(string id)
        {
            var nguoidung = _nguoiDungRepository.GetById(id);
            return View(nguoidung);
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["Message"] = "Vui lòng kiểm tra email để nhận mật khẩu mới!";
                TempData["MessageType"] = "warning";
                return View();
            }

            var user = _nguoiDungRepository.GetAll()
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                TempData["Message"] = "Vui lòng kiểm tra email không tồn tại!";
                TempData["MessageType"] = "warning";
                return View();
            }
            string newPassword = "123456";
            user.MatKhau = newPassword;
            _nguoiDungRepository.Save();
            TempData["Message"] = "Mật khẩu mới đã được gửi đến Email!";
            TempData["MessageType"] = "success";
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = _nguoiDungRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NguoiDung user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var existingUser = _nguoiDungRepository.GetById(user.MaNguoiDung);
            if (existingUser == null)
            {
                TempData["Message"] = "Người dùng không tồn tại!";
                TempData["MesseType"] = "error";
                return RedirectToAction("Index");
            }
            existingUser.TaiKhoan = user.TaiKhoan;
            existingUser.MatKhau = user.MatKhau;
            existingUser.HoTen = user.HoTen;
            existingUser.Email = user.Email;
            existingUser.SoDienThoai = user.SoDienThoai;
            existingUser.DiaChi = user.DiaChi;
            existingUser.VaiTro = user.VaiTro;

            _nguoiDungRepository.Save();

            TempData["Message"] = "Cập nhật thành công!";
            TempData["MesseType"] = "success";

            return RedirectToAction("Details", new { id = user.MaNguoiDung });
        }

    }
}

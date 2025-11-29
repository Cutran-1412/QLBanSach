using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data;
using QLBanSach.Data.SachRepository;
using QLBanSach.Data.TheLoaiRepository;
using QLBanSach.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace QLBanSach.Controllers
{
    public class SachesController : Controller
    {
        private readonly ISachRepository _sachRepository;
        private readonly ITheLoaiRepository _theLoaiRepository;

        public SachesController(ISachRepository sachRepository, ITheLoaiRepository theLoaiRepository)
        {
            _sachRepository = sachRepository;
            _theLoaiRepository = theLoaiRepository;
        }

        // GET: Saches
        public IActionResult Index(string? maSach,string? tenSach, string? maTheLoai,string? soLuongStatus,string? sortOrder,int page = 1,int pageSize = 10)
        {
            var sach = _sachRepository.GetAll().AsQueryable();
            ViewBag.SortList = new List<SelectListItem>
            {
                new SelectListItem { Text="Mặc định", Value="" },
                new SelectListItem { Text="Tăng dần", Value="gia_asc" },
                new SelectListItem { Text="Giảm dần", Value="gia_desc" }
            };
            if (!string.IsNullOrEmpty(maSach))
                sach = sach.Where(s => s.MaSach.Contains(maSach));
            if (!string.IsNullOrEmpty(tenSach))
                sach = sach.Where(s => s.TenSach.Contains(tenSach));
            if (!string.IsNullOrEmpty(maTheLoai))
                sach = sach.Where(s => s.MaTheLoai == maTheLoai);
            if (!string.IsNullOrEmpty(soLuongStatus))
            {
                if (soLuongStatus == "con")
                    sach = sach.Where(s => s.SoLuong > 0);
                else if (soLuongStatus == "het")
                    sach = sach.Where(s => s.SoLuong == 0);
            }
            switch (sortOrder)
            {
                case "gia_asc":
                    sach = sach.OrderBy(s => s.Gia);
                    break;

                case "gia_desc":
                    sach = sach.OrderByDescending(s => s.Gia);
                    break;

                default:
                    sach = sach.OrderBy(s => s.TenSach);
                    break;
            }
            ViewBag.MaSach = maSach;
            ViewBag.TenSach = tenSach;
            ViewBag.MaTheLoai = maTheLoai;
            ViewBag.SoLuongStatus = soLuongStatus;
            ViewBag.SortOrder = sortOrder;
            var theloai = _theLoaiRepository.GetAll();
            ViewBag.TheLoaiList = new SelectList(theloai, "MaTheLoai", "TenTheLoai", maTheLoai);
            var pagedList = sach.ToPagedList(page, pageSize);
            return View(pagedList);
        }


        // GET: Saches/Details/5
        public IActionResult Details(string id)
        {
            var sach = _sachRepository.GetById(id);
            if (sach == null)
            {
                return NotFound();
            }
            var theloai = _theLoaiRepository.GetAll();
            ViewData["MaTheLoai"] = new SelectList(theloai, "MaTheLoai", "MaTheLoai");
            return View(sach);
        }

        // GET: Saches/Create
        public IActionResult Create()
        {
            var theloai = _theLoaiRepository.GetAll();
            ViewData["MaTheLoai"] = new SelectList(theloai, "MaTheLoai", "MaTheLoai");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? AnhFile,[Bind("MaSach,TenSach,TacGia,NhaXuatBan,NamXuatBan,Gia,SoLuong,MoTa,MaTheLoai")]Sach sach)
        {
            if (_sachRepository.GetById(sach.MaSach) != null)
            {
                TempData["Message"] = "Bị trùng mã sách!";
                TempData["MessageType"] = "warning";
                var theloai = _theLoaiRepository.GetAll();
                ViewData["MaTheLoai"] = new SelectList(theloai, "MaTheLoai", "MaTheLoai");
                return View();
            }
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            if (AnhFile != null && AnhFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(AnhFile.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AnhFile.CopyToAsync(stream);
                }

                sach.Anh = fileName;
            }
            else
            {
                sach.Anh = "no_image.png";
            }
            TempData["Message"] = "Thêm thành công sách " + sach.TenSach + " !";
            TempData["MessageType"] = "success";
            _sachRepository.Add(sach);
            _sachRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Saches/Edit/5
        public IActionResult Edit(string id)
        {
            var sach = _sachRepository.GetById(id);
            if (sach == null)
            {
                return NotFound();
            }
            var theloai = _theLoaiRepository.GetAll();
            ViewData["MaTheLoai"] = new SelectList(theloai, "MaTheLoai", "MaTheLoai");
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, IFormFile AnhFile, Sach sach)
        {
            if (id != sach.MaSach)
                return NotFound();

            // Lấy sách từ DB
            var sachDb = _sachRepository.GetById(sach.MaSach);
            if (sachDb == null)
                return NotFound();

            // Cập nhật các trường
            sachDb.TenSach = sach.TenSach;
            sachDb.TacGia = sach.TacGia;
            sachDb.NhaXuatBan = sach.NhaXuatBan;
            sachDb.NamXuatBan = sach.NamXuatBan;
            sachDb.SoLuong = sach.SoLuong;
            sachDb.Gia = sach.Gia;
            sachDb.MoTa = sach.MoTa;

            // Nếu có ảnh mới thì xử lý ảnh
            if (AnhFile != null && AnhFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(AnhFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    AnhFile.CopyToAsync(stream);
                }

                sachDb.Anh = fileName;
            }
            TempData["Message"] = "Thêm thành công sách có mã" + sach.MaSach + " !";
            TempData["MessageType"] = "success";
            _sachRepository.Update(sachDb);
            _sachRepository.Save();
            return RedirectToAction(nameof(Index));
        }




        // GET: Saches/Delete/5
        public IActionResult Delete(string id)
        {
            var sach = _sachRepository.GetById(id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var sach = _sachRepository.GetById(id);
            if (sach != null)
            {
                // XÓA ẢNH trong wwwroot/Images
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                if (!string.IsNullOrEmpty(sach.Anh) && sach.Anh != "no_image.png")
                {
                    string imgPath = Path.Combine(uploadPath, sach.Anh);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                }

                TempData["Message"] = "Xoá thành công mã " + sach.MaSach + " !";
                TempData["MessageType"] = "error";
                _sachRepository.Delete(sach);
                _sachRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

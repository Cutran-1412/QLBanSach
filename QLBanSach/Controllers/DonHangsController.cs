using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data;
using QLBanSach.Data.ChiTietDonHangRepository;
using QLBanSach.Data.DonHangRepository;
using QLBanSach.Data.NguoiDungRepository;
using QLBanSach.Data.SachRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class DonHangsController : Controller
    {
        private readonly IDonHangRepository _donHangRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly ISachRepository _sachRepository;
        private readonly IChiTietDonHangRepository _chiTietDonHangRepository;

        public DonHangsController(IDonHangRepository donHangRepository, INguoiDungRepository nguoiDungRepository, ISachRepository sachRepository, IChiTietDonHangRepository chiTietDonHangRepository)
        {
            _donHangRepository = donHangRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _sachRepository = sachRepository;
            _chiTietDonHangRepository = chiTietDonHangRepository;
        }



        // GET: DonHangs
        public IActionResult Index(string? maNguoiDung, string? trangThai, DateTime? tuNgay, DateTime? denNgay, int page = 1, int pageSize = 10)
        {
            ViewBag.NguoiDung = _nguoiDungRepository.GetAll();

            var donhang = _donHangRepository.GetAll().AsQueryable();

            // Lọc theo mã người dùng
            if (!string.IsNullOrWhiteSpace(maNguoiDung))
                donhang = donhang.Where(x => x.MaNguoiDung.Contains(maNguoiDung));

            // Lọc theo trạng thái
            if (!string.IsNullOrWhiteSpace(trangThai))
                donhang = donhang.Where(x => x.TrangThai == trangThai);

            // Lọc theo ngày đặt hàng
            if (tuNgay.HasValue)
                donhang = donhang.Where(x => x.NgayDat >= tuNgay.Value.Date);

            if (denNgay.HasValue)
                donhang = donhang.Where(x => x.NgayDat <= denNgay.Value.Date.AddDays(1).AddTicks(-1));

            // Sắp xếp mới nhất trước
            donhang = donhang.OrderByDescending(x => x.NgayDat);

            // Phân trang
            int totalItems = donhang.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = donhang
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Lưu lại tham số lọc để hiển thị lại trên View
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            ViewBag.MaNguoiDung = maNguoiDung;
            ViewBag.TrangThai = trangThai;
            ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");
            ViewBag.DenNgay = denNgay?.ToString("yyyy-MM-dd");

            return View(result);
        }



        // GET: DonHangs/Details/5
        public IActionResult Details(string id)
        {
            var donHang = _donHangRepository.GetCTDH(id);
            return View(donHang);
        }

        // GET: DonHangs/Create
        public IActionResult Create()
        {
            var nguoidung = _nguoiDungRepository.GetAll();
            ViewData["MaNguoiDung"] = new SelectList(nguoidung, "MaNguoiDung", "MaNguoiDung");
            return View();
        }

        // POST: DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDonHang,MaNguoiDung,DiaChiNhanHang,NgayDat,TrangThai,TongTien")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _donHangRepository.Add(donHang);
                _donHangRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            var nguoidung = _nguoiDungRepository.GetAll();
            ViewData["MaNguoiDung"] = new SelectList(nguoidung, "MaNguoiDung", "MaNguoiDung");
            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public IActionResult Edit(string id)
        {
            var donHang = _donHangRepository.GetById(id);
            if (donHang == null)
            {
                return NotFound();
            }
            var nguoidung = _nguoiDungRepository.GetAll();
            ViewData["MaNguoiDung"] = new SelectList(nguoidung, "MaNguoiDung", "MaNguoiDung");
            return View(donHang);
        }

        // POST: DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDonHang,MaNguoiDung,DiaChiNhanHang,NgayDat,TrangThai,TongTien")] DonHang donHang)
        {
            if (id != donHang.MaDonHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _donHangRepository.Update(donHang);
                    _donHangRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            var nguoidung = _nguoiDungRepository.GetAll();
            ViewData["MaNguoiDung"] = new SelectList(nguoidung, "MaNguoiDung", "MaNguoiDung");
            return View(donHang);
        }

        // GET: DonHangs/Delete/5
        public IActionResult Delete(string id)
        {
            var donHang = _donHangRepository.GetById(id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // POST: DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var donHang = _donHangRepository.GetById(id);
            if (donHang != null)
            {
                _donHangRepository.Delete(donHang);
                _donHangRepository.Save();
            }     
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GiaoHang(string id)
        {
            var donhang = _donHangRepository.GetById(id);
            donhang.TrangThai = "Đang giao hàng";
            TempData["Message"] = "Đang giao hàng " + donhang.MaDonHang + " !";
            TempData["MessageType"] = "success";
            _donHangRepository.Update(donhang);
            _donHangRepository.Save();
            var chitiet = _chiTietDonHangRepository.GetByMaDonHang(donhang.MaDonHang);
            foreach(var item in chitiet)
            {
                var sach = _sachRepository.GetById(item.MaSach);
                sach.SoLuong -= item.SoLuong;
                _sachRepository.Update(sach);
                _sachRepository.Save();
            }
            return RedirectToAction("Index");
        }
        // POST: DonHang/HuyHang
       

    }
}

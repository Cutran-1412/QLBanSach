using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data;
using QLBanSach.Data.DonHangRepository;
using QLBanSach.Data.NguoiDungRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class DonHangsController : Controller
    {
        private readonly IDonHangRepository _donHangRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;

        public DonHangsController(IDonHangRepository donHangRepository, INguoiDungRepository nguoiDungRepository)
        {
            _donHangRepository = donHangRepository;
            _nguoiDungRepository = nguoiDungRepository;
        }



        // GET: DonHangs
        public IActionResult Index()
        {
            var donghang = _donHangRepository.GetAll();
            return View(donghang);
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class DonHangsController : Controller
    {
        private readonly AppDbContext _context;

        public DonHangsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DonHangs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DonHang.Include(d => d.NguoiDung);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DonHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DonHang == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .Include(d => d.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaDonHang == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // GET: DonHangs/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDung, "MaNguoiDung", "MaNguoiDung");
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
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDung, "MaNguoiDung", "MaNguoiDung", donHang.MaNguoiDung);
            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DonHang == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDung, "MaNguoiDung", "MaNguoiDung", donHang.MaNguoiDung);
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
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.MaDonHang))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDung, "MaNguoiDung", "MaNguoiDung", donHang.MaNguoiDung);
            return View(donHang);
        }

        // GET: DonHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DonHang == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .Include(d => d.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaDonHang == id);
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
            if (_context.DonHang == null)
            {
                return Problem("Entity set 'AppDbContext.DonHang'  is null.");
            }
            var donHang = await _context.DonHang.FindAsync(id);
            if (donHang != null)
            {
                _context.DonHang.Remove(donHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(string id)
        {
          return (_context.DonHang?.Any(e => e.MaDonHang == id)).GetValueOrDefault();
        }
    }
}

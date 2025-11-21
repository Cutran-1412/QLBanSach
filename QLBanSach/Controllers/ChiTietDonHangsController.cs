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
    public class ChiTietDonHangsController : Controller
    {
        private readonly AppDbContext _context;

        public ChiTietDonHangsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChiTietDonHangs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ChiTietDonHang.Include(c => c.DonHang).Include(c => c.Sach);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ChiTietDonHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ChiTietDonHang == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHang
                .Include(c => c.DonHang)
                .Include(c => c.Sach)
                .FirstOrDefaultAsync(m => m.MaChiTiet == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Create
        public IActionResult Create()
        {
            ViewData["MaDonHang"] = new SelectList(_context.DonHang, "MaDonHang", "MaDonHang");
            ViewData["MaSach"] = new SelectList(_context.Sach, "MaSach", "MaSach");
            return View();
        }

        // POST: ChiTietDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChiTiet,MaDonHang,MaSach,SoLuong,DonGia")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDonHang"] = new SelectList(_context.DonHang, "MaDonHang", "MaDonHang", chiTietDonHang.MaDonHang);
            ViewData["MaSach"] = new SelectList(_context.Sach, "MaSach", "MaSach", chiTietDonHang.MaSach);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ChiTietDonHang == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHang.FindAsync(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }
            ViewData["MaDonHang"] = new SelectList(_context.DonHang, "MaDonHang", "MaDonHang", chiTietDonHang.MaDonHang);
            ViewData["MaSach"] = new SelectList(_context.Sach, "MaSach", "MaSach", chiTietDonHang.MaSach);
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaChiTiet,MaDonHang,MaSach,SoLuong,DonGia")] ChiTietDonHang chiTietDonHang)
        {
            if (id != chiTietDonHang.MaChiTiet)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietDonHangExists(chiTietDonHang.MaChiTiet))
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
            ViewData["MaDonHang"] = new SelectList(_context.DonHang, "MaDonHang", "MaDonHang", chiTietDonHang.MaDonHang);
            ViewData["MaSach"] = new SelectList(_context.Sach, "MaSach", "MaSach", chiTietDonHang.MaSach);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ChiTietDonHang == null)
            {
                return NotFound();
            }

            var chiTietDonHang = await _context.ChiTietDonHang
                .Include(c => c.DonHang)
                .Include(c => c.Sach)
                .FirstOrDefaultAsync(m => m.MaChiTiet == id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ChiTietDonHang == null)
            {
                return Problem("Entity set 'AppDbContext.ChiTietDonHang'  is null.");
            }
            var chiTietDonHang = await _context.ChiTietDonHang.FindAsync(id);
            if (chiTietDonHang != null)
            {
                _context.ChiTietDonHang.Remove(chiTietDonHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietDonHangExists(string id)
        {
          return (_context.ChiTietDonHang?.Any(e => e.MaChiTiet == id)).GetValueOrDefault();
        }
    }
}

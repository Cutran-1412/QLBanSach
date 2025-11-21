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
    public class ChiTietGioHangsController : Controller
    {
        private readonly AppDbContext _context;

        public ChiTietGioHangsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChiTietGioHangs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ChiTietGioHang.Include(c => c.GioHang).Include(c => c.Sach);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ChiTietGioHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ChiTietGioHang == null)
            {
                return NotFound();
            }

            var chiTietGioHang = await _context.ChiTietGioHang
                .Include(c => c.GioHang)
                .Include(c => c.Sach)
                .FirstOrDefaultAsync(m => m.MaChiTiet == id);
            if (chiTietGioHang == null)
            {
                return NotFound();
            }

            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Create
        public IActionResult Create()
        {
            ViewData["MaGioHang"] = new SelectList(_context.GioHang, "MaGioHang", "MaGioHang");
            ViewData["MaSach"] = new SelectList(_context.Sach, "MaSach", "MaSach");
            return View();
        }

        // POST: ChiTietGioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChiTiet,MaGioHang,MaSach,SoLuong")] ChiTietGioHang chiTietGioHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietGioHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaGioHang"] = new SelectList(_context.GioHang, "MaGioHang", "MaGioHang", chiTietGioHang.MaGioHang);
            ViewData["MaSach"] = new SelectList(_context.Sach, "MaSach", "MaSach", chiTietGioHang.MaSach);
            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ChiTietGioHang == null)
            {
                return NotFound();
            }

            var chiTietGioHang = await _context.ChiTietGioHang.FindAsync(id);
            if (chiTietGioHang == null)
            {
                return NotFound();
            }
            ViewData["MaGioHang"] = new SelectList(_context.GioHang, "MaGioHang", "MaGioHang", chiTietGioHang.MaGioHang);
            ViewData["MaSach"] = new SelectList(_context.Sach, "MaSach", "MaSach", chiTietGioHang.MaSach);
            return View(chiTietGioHang);
        }

        // POST: ChiTietGioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaChiTiet,MaGioHang,MaSach,SoLuong")] ChiTietGioHang chiTietGioHang)
        {
            if (id != chiTietGioHang.MaChiTiet)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietGioHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietGioHangExists(chiTietGioHang.MaChiTiet))
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
            ViewData["MaGioHang"] = new SelectList(_context.GioHang, "MaGioHang", "MaGioHang", chiTietGioHang.MaGioHang);
            ViewData["MaSach"] = new SelectList(_context.Sach, "MaSach", "MaSach", chiTietGioHang.MaSach);
            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ChiTietGioHang == null)
            {
                return NotFound();
            }

            var chiTietGioHang = await _context.ChiTietGioHang
                .Include(c => c.GioHang)
                .Include(c => c.Sach)
                .FirstOrDefaultAsync(m => m.MaChiTiet == id);
            if (chiTietGioHang == null)
            {
                return NotFound();
            }

            return View(chiTietGioHang);
        }

        // POST: ChiTietGioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ChiTietGioHang == null)
            {
                return Problem("Entity set 'AppDbContext.ChiTietGioHang'  is null.");
            }
            var chiTietGioHang = await _context.ChiTietGioHang.FindAsync(id);
            if (chiTietGioHang != null)
            {
                _context.ChiTietGioHang.Remove(chiTietGioHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietGioHangExists(string id)
        {
          return (_context.ChiTietGioHang?.Any(e => e.MaChiTiet == id)).GetValueOrDefault();
        }
    }
}

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
    public class GioHangsController : Controller
    {
        private readonly AppDbContext _context;

        public GioHangsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GioHangs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GioHang.Include(g => g.NguoiDung);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GioHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.GioHang == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHang
                .Include(g => g.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaGioHang == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // GET: GioHangs/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDung, "MaNguoiDung", "MaNguoiDung");
            return View();
        }

        // POST: GioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaGioHang,MaNguoiDung,NgayTao")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gioHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDung, "MaNguoiDung", "MaNguoiDung", gioHang.MaNguoiDung);
            return View(gioHang);
        }

        // GET: GioHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.GioHang == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHang.FindAsync(id);
            if (gioHang == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDung, "MaNguoiDung", "MaNguoiDung", gioHang.MaNguoiDung);
            return View(gioHang);
        }

        // POST: GioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaGioHang,MaNguoiDung,NgayTao")] GioHang gioHang)
        {
            if (id != gioHang.MaGioHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gioHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GioHangExists(gioHang.MaGioHang))
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
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDung, "MaNguoiDung", "MaNguoiDung", gioHang.MaNguoiDung);
            return View(gioHang);
        }

        // GET: GioHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.GioHang == null)
            {
                return NotFound();
            }

            var gioHang = await _context.GioHang
                .Include(g => g.NguoiDung)
                .FirstOrDefaultAsync(m => m.MaGioHang == id);
            if (gioHang == null)
            {
                return NotFound();
            }

            return View(gioHang);
        }

        // POST: GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.GioHang == null)
            {
                return Problem("Entity set 'AppDbContext.GioHang'  is null.");
            }
            var gioHang = await _context.GioHang.FindAsync(id);
            if (gioHang != null)
            {
                _context.GioHang.Remove(gioHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GioHangExists(string id)
        {
          return (_context.GioHang?.Any(e => e.MaGioHang == id)).GetValueOrDefault();
        }
    }
}

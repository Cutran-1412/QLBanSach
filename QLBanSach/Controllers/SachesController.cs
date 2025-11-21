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
    public class SachesController : Controller
    {
        private readonly AppDbContext _context;

        public SachesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Saches
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Sach.Include(s => s.TheLoai);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Saches/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Sach == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach
                .Include(s => s.TheLoai)
                .FirstOrDefaultAsync(m => m.MaSach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: Saches/Create
        public IActionResult Create()
        {
            ViewData["MaTheLoai"] = new SelectList(_context.Set<TheLoai>(), "MaTheLoai", "MaTheLoai");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSach,TenSach,TacGia,NhaXuatBan,NamXuatBan,Gia,SoLuong,MoTa,Anh,MaTheLoai")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaTheLoai"] = new SelectList(_context.Set<TheLoai>(), "MaTheLoai", "MaTheLoai", sach.MaTheLoai);
            return View(sach);
        }

        // GET: Saches/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Sach == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            ViewData["MaTheLoai"] = new SelectList(_context.Set<TheLoai>(), "MaTheLoai", "MaTheLoai", sach.MaTheLoai);
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSach,TenSach,TacGia,NhaXuatBan,NamXuatBan,Gia,SoLuong,MoTa,Anh,MaTheLoai")] Sach sach)
        {
            if (id != sach.MaSach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.MaSach))
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
            ViewData["MaTheLoai"] = new SelectList(_context.Set<TheLoai>(), "MaTheLoai", "MaTheLoai", sach.MaTheLoai);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Sach == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach
                .Include(s => s.TheLoai)
                .FirstOrDefaultAsync(m => m.MaSach == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Sach == null)
            {
                return Problem("Entity set 'AppDbContext.Sach'  is null.");
            }
            var sach = await _context.Sach.FindAsync(id);
            if (sach != null)
            {
                _context.Sach.Remove(sach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(string id)
        {
          return (_context.Sach?.Any(e => e.MaSach == id)).GetValueOrDefault();
        }
    }
}

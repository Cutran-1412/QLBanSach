using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data;
using QLBanSach.Data.NguoiDungRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class NguoiDungsController : Controller
    {
        private readonly INguoiDungRepository _nguoiDungRepository;

        public NguoiDungsController(INguoiDungRepository nguoiDungRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
        }



        // GET: NguoiDungs
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;
            int totalItems = _nguoiDungRepository.GetTotalCount();

            var items = _nguoiDungRepository.GetPaged(page, pageSize);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return View(items);
        }

        // GET: NguoiDungs/Details/5
        public IActionResult Details(string id)
        {
            var nguoiDung = _nguoiDungRepository.GetById(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
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
        // GET: NguoiDungs/Create
        public IActionResult Create()
        {
            ViewBag.MaNguoiDung = GenerateMaNguoiDung();
            return View();
        }

        // POST: NguoiDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                _nguoiDungRepository.Add(nguoiDung);
                _nguoiDungRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Edit/5
        public IActionResult Edit(string id)
        {
            var nguoiDung = _nguoiDungRepository.GetById(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _nguoiDungRepository.Update(nguoiDung);
                    _nguoiDungRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Delete/5
        public IActionResult Delete(string id)
        {
            var nguoiDung = _nguoiDungRepository.GetById(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var nguoiDung = _nguoiDungRepository.GetById(id);
            if (nguoiDung != null)
            {
                _nguoiDungRepository.Delete(nguoiDung);
                _nguoiDungRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

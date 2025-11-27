using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBanSach.Data;
using QLBanSach.Data.TheLoaiRepository;
using QLBanSach.Models;

namespace QLBanSach.Controllers
{
    public class TheLoaisController : Controller
    {
        private readonly ITheLoaiRepository _theLoaiRepository;

        public TheLoaisController(ITheLoaiRepository theLoaiRepository)
        {
            _theLoaiRepository = theLoaiRepository;
        }

        // GET: TheLoais
        public IActionResult Index()
        {
            var theloai = _theLoaiRepository.GetAll();
            return View(theloai);
        }

        // GET: TheLoais/Details/5
        public IActionResult Details(string id)
        {
            var theLoai = _theLoaiRepository.GetById(id);
            if (theLoai == null)
            {
                return NotFound();
            }

            return View(theLoai);
        }

        // GET: TheLoais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TheLoais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                _theLoaiRepository.Add(theLoai);
                _theLoaiRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(theLoai);
        }

        // GET: TheLoais/Edit/5
        public IActionResult Edit(string id)
        {
            var theLoai = _theLoaiRepository.GetById(id);
            if (theLoai == null)
            {
                return NotFound();
            }
            return View(theLoai);
        }

        // POST: TheLoais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("MaTheLoai,TenTheLoai")] TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _theLoaiRepository.Update(theLoai);
                    _theLoaiRepository.Delete(theLoai);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(theLoai);
        }

        // GET: TheLoais/Delete/5
        public IActionResult Delete(string id)
        {
            var theLoai = _theLoaiRepository.GetById(id);
            if (theLoai == null)
            {
                return NotFound();
            }

            return View(theLoai);
        }

        // POST: TheLoais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var theLoai = _theLoaiRepository.GetById(id);
            if (theLoai != null)
            {
                _theLoaiRepository.Delete(theLoai);
                _theLoaiRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}

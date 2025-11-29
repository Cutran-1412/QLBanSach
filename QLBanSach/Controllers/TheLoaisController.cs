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
        public IActionResult Index(int page = 1, int pageSize =10)
        {
            var theloai = _theLoaiRepository.GetAll().ToList();

            int totalItems = theloai.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = theloai
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View(items);
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
                if (_theLoaiRepository.GetById(theLoai.MaTheLoai)!=null)
                {
                    TempData["Message"] = "Bị trùng mã thể loại!";
                    TempData["MessageType"] = "warning";
                    return View(theLoai);
                }
                _theLoaiRepository.Add(theLoai);
                _theLoaiRepository.Save();
                TempData["Message"] = "Thêm thành công thể loại "+theLoai.TenTheLoai+" !" ;
                TempData["MessageType"] = "success";
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
                    TempData["Message"] = "Sửa thành công mã thể loại " + theLoai.MaTheLoai + " !";
                    TempData["MessageType"] = "success";
                    _theLoaiRepository.Update(theLoai);
                    _theLoaiRepository.Save();
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
            if (_theLoaiRepository.IsUsedInSach(id))
            {
                TempData["Message"] = "Thể loại đang có ở sách vui lòng không xoá!";
                TempData["MessageType"] = "warning";
                return RedirectToAction(nameof(Index));
            }
            if (theLoai != null)
            {
                TempData["Message"] = "Xoá thành công mã "+theLoai.MaTheLoai+" !";
                TempData["MessageType"] = "error";
                _theLoaiRepository.Delete(theLoai);
                _theLoaiRepository.Save();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}

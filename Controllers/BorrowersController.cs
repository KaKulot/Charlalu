using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowersController(LibraryContext context)
        {
            _context = context;
        }

        // READ - List all records
        public async Task<IActionResult> Index(string search, string statusFilter)
        {
            var records = _context.BorrowRecords.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                records = records.Where(r =>
                    (r.BorrowerName ?? "").Contains(search) ||
                    (r.BookTitle ?? "").Contains(search));
            }

            if (!string.IsNullOrEmpty(statusFilter))
            {
                records = records.Where(r => r.Status == statusFilter);
            }

            ViewBag.Search = search;
            ViewBag.StatusFilter = statusFilter;
            ViewBag.TotalCount = await _context.BorrowRecords.CountAsync();
            ViewBag.BorrowedCount = await _context.BorrowRecords.CountAsync(r => r.Status == "Borrowed");
            ViewBag.ReturnedCount = await _context.BorrowRecords.CountAsync(r => r.Status == "Returned");

            return View(await records.OrderByDescending(r => r.BorrowDate).ToListAsync());
        }

        // READ - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var record = await _context.BorrowRecords.FirstOrDefaultAsync(r => r.BorrowId == id);
            if (record == null) return NotFound();

            return View(record);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            var model = new BorrowRecord { BorrowDate = DateTime.Today };
            return View(model);
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowerName,BookTitle,BorrowDate,ReturnDate,Status")] BorrowRecord record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(record);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Record added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(record);
        }

        // UPDATE - GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null) return NotFound();

            return View(record);
        }

        // UPDATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowId,BorrowerName,BookTitle,BorrowDate,ReturnDate,Status")] BorrowRecord record)
        {
            if (id != record.BorrowId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Record updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.BorrowRecords.Any(r => r.BorrowId == record.BorrowId))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(record);
        }

        // DELETE - GET (Confirm page)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var record = await _context.BorrowRecords.FirstOrDefaultAsync(r => r.BorrowId == id);
            if (record == null) return NotFound();

            return View(record);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record != null)
            {
                _context.BorrowRecords.Remove(record);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Record deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookLibrary.Controllers
{
    public class BooksController : Controller

    {
        private ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_db.Books.OrderBy(x => x.Price).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction("index");

            }
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);

            if(book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                //_db.Update(book);
                var BookFromDb = await _db.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

                BookFromDb.Name = book.Name;
                BookFromDb.Author = book.Author;
                BookFromDb.Price = book.Price;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBook(int? id)
        {
           
                var book = await _db.Books.FirstOrDefaultAsync(m => m.Id == id);

            _db.Books.Remove(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


    }
}

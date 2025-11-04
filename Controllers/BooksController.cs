using Microsoft.AspNetCore.Mvc;
using GrandLineBooks.Models;
using GrandLineBooks.Services;
using MongoDB.Driver;

namespace GrandLineBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public BooksController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        // GET: Books (List all books)
        public async Task<IActionResult> Index()
        {
            var services = await _mongoDBService.Services.Find(_ => true).ToListAsync();
            return View(services);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        public async Task<IActionResult> Create(Service book)
        {
            book.CreatedAt = DateTime.Now;
            await _mongoDBService.Services.InsertOneAsync(book);
            return RedirectToAction("Index");
        }

        // GET: Books/Edit/id
        public async Task<IActionResult> Edit(string id)
        {
            var book = await _mongoDBService.Services
                .Find(b => b.Id == id)
                .FirstOrDefaultAsync();
            return View(book);
        }

        // POST: Books/Edit/id
        [HttpPost]
        public async Task<IActionResult> Edit(string id, Service book)
        {
            var filter = Builders<Service>.Filter.Eq(b => b.Id, id);
            await _mongoDBService.Services.ReplaceOneAsync(filter, book);
            return RedirectToAction("Index");
        }

        // GET: Books/Delete/id
        public async Task<IActionResult> Delete(string id)
        {
            var filter = Builders<Service>.Filter.Eq(b => b.Id, id);
            await _mongoDBService.Services.DeleteOneAsync(filter);
            return RedirectToAction("Index");
        }

        // GET: Books/Details/id
        public async Task<IActionResult> Details(string id)
        {
            var book = await _mongoDBService.Services
                .Find(b => b.Id == id)
                .FirstOrDefaultAsync();
            return View(book);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using GrandLineBooks.Models;
using GrandLineBooks.Services;
using MongoDB.Driver;

namespace GrandLineBooks.Controllers
{
    public class AdminController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public AdminController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        // Check if user is admin
        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Admin";
        }

        // GET: Admin/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            // Get statistics
            ViewBag.TotalBooks = await _mongoDBService.Services.CountDocumentsAsync(_ => true);
            ViewBag.TotalOrders = await _mongoDBService.Orders.CountDocumentsAsync(_ => true);
            ViewBag.TotalUsers = await _mongoDBService.Users.CountDocumentsAsync(u => u.Role == "Customer");
            ViewBag.PendingOrders = await _mongoDBService.Orders.CountDocumentsAsync(o => o.Status == "Pending");

            return View();
        }

        // ===== BOOKS MANAGEMENT =====

        // GET: Admin/Books
        public async Task<IActionResult> Books()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var books = await _mongoDBService.Services.Find(_ => true).ToListAsync();
            return View(books);
        }

        // GET: Admin/CreateBook
        public IActionResult CreateBook()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: Admin/CreateBook
        [HttpPost]
        public async Task<IActionResult> CreateBook(Service book)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            book.CreatedAt = DateTime.Now;
            await _mongoDBService.Services.InsertOneAsync(book);

            TempData["Success"] = "Layanan berhasil ditambahkan!";
            return RedirectToAction("Books");
        }

        // GET: Admin/EditBook/id
        public async Task<IActionResult> EditBook(string id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var book = await _mongoDBService.Services.Find(b => b.Id == id).FirstOrDefaultAsync();
            return View(book);
        }

        // POST: Admin/EditBook/id
        [HttpPost]
        public async Task<IActionResult> EditBook(string id, Service book)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            book.Id = id;
            var filter = Builders<Service>.Filter.Eq(b => b.Id, id);
            await _mongoDBService.Services.ReplaceOneAsync(filter, book);

            TempData["Success"] = "Layanan berhasil diupdate!";
            return RedirectToAction("Books");
        }

        // GET: Admin/DeleteBook/id
        public async Task<IActionResult> DeleteBook(string id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var filter = Builders<Service>.Filter.Eq(b => b.Id, id);
            await _mongoDBService.Services.DeleteOneAsync(filter);

            TempData["Success"] = "Layanan berhasil dihapus!";
            return RedirectToAction("Books");
        }

        // ===== ORDERS MANAGEMENT =====

        // GET: Admin/Orders
        public async Task<IActionResult> Orders()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var orders = await _mongoDBService.Orders.Find(_ => true).SortByDescending(o => o.OrderDate).ToListAsync();

            // Get customer names for each order
            foreach (var order in orders)
            {
                var customer = await _mongoDBService.Users.Find(u => u.Id == order.CustomerId).FirstOrDefaultAsync();
                ViewData[$"CustomerName_{order.Id}"] = customer?.FullName ?? "Unknown";
            }

            return View(orders);
        }

        // POST: Admin/UpdateOrderStatus
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(string orderId, string status)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
            var update = Builders<Order>.Update.Set(o => o.Status, status);
            await _mongoDBService.Orders.UpdateOneAsync(filter, update);

            TempData["Success"] = "Status order berhasil diupdate!";
            return RedirectToAction("Orders");
        }

        // GET: Admin/OrderDetails/id
        public async Task<IActionResult> OrderDetails(string id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var order = await _mongoDBService.Orders.Find(o => o.Id == id).FirstOrDefaultAsync();
            var customer = await _mongoDBService.Users.Find(u => u.Id == order.CustomerId).FirstOrDefaultAsync();

            ViewBag.CustomerName = customer?.FullName;
            ViewBag.CustomerEmail = customer?.Email;
            ViewBag.CustomerPhone = customer?.PhoneNumber;

            return View(order);
        }

        // ===== USERS MANAGEMENT =====

        // GET: Admin/Users
        public async Task<IActionResult> Users()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var users = await _mongoDBService.Users.Find(_ => true).ToListAsync();
            return View(users);
        }

        // GET: Admin/DeleteUser/id
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            await _mongoDBService.Users.DeleteOneAsync(filter);

            TempData["Success"] = "User berhasil dihapus!";
            return RedirectToAction("Users");
        }

        // POST: Admin/ToggleUserRole
        [HttpPost]
        public async Task<IActionResult> ToggleUserRole(string userId)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var user = await _mongoDBService.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
            var newRole = user.Role == "Admin" ? "Customer" : "Admin";

            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Set(u => u.Role, newRole);
            await _mongoDBService.Users.UpdateOneAsync(filter, update);

            TempData["Success"] = $"Role berhasil diubah menjadi {newRole}!";
            return RedirectToAction("Users");
        }
    }
}
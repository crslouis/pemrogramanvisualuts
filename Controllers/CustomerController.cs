using Microsoft.AspNetCore.Mvc;
using GrandLineBooks.Models;
using GrandLineBooks.Services;
using MongoDB.Driver;

namespace GrandLineBooks.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public CustomerController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        // Check if user is logged in
        private bool IsLoggedIn()
        {
            var userId = HttpContext.Session.GetString("UserId");
            return !string.IsNullOrEmpty(userId);
        }

        // Get current user ID
        private string GetUserId()
        {
            return HttpContext.Session.GetString("UserId") ?? string.Empty;
        }

        // ===== CATALOG =====
        // GET: Customer/Home
        public IActionResult Home()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Index", "Home");

            return View();
        }
        // GET: Customer/Catalog
        public async Task<IActionResult> Catalog(string category = "", string search = "")
        {
            var filter = Builders<Service>.Filter.Empty;

            // Filter by category
            if (!string.IsNullOrEmpty(category))
            {
                filter = Builders<Service>.Filter.Eq(b => b.Category, category);
            }

            // Filter by search
            if (!string.IsNullOrEmpty(search))
            {
                var titleFilter = Builders<Service>.Filter.Regex(b => b.Title, new MongoDB.Bson.BsonRegularExpression(search, "i"));
                var authorFilter = Builders<Service>.Filter.Regex(b => b.Author, new MongoDB.Bson.BsonRegularExpression(search, "i"));
                filter = filter & (titleFilter | authorFilter);
            }

            var books = await _mongoDBService.Services.Find(filter).ToListAsync();

            ViewBag.Categories = await _mongoDBService.Services.Distinct<string>("Category", Builders<Service>.Filter.Empty).ToListAsync();
            ViewBag.SelectedCategory = category;
            ViewBag.SearchQuery = search;

            return View(books);
        }

        // GET: Customer/BookDetail/id
        public async Task<IActionResult> BookDetail(string id)
        {
            var book = await _mongoDBService.Services.Find(b => b.Id == id).FirstOrDefaultAsync();

            if (book == null)
            {
                return RedirectToAction("Catalog");
            }

            return View(book);
        }

        // ===== CART =====

        // GET: Customer/Cart
        public async Task<IActionResult> Cart()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetUserId();
            var cart = await _mongoDBService.Carts.Find(c => c.CustomerId == userId).FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new Cart { CustomerId = userId, Items = new List<CartItem>() };
            }

            return View(cart);
        }

        // POST: Customer/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(string bookId, int quantity = 1)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetUserId();
            var book = await _mongoDBService.Services.Find(b => b.Id == bookId).FirstOrDefaultAsync();

            if (book == null || book.Stock < quantity)
            {
                TempData["Error"] = "Stok layanan tidak mencukupi!";
                return RedirectToAction("BookDetail", new { id = bookId });
            }

            var cart = await _mongoDBService.Carts.Find(c => c.CustomerId == userId).FirstOrDefaultAsync();

            if (cart == null)
            {
                // Create new cart
                cart = new Cart
                {
                    CustomerId = userId,
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            BookId = bookId,
                            BookTitle = book.Title,
                            Quantity = quantity,
                            Price = book.Price
                        }
                    }
                };
                await _mongoDBService.Carts.InsertOneAsync(cart);
            }
            else
            {
                // Update existing cart
                var existingItem = cart.Items.FirstOrDefault(i => i.BookId == bookId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        BookId = bookId,
                        BookTitle = book.Title,
                        Quantity = quantity,
                        Price = book.Price
                    });
                }

                cart.UpdatedAt = DateTime.Now;
                var filter = Builders<Cart>.Filter.Eq(c => c.Id, cart.Id);
                await _mongoDBService.Carts.ReplaceOneAsync(filter, cart);
            }

            TempData["Success"] = "Layanan berhasil ditambahkan ke keranjang!";
            return RedirectToAction("Cart");
        }

        // POST: Customer/UpdateCartItem
        [HttpPost]
        public async Task<IActionResult> UpdateCartItem(string bookId, int quantity)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetUserId();
            var cart = await _mongoDBService.Carts.Find(c => c.CustomerId == userId).FirstOrDefaultAsync();

            if (cart != null)
            {
                var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
                if (item != null)
                {
                    if (quantity <= 0)
                    {
                        cart.Items.Remove(item);
                    }
                    else
                    {
                        item.Quantity = quantity;
                    }

                    cart.UpdatedAt = DateTime.Now;
                    var filter = Builders<Cart>.Filter.Eq(c => c.Id, cart.Id);
                    await _mongoDBService.Carts.ReplaceOneAsync(filter, cart);
                }
            }

            return RedirectToAction("Cart");
        }

        // GET: Customer/RemoveFromCart/bookId
        public async Task<IActionResult> RemoveFromCart(string bookId)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetUserId();
            var cart = await _mongoDBService.Carts.Find(c => c.CustomerId == userId).FirstOrDefaultAsync();

            if (cart != null)
            {
                cart.Items.RemoveAll(i => i.BookId == bookId);
                cart.UpdatedAt = DateTime.Now;

                var filter = Builders<Cart>.Filter.Eq(c => c.Id, cart.Id);
                await _mongoDBService.Carts.ReplaceOneAsync(filter, cart);

                TempData["Success"] = "Item berhasil dihapus dari keranjang!";
            }

            return RedirectToAction("Cart");
        }

        // ===== CHECKOUT =====

        // GET: Customer/Checkout
        public async Task<IActionResult> Checkout()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetUserId();
            var cart = await _mongoDBService.Carts.Find(c => c.CustomerId == userId).FirstOrDefaultAsync();

            if (cart == null || cart.Items.Count == 0)
            {
                TempData["Error"] = "Keranjang kosong!";
                return RedirectToAction("Cart");
            }

            var user = await _mongoDBService.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();
            ViewBag.User = user;

            return View(cart);
        }

        // POST: Customer/PlaceOrder
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string shippingAddress)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetUserId();
            var cart = await _mongoDBService.Carts.Find(c => c.CustomerId == userId).FirstOrDefaultAsync();

            if (cart == null || cart.Items.Count == 0)
            {
                TempData["Error"] = "Keranjang kosong!";
                return RedirectToAction("Cart");
            }

            // Calculate total
            decimal totalAmount = 0;
            foreach (var item in cart.Items)
            {
                totalAmount += item.Price * item.Quantity;
            }

            // Create order
            var order = new Order
            {
                CustomerId = userId,
                Items = cart.Items.Select(i => new OrderItem
                {
                    BookId = i.BookId,
                    BookTitle = i.BookTitle,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList(),
                TotalAmount = totalAmount,
                Status = "Pending",
                ShippingAddress = shippingAddress,
                OrderDate = DateTime.Now
            };

            await _mongoDBService.Orders.InsertOneAsync(order);

            // Update stock
            foreach (var item in cart.Items)
            {
                var book = await _mongoDBService.Services.Find(b => b.Id == item.BookId).FirstOrDefaultAsync();
                if (book != null)
                {
                    book.Stock -= item.Quantity;
                    var filter = Builders<Service>.Filter.Eq(b => b.Id, item.BookId);
                    await _mongoDBService.Services.ReplaceOneAsync(filter, book);
                }
            }

            // Clear cart
            var cartFilter = Builders<Cart>.Filter.Eq(c => c.Id, cart.Id);
            await _mongoDBService.Carts.DeleteOneAsync(cartFilter);

            TempData["Success"] = "Pesanan berhasil dibuat!";
            return RedirectToAction("OrderSuccess", new { orderId = order.Id });
        }

        // GET: Customer/OrderSuccess/orderId
        public async Task<IActionResult> OrderSuccess(string orderId)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var order = await _mongoDBService.Orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();
            return View(order);
        }

        // ===== MY ORDERS =====

        // GET: Customer/MyOrders
        public async Task<IActionResult> MyOrders()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetUserId();
            var orders = await _mongoDBService.Orders
                .Find(o => o.CustomerId == userId)
                .SortByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: Customer/MyOrderDetail/id
        public async Task<IActionResult> MyOrderDetail(string id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var userId = GetUserId();
            var order = await _mongoDBService.Orders.Find(o => o.Id == id && o.CustomerId == userId).FirstOrDefaultAsync();

            if (order == null)
            {
                return RedirectToAction("MyOrders");
            }

            return View(order);

        }
    }
}

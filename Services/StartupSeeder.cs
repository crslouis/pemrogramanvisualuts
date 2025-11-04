using GrandLineBooks.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Hosting;

namespace GrandLineBooks.Services
{
    public class StartupSeeder : IHostedService
    {
        private readonly MongoDBService _mongo;

        public StartupSeeder(MongoDBService mongo)
        {
            _mongo = mongo;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Seed default laundry services if empty
            var count = await _mongo.Services.CountDocumentsAsync(Builders<Service>.Filter.Empty, cancellationToken: cancellationToken);
            if (count > 0) return;

            var now = DateTime.Now;
            var defaults = new List<Service>
            {
                new Service { Title = "Cuci Kering Lipat - Baju", Author = "Reguler", ISBN = "SKU-BJU-CKL", Category = "Baju", Price = 8000, Stock = 9999, Description = "Cuci, kering, dan lipat untuk baju harian.", ImageUrl = "", PublishedDate = now, CreatedAt = now },
                new Service { Title = "Cuci Kering Lipat - Celana", Author = "Reguler", ISBN = "SKU-CLN-CKL", Category = "Celana", Price = 9000, Stock = 9999, Description = "Cuci, kering, lipat celana kain/jeans.", ImageUrl = "", PublishedDate = now, CreatedAt = now },
                new Service { Title = "Setrika Saja - Baju", Author = "Express", ISBN = "SKU-BJU-STR", Category = "Baju", Price = 6000, Stock = 9999, Description = "Layanan setrika rapi untuk baju.", ImageUrl = "", PublishedDate = now, CreatedAt = now },
                new Service { Title = "Dry Clean - Jas/Jaket", Author = "Premium", ISBN = "SKU-JKT-DRY", Category = "Jaket", Price = 35000, Stock = 9999, Description = "Dry clean aman untuk jas dan jaket.", ImageUrl = "", PublishedDate = now, CreatedAt = now },
                new Service { Title = "Bed Cover", Author = "Besar", ISBN = "SKU-BDC-STD", Category = "Selimut", Price = 40000, Stock = 9999, Description = "Cuci bed cover/selimut ukuran besar.", ImageUrl = "", PublishedDate = now, CreatedAt = now },
                new Service { Title = "Sepatu - Deep Clean", Author = "Premium", ISBN = "SKU-SPT-CLN", Category = "Sepatu", Price = 30000, Stock = 9999, Description = "Pembersihan sepatu mendalam, aman untuk bahan sensitif.", ImageUrl = "", PublishedDate = now, CreatedAt = now }
            };

            await _mongo.Services.InsertManyAsync(defaults, cancellationToken: cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}


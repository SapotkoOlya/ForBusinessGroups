
using Microsoft.Data.Sqlite;
using Dapper;
using Lection11_Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Lection11_Tests.Models;
using Lection11_Tests.Interfaces.ForDapper;
using Lection11_Tests.Repositories;

namespace Lection11_Tests.Tests
{
    public class Dapper
    {
        private readonly TestFixture fixture = new();

        //[Test]
        public async Task Initialize()
        {
            var connectionString = "Data Source=marketplace.db";

            await using var connection = new SqliteConnection(connectionString);

            await connection.OpenAsync();

            await DatabaseInitializer.InitializeAsync(connection);
        }

        [Test] //10
        //получить всех юзеров и проверить количество
        public async Task Should_Return_All_Users()
        {
            var repo = fixture.Provider.GetRequiredService<IUsersRepository>();
            var users = await repo.GetAllAsync();
            Assert.AreEqual(15, users.Count());
        }

        [Test] //10
        //получить конкретного юзера
        public async Task Should_Return_User_By_Id()
        {
            var repo = fixture.Provider.GetRequiredService<IUsersRepository>();
            var user = await repo.GetByIdAsync(1);
            Assert.IsNotNull(user);
        }

        [Test] //10
        // получить адрес юзера по айди юзера
        public async Task Should_Return_User_Addresses()
        {
            var repo = fixture.Provider.GetRequiredService<IUsersRepository>();
            var addresses = await repo.GetAddressesAsync(1);
            Assert.IsTrue(addresses.Any());
        }

        [Test]
        //получить список всех продуктов
        public async Task Should_Return_All_Products()
        {
            var repo = fixture.Provider.GetRequiredService<IProductsRepository>();
            var products = await repo.GetAllAsync();
            Assert.AreEqual(18, products.Count());
        }

        [Test]
        //получить продукт по айди
        public async Task Should_Return_Product_By_Id()
        {
            var repo = fixture.Provider.GetRequiredService<IProductsRepository>();
            var product = await repo.GetByIdAsync(18);
            Assert.IsNotNull(product);
        }

        [Test]
        //продукт по категории
        public async Task Should_Return_Products_By_Category()
        {
            var repo = fixture.Provider.GetRequiredService<IProductsRepository>();
            var products = await repo.GetByCategoryAsync(1);
            Assert.IsTrue(products.Any());
        }

        [Test]
        //все заказы из базы
        public async Task Should_Return_All_Orders()
        {
            var repo = fixture.Provider.GetRequiredService<IOrdersRepository>();
            var orders = await repo.GetAllAsync();
            Assert.AreEqual(17, orders.Count());
        }

        // 🟩 Проверяет, что заказ с указанным Id существует
        [Test]
        public async Task Should_Return_Order_By_Id()
        {
            var repo = fixture.Provider.GetRequiredService<IOrdersRepository>();
            var order = await repo.GetByIdAsync(1);
            Assert.IsNotNull(order);
        }

        // 🟩 Проверяет, что у заказа есть связанные товары (OrderItems)
        [Test]
        public async Task Should_Return_Order_Items()
        {
            var repo = fixture.Provider.GetRequiredService<IOrdersRepository>();
            var items = await repo.GetItemsAsync(1);
            Assert.IsTrue(items.Any());
        }

        // 🟩 Проверяет, что у товара есть отзывы
        [Test]
        public async Task Should_Return_Reviews_By_Product()
        {
            var repo = fixture.Provider.GetRequiredService<IReviewsRepository>();
            var reviews = await repo.GetByProductAsync(1);
            Assert.IsTrue(reviews.Any());
        }

        // 🟩 Проверяет, что у пользователя есть отзывы
        [Test]
        public async Task Should_Return_Reviews_By_User()
        {
            var repo = fixture.Provider.GetRequiredService<IReviewsRepository>();
            var reviews = await repo.GetByUserAsync(1);
            Assert.IsTrue(reviews.Any());
        }

        // 🟩 Проверяет, что пользователь покупал хотя бы один товар
        [Test]
        public async Task User_Should_Have_Purchased_Products()
        {
            var repo = fixture.Provider.GetRequiredService<IUsersRepository>();

            var products = await repo.GetUserPurchasedProductsAsync(1);

            Assert.IsTrue(products.Any());
        }

        // 🟩 Проверяет, что у пользователя есть заказы
        [Test]
        public async Task User_Orders_Should_Be_Returned()
        {
            var repo = fixture.Provider.GetRequiredService<IUsersRepository>();

            var orders = await repo.GetUserOrdersAsync(1);

            Assert.IsTrue(orders.Any());
        }

        // 🟩 Проверяет, что метод возвращает топ‑продаваемые товары и что у лидера есть выручка
        [Test]
        public async Task Should_Return_Top_Selling_Products()
        {
            var repo = fixture.Provider.GetRequiredService<IProductsRepository>();

            var top = await repo.GetTopSellingProductsAsync();

            Assert.IsTrue(top.Any());
            Assert.Greater(top.First().Item2, 0); // revenue > 0
        }

        // 🟩 Проверяет, что сумма заказа совпадает с суммой его товаров
        [Test]
        public async Task Order_Total_Should_Match_Items()
        {
            var repo = fixture.Provider.GetRequiredService<IOrdersRepository>();

            var total = await repo.GetOrderTotalAsync(1);
            var order = await repo.GetByIdAsync(1);

            Assert.AreEqual(order!.TotalPrice, total);
        }

        // 🟩 Проверяет, что метод возвращает заказы вместе с их товарами
        [Test]
        public async Task Should_Return_Orders_With_Items()
        {
            var repo = fixture.Provider.GetRequiredService<IOrdersRepository>();

            var orders = await repo.GetOrdersWithItemsAsync();

            Assert.IsTrue(orders.Any());
            Assert.IsTrue(orders.First().Items.Any());
        }

        // 🟩 Проверяет, что средний рейтинг товара корректный (0 < avg ≤ 5)
        [Test]
        public async Task Should_Return_Average_Rating()
        {
            var repo = fixture.Provider.GetRequiredService<IReviewsRepository>();

            var avg = await repo.GetAverageRatingAsync(1);

            Assert.Greater(avg, 0);
            Assert.LessOrEqual(avg, 5);
        }

    }  
}

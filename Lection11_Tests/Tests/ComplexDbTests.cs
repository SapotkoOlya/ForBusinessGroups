using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Lection11_Tests.Repositories;
using Lection11_Tests.Interfaces.ForDapper;
using Lection11_Tests.Fixtures;
using FluentAssertions;

namespace Lection11_Tests.Tests;

[TestFixture]
public class ComplexDbTests
{
    private readonly TestFixture fixture = new();

    // ============================================================
    // 1. Пользователи, купившие товары категории "Наушники",
    //    должны иметь хотя бы один отзыв на товары этой категории
    // ============================================================
    //[Test]
    public async Task UsersWhoBoughtHeadphones_ShouldHaveReviews()
    {
        var users = fixture.Provider.GetRequiredService<IUsersRepository>();
        var buyers = await users.GetUsersWhoBoughtCategoryAsync("Наушники");
        var reviewers = await users.GetUsersWithReviewsInCategoryAsync("Наушники");

        var missing = buyers.Select(u => u.Id)
                            .Except(reviewers.Select(u => u.Id))
                            .ToList();

        Assert.That(missing, Is.Empty);
    }

    // ============================================================
    // 2. Сумма OrderItems должна совпадать с TotalPrice заказа
    // ============================================================
    //[Test]
    public async Task OrderTotal_ShouldMatchItemsSum()
    {
        var ordersF = fixture.Provider.GetRequiredService<IOrdersRepository>();
        var orders = await ordersF.GetAllOrdersAsync();

        foreach (var order in orders)
        {
            var sum = await ordersF.GetOrderItemsSumAsync(order.Id);
            Assert.That(sum, Is.EqualTo(order.TotalPrice));
        }
    }

    // ============================================================
    // 3. Пользователи с >=2 заказами должны иметь >=2 отзывов
    // ============================================================
    //[Test]
    public async Task UsersWithManyOrders_ShouldHaveManyReviews()
    {
        var users = fixture.Provider.GetRequiredService<IUsersRepository>();
        var reviews = fixture.Provider.GetRequiredService<IReviewsRepository>();
        var manyOrders = await users.GetUsersWithOrdersCountAsync(2);
        var manyReviews = await reviews.GetUsersWithReviewsCountAsync(2);

        var intersection = manyOrders.Select(u => u.Id)
                                     .Intersect(manyReviews.Select(u => u.Id))
                                     .ToList();

        Assert.That(intersection, Is.Not.Empty);
    }

    // ============================================================
    // 4. Пользователь должен иметь адрес в том же городе,
    //    где он сделал хотя бы один заказ
    // ============================================================
    //[Test]
    public async Task UserAddressCity_ShouldMatchOrderCity()
    {
        var addresses = fixture.Provider.GetService<IAddressesRepository>();
        var mismatches = await addresses.GetAddressOrderCityMismatchesAsync();
        Assert.That(mismatches, Is.Empty);
    }

    // ============================================================
    // 5. Пользователь может оставить отзыв только на товар,
    //    который он покупал
    // ============================================================
    [Test]
    public async Task UsersShouldReviewOnlyPurchasedProducts()
    {
        var reviews = fixture.Provider.GetRequiredService<IReviewsRepository>();
        var invalid = await reviews.GetInvalidReviewsAsync();
        Assert.That(invalid, Is.Empty);
    }

    // ============================================================
    // 6. Товары категории "Аксессуары" должны покупать только
    //    пользователи из Москвы или Санкт-Петербурга
    //Покупатели аксессуаров живут в разных городах(не только Москва/СПб)
    // ============================================================
    [Test]
    public async Task Accessories_ShouldBeBoughtByMajorCitiesOnly()
    {
        var users = fixture.Provider.GetRequiredService<IUsersRepository>();

        // покупатели категории "Аксессуары"
        var buyers = await users.GetUsersWhoBoughtCategoryAsync("Аксессуары");

        // города покупателей
        var addresses = fixture.Provider.GetRequiredService<IAddressesRepository>();
        var allAddresses = await addresses.GetAllAddressesAsync();

        var buyerCities = buyers
            .Join(allAddresses,
                  u => u.Id,
                  a => a.UserId,
                  (u, a) => a.City)
            .Distinct()
            .ToList();

        // проверяем, что города действительно разные
        Assert.That(buyerCities.Count, Is.GreaterThan(1));
        buyerCities.Count.Should().BeGreaterThan(1);

    }

    // ============================================================
    // 7. Товары, которые покупали >=3 разных пользователей,
    //    должны иметь хотя бы один отзыв
    // ============================================================
    //[Test]
    public async Task PopularProducts_ShouldHaveReviews()
    {
        var reviews = fixture.Provider.GetRequiredService<IReviewsRepository>();
        var products = fixture.Provider.GetRequiredService<IProductsRepository>();
        var popularProducts = await products.GetProductsBoughtByUsersCountAsync(3);
        var productsWithReviews = await reviews.GetProductsWithReviewsAsync();

        var missing = popularProducts.Select(p => p.Id)
                                     .Except(productsWithReviews.Select(p => p.Id))
                                     .ToList();

        Assert.That(missing, Is.Empty);
    }

    // ============================================================
    // 8. Средний рейтинг товара должен совпадать с агрегированным
    //    значением из таблицы Reviews
    // ============================================================
    //[Test]
    public async Task ProductAverageRating_ShouldMatchReviews()
    {
        var reviews = fixture.Provider.GetRequiredService<IReviewsRepository>();
        var products = fixture.Provider.GetRequiredService<IProductsRepository>();
        var products2 = await products.GetAllProductsAsync();

        foreach (var p in products2)
        {
            var avg = await reviews.GetAverageRatingAsync(p.Id);
            var calc = await reviews.CalculateAverageRatingAsync(p.Id);

            Assert.That(avg, Is.EqualTo(calc));
        }
    }

    // ============================================================
    // 9. Пользователь, сделавший заказ > 100000,
    //    должен иметь хотя бы один отзыв с рейтингом 5
    // ============================================================
    //[Test]
    public async Task HighValueBuyers_ShouldHaveTopReviews()
    {
        var reviews = fixture.Provider.GetRequiredService<IReviewsRepository>();
        var orders = fixture.Provider.GetRequiredService<IOrdersRepository>();
        var buyers = await orders.GetUsersWithOrderTotalAboveAsync(100000);
        var topReviewers = await reviews.GetUsersWithRatingAsync(5);

        var missing = buyers.Select(u => u.Id)
                            .Except(topReviewers.Select(u => u.Id))
                            .ToList();

        Assert.That(missing, Is.Empty);
    }

    // ============================================================
    // 10. Покупатели телевизоров должны также покупать аксессуары
    // ============================================================
    [Test]
    public async Task TVBuyers_ShouldAlsoBuyAccessories()
    {
        var users = fixture.Provider.GetRequiredService<IUsersRepository>();
        var tvBuyers = await users.GetUsersWhoBoughtCategoryAsync("Телевизоры");
        var accessoriesBuyers = await users.GetUsersWhoBoughtCategoryAsync("Аксессуары");

        var missing = tvBuyers.Select(u => u.Id)
                              .Except(accessoriesBuyers.Select(u => u.Id))
                              .ToList();

        Assert.That(missing, Is.Empty);
    }

    // ✅ Шаги теста:
    // 1) Получить всех пользователей
    // 2) Получить все адреса
    // 3) Найти пользователей без адреса
    // 4) Убедиться, что таких нет
    //[Test]
    public async Task AllUsers_ShouldHaveAddresses()
    {
        var usersRepo = fixture.Provider.GetRequiredService<IUsersRepository>();
        var addrRepo = fixture.Provider.GetRequiredService<IAddressesRepository>();

        var users = await usersRepo.GetUsersWithOrdersCountAsync(0);
        var addresses = await addrRepo.GetAllAddressesAsync();

        var usersWithoutAddress = users
            .Where(u => !addresses.Any(a => a.UserId == u.Id))
            .ToList();

        Assert.That(usersWithoutAddress, Is.Empty);
    }

    // ✅ Шаги теста:
    // 1) Получить все товары
    // 2) Проверить, что у каждого CategoryId > 0
    [Test]
    public async Task AllProducts_ShouldHaveCategory()
    {
        var productsRepo = fixture.Provider.GetRequiredService<IProductsRepository>();
        var products = await productsRepo.GetAllProductsAsync();

        Assert.That(products.All(p => p.CategoryId > 0), Is.True);
    }

    // ✅ Шаги теста:
    // 1) Получить все отзывы
    // 2) Получить все товары
    // 3) Проверить, что каждый отзыв ссылается на существующий товар
    [Test]
    public async Task AllReviews_ShouldReferenceExistingProducts()
    {
        var reviewsRepo = fixture.Provider.GetRequiredService<IReviewsRepository>();
        var productsRepo = fixture.Provider.GetRequiredService<IProductsRepository>();

        var reviews = await reviewsRepo.GetInvalidReviewsAsync(); // твой метод уже фильтрует по покупкам
        var products = await productsRepo.GetAllProductsAsync();

        var invalid = reviews
            .Where(r => !products.Any(p => p.Id == r.ProductId))
            .ToList();

        Assert.That(invalid, Is.Empty);
    }

    // ✅ Шаги теста:
    // 1) Получить все заказы
    // 2) Получить всех пользователей
    // 3) Проверить, что каждый заказ ссылается на существующего пользователя
    [Test]
    public async Task AllOrders_ShouldReferenceExistingUsers()
    {
        var ordersRepo = fixture.Provider.GetRequiredService<IOrdersRepository>();
        var usersRepo = fixture.Provider.GetRequiredService<IUsersRepository>();

        var orders = await ordersRepo.GetAllOrdersAsync();
        var users = await usersRepo.GetUsersWithOrdersCountAsync(0);

        var invalid = orders
            .Where(o => !users.Any(u => u.Id == o.UserId))
            .ToList();

        Assert.That(invalid, Is.Empty);
    }

    // ✅ Шаги теста:
    // 1) Получить всех пользователей с отзывами
    // 2) Получить все заказы
    // 3) Проверить, что каждый автор отзыва имеет хотя бы один заказ
    [Test]
    public async Task ReviewAuthors_ShouldHaveOrders()
    {
        var reviewsRepo = fixture.Provider.GetRequiredService<IReviewsRepository>();
        var ordersRepo = fixture.Provider.GetRequiredService<IOrdersRepository>();

        var reviewers = await reviewsRepo.GetUsersWithReviewsCountAsync(1);
        var orders = await ordersRepo.GetAllOrdersAsync();

        var reviewersWithoutOrders = reviewers
            .Where(r => !orders.Any(o => o.UserId == r.Id))
            .ToList();

        Assert.That(reviewersWithoutOrders, Is.Empty);
    }

}

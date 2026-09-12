using FluentAssertions;
using Lection11_Tests.Interfaces;
using Lection11_Tests.Models.BookStore;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Lection11_Tests.Tests
{
    public class BookStoreTests
    {
        private IBookStoreApi api;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services
                .AddRefitClient<IBookStoreApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://demoqa.com");
                });

            var provider = services.BuildServiceProvider();
            api = provider.GetRequiredService<IBookStoreApi>();
        }

        //[Test]
        public async Task CreateUser_ShouldReturnUserId()
        {
            var response = await api.CreateUserAsync(new CreateUserDTO
            {
                UserName = "OlgaTestUser1",
                Password = "StrongPass123!"
            });

            response.UserID.Should().NotBeNullOrEmpty();
            response.Username.Should().Be("OlgaTestUser1");
            response.IsActive.Should().BeFalse();
        }

        //[Test]
        public async Task GenerateToken_ShouldReturnToken()
        {
            var tokenResponse = await api.GenerateTokenAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser1",
                Password = "StrongPass123!"
            });

            tokenResponse.Token.Should().NotBeNullOrEmpty();
            tokenResponse.Status.Should().Be("Success");
            tokenResponse.Result.Should().Contain("authorized");
        }

        //[Test]
        public async Task AddBook_ShouldAddBookToUser()
        {
            // 1. Получаем токен
            var tokenResponse = await api.GenerateTokenAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser2",
                Password = "StrongPass123!"
            });

            var token = $"Bearer {tokenResponse.Token}";

            // 2. Добавляем книгу
            var addResponse = await api.AddBooksAsync(
                new AddBooksRequestDTO
                {
                    UserId = "7be19e22-abe1-4d61-855c-a529cfcba4cb",
                    CollectionOfIsbns = new List<BookDTO>
                    {
                    new BookDTO { isbn = "9781449331818" }
                    }
                },
                token
            );

            addResponse.Books.Should().NotBeEmpty();
            addResponse.Books.Should().ContainSingle(b => b.isbn == "9781449325862");
        }

        [Test]
        public async Task DeleteBook_ShouldReturnSuccessMessage()
        {
            // 1. Получаем токен
            var tokenResponse = await api.GenerateTokenAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser2",
                Password = "StrongPass123!"
            });

            var token = $"Bearer {tokenResponse.Token}";

            // 2. Удаляем книгу
            var deleteResponse = await api.DeleteBookAsync(
                "7be19e22-abe1-4d61-855c-a529cfcba4cb",
                "9781449325862",
                token
            );

            deleteResponse.Message.Should().Be("Book deleted successfully");
        }

        //[Test]
        public async Task GetAllBooks_ShouldReturnList()
        {
            var books = await api.GetBooksAsync();

            books.Books.Should().NotBeNull();
            books.Books.Should().HaveCountGreaterThan(0);
        }

        //[Test]
        public async Task GetBookByIsbn_ShouldReturnBook()
        {
            var book = await api.GetBookAsync("9781449325862");

            book.isbn.Should().Be("9781449325862");
            book.title.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task AddMultipleBooks_ShouldAddAll()
        {
            var tokenResponse = await api.GenerateTokenAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser2",
                Password = "StrongPass123!"
            });

            var token = $"Bearer {tokenResponse.Token}";

            var response = await api.AddBooksAsync(
                new AddBooksRequestDTO
                {
                    UserId = "7be19e22-abe1-4d61-855c-a529cfcba4cb",
                    CollectionOfIsbns = new List<BookDTO>
                    {
                new BookDTO { isbn = "9781449325862" },
                new BookDTO { isbn = "9781449331818" }
                    }
                },
                token
            );

            response.Books.Should().HaveCount(2);
            response.Books.Should().Contain(b => b.isbn == "9781449331818");
        }

        [Test]
        public void AddBook_WithoutToken_ShouldReturnUnauthorized()
        {
            Func<Task> act = async () =>
            await api.AddBooksAsync(
                new AddBooksRequestDTO
                {
                    UserId = "7be19e22-abe1-4d61-855c-a529cfcba4cb",
                    CollectionOfIsbns = new List<BookDTO>
                    {
                        new BookDTO { isbn = "9781449325862" }
                    }
                },
                token: null
            );

            act.Should().ThrowAsync<ApiException>()
                .Where(e => e.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task AddInvalidBook_ShouldReturnError()
        {
            var tokenResponse = await api.GenerateTokenAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser2",
                Password = "StrongPass123!"
            });

            var token = $"Bearer {tokenResponse.Token}";

            Func<Task> act = async () =>
            await api.AddBooksAsync(
                new AddBooksRequestDTO
                {
                    UserId = "7be19e22-abe1-4d61-855c-a529cfcba4cb",
                    CollectionOfIsbns = new List<BookDTO>
                    {
                        new BookDTO { isbn = "INVALID_ISBN" }
                    }
                },
                token
            );

            act.Should().ThrowAsync<ApiException>()
                .Where(e => e.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteAllBooks_ShouldLeaveUserEmpty()
        {
            var tokenResponse = await api.GenerateTokenAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser2",
                Password = "StrongPass123!"
            });

            var token = $"Bearer {tokenResponse.Token}";

            // Получаем пользователя
            var user = await api.GetUserAsync("7be19e22-abe1-4d61-855c-a529cfcba4cb", token);

            // Удаляем каждую книгу
            foreach (var book in user.Books)
            {
                await api.DeleteBookAsync("7be19e22-abe1-4d61-855c-a529cfcba4cb", book.isbn, token);
            }

            // Проверяем, что книг нет
            var updatedUser = await api.GetUserAsync("7be19e22-abe1-4d61-855c-a529cfcba4cb", token);

            updatedUser.Books.Should().BeEmpty();
        }

        [Test]
        public async Task Login_ShouldReturnUserId()
        {
            var response = await api.LoginUserAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser2",
                Password = "StrongPass123!"
            });

            response.UserId.Should().NotBeNullOrEmpty();
            response.Username.Should().Be("OlgaTestUser2");

            TestContext.WriteLine($"UserId = {response.UserId}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Interfaces;
using Lection11_Tests.Models.BookStore;
using Lection11_Tests.RetryDemonstration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using FluentAssertions;
using Lection11_Tests.ForUI.Pages.DemoQABookStore;
using Microsoft.Playwright;

namespace Lection11_Tests.Tests.UITests
{
    [TestFixture]
    public class DemoQABookStoreTest : BaseTest
    {
        private IBookStoreApi Api;
        private string BookName = "Git Pocket Guide";


        [SetUp]
        public async Task Setup()
        {
            var services = new ServiceCollection();
            services
                .AddRefitClient<IBookStoreApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://demoqa.com");
                });
            var provider = services.BuildServiceProvider();
            Api = provider.GetRequiredService<IBookStoreApi>();
            //можно добавить метод который удаляет книги
            //нужно хранить в самом автотесте, если другие тесты этого класса эти шаги иметь не должны
            string isbn = "9781449325862";
            string userId = await GetUserIdAsync();
            string token = await GetTokenAsync();
            var response = await RetryUtils.RetryForApi<AddBooksResponseDTO>(
                () => AddBookToUserAsync(userId, token, isbn),
                TimeSpan.FromMilliseconds(30000), TimeSpan.FromMilliseconds(5000));
            response.Books.Should().ContainSingle(b => b.isbn == isbn);
        }

        [Test]
        public async Task DeleteBookUITest()
        {
            LoginPage loginPage = new LoginPage(Page);
            await loginPage.OpenBookStorPageAsync();
            var stateOfLoginPage = await loginPage.IsLoginInBookStoreLabelVisibleAsync();
            Assert.IsTrue(stateOfLoginPage, "Страница логина не открылась");
            await loginPage.LoginUserToBookStoreAsync("OlgaTestUser2", "StrongPass123!");
            BooksPage booksPage = new BooksPage(Page);
            var stateOfBooksPage = await booksPage.IsBooksLabelVisibleAsync();
            Assert.IsTrue(stateOfBooksPage, "Страница с книгами не открыта");
            var stateOfBook = await booksPage.IsBookVisibleAsync(BookName);
            Assert.IsTrue(stateOfBook, $"Книга {BookName} отсутствует на странице");

            //alert
            IDialog actualDialog = null;
            //подписываемся на событие появления алерта
            //Page.Dialog - это событие
            //когда алерт появится - выполни этот код
            Page.Dialog += async (_, dialog) =>
            {
                actualDialog = dialog;
                await actualDialog.AcceptAsync();
            };

            await booksPage.ClickDeleteBookButtonAsync(BookName);
            DeleteBookPopup deleteBookPopup = new DeleteBookPopup(Page);
            await deleteBookPopup.ClickButtonInPopup("OK");

            var stateOfBookAfterDelete = await booksPage.IsBookVisibleAsync(BookName);
            Assert.IsFalse(stateOfBookAfterDelete, $"Книга {BookName} присутствует на странице");
        }

        private async Task<string> GetUserIdAsync()
        {
            var response = await Api.LoginUserAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser2",
                Password = "StrongPass123!"
            });
            return response.UserId;
        }

        private async Task<string> GetTokenAsync()
        {
            var tokenResponse = await Api.GenerateTokenAsync(new LoginRequestDTO
            {
                UserName = "OlgaTestUser2",
                Password = "StrongPass123!"
            });

            return $"Bearer {tokenResponse.Token}";
        }

        public async Task<AddBooksResponseDTO> AddBookToUserAsync(string userId, string token, string isbn)
        {
            return await Api.AddBooksAsync(
                new AddBooksRequestDTO
                {
                    UserId = userId,
                    CollectionOfIsbns = new List<BookDTO>
                    {
                    new BookDTO { isbn = isbn }
                    }
                },
                token
            );
        }
    }
}
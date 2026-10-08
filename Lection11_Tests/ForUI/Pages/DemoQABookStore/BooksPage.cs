using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.DemoQABookStore
{
    public class BooksPage
    {
        private readonly IPage Page;
        private ILocator BooksLabel => Page.Locator("//label[contains(text(), 'Books')]");
        private ILocator BooksName(string booksName) => Page.Locator($"//span//a[text()='{booksName}']");
        private ILocator DeleteBookButton(string bookName) => Page.Locator("//tr")
            .Filter(new() { HasText = $"{bookName}" })
            .GetByTitle("Delete");

        public BooksPage(IPage page)
        {
            Page = page;
        }

        public async Task<bool> IsBooksLabelVisibleAsync()
        {
            return await BooksLabel.IsVisibleAsync();
        }

        public async Task<bool> IsBookVisibleAsync(string bookName)
        {
            return await BooksName(bookName).IsVisibleAsync();
        }

        public async Task ClickDeleteBookButtonAsync(string bookName)
        {
            await DeleteBookButton(bookName).ClickAsync();
        }
    }
}

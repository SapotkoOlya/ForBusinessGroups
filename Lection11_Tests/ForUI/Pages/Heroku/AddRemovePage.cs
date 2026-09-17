using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.Heroku
{
    public class AddRemovePage
    {
        private readonly IPage Page;
        private ILocator Button(string buttonName) => Page.GetByRole(AriaRole.Button, new() { Name = $"{buttonName}" });

        public AddRemovePage(IPage page)
        {
            Page = page;
        }

        public async Task OpenAddRemovePageAsync()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/add_remove_elements/");
        }

        public async Task CheckPageOpenAsync()
        {
            await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/add_remove_elements/");
        }

        public async Task ClickButtonByNameAsync(string buttonName)
        {
            await Button(buttonName).ClickAsync();
        }

        public async Task CheckNumberOfButtonAsync(string buttonName, int count)
        {
            await Assertions.Expect(Button(buttonName)).ToHaveCountAsync(count);
        }

        public async Task ClickButtonByNameAndNumberAsync(string buttonName, int number)
        {
            await Button(buttonName).Nth(number - 1).ClickAsync();
        }
    }
}

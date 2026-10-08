using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.DemoQABookStore
{
    public class LoginPage
    {
        private readonly IPage Page;
        private ILocator LoginInBookStoreLabel => Page.Locator("//h5[text()='Login in Book Store']");
        private ILocator LoginTextBox => Page.Locator("#userName");
        private ILocator PassTextBox => Page.Locator("#password");
        private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });

        public LoginPage(IPage page)
        {
            Page = page;
        }

        public async Task OpenBookStorPageAsync()
        {
            await Page.GotoAsync("https://demoqa.com/login");
        }

        public async Task<bool> IsLoginInBookStoreLabelVisibleAsync()
        {
            return await LoginInBookStoreLabel.IsVisibleAsync();
        }

        public async Task LoginUserToBookStoreAsync(string userName, string pass)
        {
            await LoginTextBox.FillAsync(userName);
            await PassTextBox.FillAsync(pass);
            await LoginButton.ClickAsync();
        }
    }
}

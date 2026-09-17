using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.Heroku
{
    public class LoginPage
    {
        private readonly IPage Page;

        private ILocator UserNameTextBox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
        private ILocator PasswordTextBox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
        private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });
        private ILocator ErrorMessageLabel => Page.Locator("#flash");

        public LoginPage(IPage page)
        {
            Page = page;
        }

        public async Task OpenLoginPageAsync()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/login");
        }

        public async Task FillLoginFormAsync(string username, string password)
        {
            await UserNameTextBox.FillAsync(username);
            await PasswordTextBox.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        public async Task<string> GetTextFromErrorLabelAsync()
        {
            return await ErrorMessageLabel.TextContentAsync();
        }
    }
}

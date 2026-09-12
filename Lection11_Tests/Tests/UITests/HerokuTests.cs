using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;

namespace Lection11_Tests.Tests.UITests
{
    public class HerokuTests : BaseTest
    {
        [Test]
        public async Task FormAuthentication()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/login");
            var userNameTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Username"});
            await userNameTextBox.FillAsync("wrong");
            var passTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
            //var passTextBox = await Page.QuerySelectorAsync("#password");
            //var passTextBox = Page.Locator("#password");
            await passTextBox.FillAsync("wrong");
            var loginButton = Page.GetByRole(AriaRole.Button, new() { Name = "Login" });
            await loginButton.ClickAsync();
            var errorMessageLabel = Page.Locator("//div[@id='flash']");
            var errorMessage = await errorMessageLabel.TextContentAsync();
            errorMessage.Should().Contain("Your username is invalid!");
        }

    }
}

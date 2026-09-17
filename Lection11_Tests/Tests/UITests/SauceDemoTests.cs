using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;

namespace Lection11_Tests.Tests.UITests
{
    public class SauceDemoTests : BaseTest
    {
        [Test]
        public async Task SuccessLogin()
        {
            await Page.GotoAsync("https://www.saucedemo.com/");
            var loginInput = Page.Locator("//input[@id='user-name']");
            await loginInput.FillAsync("standard_user");
            var passwordInput = Page.GetByRole(AriaRole.Textbox, new() { Name = "password" });
            await passwordInput.FillAsync("secret_sauce");
            var loginButton = Page.Locator("//input[@id='login-button']");
            await loginButton.ClickAsync();

            var checkMessage = Page.Locator("//span[text()='Products']");
            //#1
            var state = await checkMessage.IsVisibleAsync();
            state.Should().BeTrue();
            //#2
            await Assertions.Expect(checkMessage).ToBeVisibleAsync();


            await Page.Locator(".inventory_item")
                .Filter(new() { HasText = "Sauce Labs Fleece Jacket" })
                .GetByRole(AriaRole.Button, new() { Name = "Add to cart" })
                .ClickAsync();

            await Page.Locator(".inventory_item")
                .Filter(new() { HasText = "Sauce Labs Bolt T-Shirt" })
                .GetByRole(AriaRole.Button, new() { Name = "Add to cart" })
                .ClickAsync();

            await Page.Locator(".shopping_cart_link").ClickAsync();
            var cartList = await Page.Locator(".cart_list").InnerTextAsync();
        }
    }
}

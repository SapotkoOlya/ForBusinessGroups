using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Lection11_Tests.Tests.UITests
{
    public class HerokuTests : BaseTest
    {
        [Test]
        public async Task FormAuthentication()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/login");
            var userNameTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
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

        [Test]
        //стандартный дропдаун с select и option и value
        public async Task DropDown()
        {
            // Открываем страницу
            await Page.GotoAsync("https://the-internet.herokuapp.com/dropdown");
            await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/dropdown");
            var dropdown = Page.Locator("#dropdown");
            await Assertions.Expect(dropdown).ToBeVisibleAsync();
            await dropdown.SelectOptionAsync("1"); //value в верстке
            //#1
            await Assertions.Expect(dropdown).ToHaveValueAsync("1");
            //#2
            var selected = dropdown.Locator("option:checked"); //запомнить ситуацию
            await Assertions.Expect(selected).ToHaveTextAsync("Option 1");
            await dropdown.SelectOptionAsync("2"); //value в верстке
            //#1
            await Assertions.Expect(dropdown).ToHaveValueAsync("2");
            //#2
            await Assertions.Expect(selected).ToHaveTextAsync("Option 2");
        }

        [Test]
        //нестандартный дропдаун
        public async Task Should_Select_Sub_Item()
        {
            await Page.GotoAsync("https://demoqa.com/select-menu");
            var dropdown = Page.Locator("#withOptGroup");
            await dropdown.ClickAsync();
            var option = Page.GetByText("Group 1, option 1");
            await option.ClickAsync();
            var text = dropdown.TextContentAsync();
            await Assertions.Expect(dropdown).ToContainTextAsync("Group 1, option 1");
        }

        [Test]
        public async Task CheckBoxes()
        {
            // Открываем страницу
            await Page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
            await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/checkboxes");

            // Локаторы чекбоксов
            var first = Page.Locator("input[type='checkbox']").Nth(0);
            var second = Page.Locator("input[type='checkbox']").Nth(1);

            // --- Проверка дефолтного состояния ---
            await Assertions.Expect(first).Not.ToBeCheckedAsync();
            await Assertions.Expect(second).ToBeCheckedAsync();

            // --- ДЕЙСТВИЕ 1: отщёлкнуть второй чекбокс ---
            await second.UncheckAsync();

            // Проверка после действия
            await Assertions.Expect(second).Not.ToBeCheckedAsync();

            // --- ДЕЙСТВИЕ 2: щёлкнуть первый чекбокс ---
            await first.CheckAsync();

            // Проверка после действия
            await Assertions.Expect(first).ToBeCheckedAsync();

            // --- ДЕЙСТВИЕ 3: вернуть второй обратно ---
            await second.CheckAsync();

            // Проверка после действия
            await Assertions.Expect(second).ToBeCheckedAsync();
        }

        [Test]
        public async Task AddRemoveElements()
        {
            // Открываем страницу
            await Page.GotoAsync("https://the-internet.herokuapp.com/add_remove_elements/");

            // Проверка title
            await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");

            // Проверка URL
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/add_remove_elements/");

            // Локатор кнопки Add Element
            var addButton = Page.GetByRole(AriaRole.Button, new() { Name = "Add Element" });

            // Проверка видимости кнопки Add Element
            await Assertions.Expect(addButton).ToBeVisibleAsync();

            // Локатор всех Delete-кнопок
            var deleteButtons = Page.Locator("button.added-manually");

            // --- ДЕЙСТВИЕ 1: добавить первую кнопку ---
            await addButton.ClickAsync();

            // Проверка: появилась 1 кнопка Delete
            await Assertions.Expect(deleteButtons).ToHaveCountAsync(1);

            // --- ДЕЙСТВИЕ 2: добавить вторую кнопку ---
            await addButton.ClickAsync();

            // Проверка: теперь их 2
            await Assertions.Expect(deleteButtons).ToHaveCountAsync(2);

            // --- ДЕЙСТВИЕ 3: удалить одну кнопку ---
            await deleteButtons.Nth(0).ClickAsync();

            // Проверка: осталась 1 кнопка
            await Assertions.Expect(deleteButtons).ToHaveCountAsync(1);
        }

        [Test]
        public async Task StatusCodes()
        {
            // Открываем страницу
            await Page.GotoAsync("https://the-internet.herokuapp.com/status_codes");

            // Проверка title и URL
            await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes");

            // Локаторы ссылок
            var link200 = Page.GetByRole(AriaRole.Link, new() { Name = "200" });
            var link301 = Page.GetByRole(AriaRole.Link, new() { Name = "301" });
            var link404 = Page.GetByRole(AriaRole.Link, new() { Name = "404" });
            var link500 = Page.GetByRole(AriaRole.Link, new() { Name = "500" });

            // --- 1. Переход в 200 ---
            await link200.ClickAsync();
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes/200");
            await Assertions.Expect(Page.Locator("p")).ToContainTextAsync("200");

            // Возврат назад браузерным методом
            await Page.GoBackAsync();
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes");

            // --- 2. Переход в 301 ---
            await link301.ClickAsync();
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes/301");
            await Assertions.Expect(Page.Locator("p")).ToContainTextAsync("301");

            await Page.GoBackAsync();
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes");

            // --- 3. Переход в 404 ---
            await link404.ClickAsync();
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes/404");
            await Assertions.Expect(Page.Locator("p")).ToContainTextAsync("404");

            await Page.GoBackAsync();
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes");

            // --- 4. Переход в 500 ---
            await link500.ClickAsync();
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes/500");
            await Assertions.Expect(Page.Locator("p")).ToContainTextAsync("500");

            await Page.GoBackAsync();
            await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/status_codes");
        }
    }
}

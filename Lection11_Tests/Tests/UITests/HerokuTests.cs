using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Lection11_Tests.ForUI.Pages.Heroku;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Lection11_Tests.Tests.UITests
{
    public class HerokuTests : BaseTest
    {
        [Test]
        public async Task FormAuthentication()
        {
            LoginPage loginPage = new LoginPage(Page);
            await loginPage.OpenLoginPageAsync();
            await loginPage.FillLoginFormAsync("wrong", "wrong");
            string errorMessage = await loginPage.GetTextFromErrorLabelAsync();
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

            var text = await dropdown.TextContentAsync();
            await Assertions.Expect(dropdown).ToContainTextAsync("Group 1, option 1");
        }

        [Test]
        public async Task CheckBoxes_ID123()
        {
            CheckBoxesPage checkBoxesPage = new CheckBoxesPage(Page);
            // Открываем страницу
            await checkBoxesPage.OpenCheckboxesPageAsync();
            await checkBoxesPage.CheckPageOpenAsync();

            // --- Проверка дефолтного состояния ---
            bool stateOfCheckbox1 = await checkBoxesPage.GetStateOfCheckboxAsync(1);
            bool stateOfCheckbox2 = await checkBoxesPage.GetStateOfCheckboxAsync(2);
            stateOfCheckbox1.Should().BeFalse();
            stateOfCheckbox2.Should().BeTrue();

            // --- ДЕЙСТВИЕ 1: отщёлкнуть второй чекбокс ---
            await checkBoxesPage.UncheckCheckboxAsync(2);

            // Проверка после действия
            stateOfCheckbox2 = await checkBoxesPage.GetStateOfCheckboxAsync(2);
            stateOfCheckbox2.Should().BeFalse();

            // --- ДЕЙСТВИЕ 2: щёлкнуть первый чекбокс ---
            await checkBoxesPage.CheckCheckboxAsync(1);
            stateOfCheckbox1 = await checkBoxesPage.GetStateOfCheckboxAsync(1);
            stateOfCheckbox1.Should().BeTrue();

            // --- ДЕЙСТВИЕ 3: вернуть второй обратно ---
            await checkBoxesPage.CheckCheckboxAsync(2);
            stateOfCheckbox2 = await checkBoxesPage.GetStateOfCheckboxAsync(2);
            stateOfCheckbox2.Should().BeTrue();      
        }

        [Test]
        public async Task AddRemoveElements()
        {
            // Открываем страницу
            AddRemovePage addRemovePage = new AddRemovePage(Page);
            await addRemovePage.OpenAddRemovePageAsync();

            // Проверка страницы
            await addRemovePage.CheckPageOpenAsync();

            // ДЕЙСТВИЕ 1: добавить первую кнопку 
            await addRemovePage.ClickButtonByNameAsync("Add Element");

            // Проверка: появилась 1 кнопка Delete
            await addRemovePage.CheckNumberOfButtonAsync("Delete", 1); 
            
            // ДЕЙСТВИЕ 2: добавить вторую кнопку 
            await addRemovePage.ClickButtonByNameAsync("Add Element"); 
            
            // Проверка: теперь их 2
            await addRemovePage.CheckNumberOfButtonAsync("Delete", 2); 

            //  ДЕЙСТВИЕ 3: удалить одну кнопку 
            await addRemovePage.ClickButtonByNameAndNumberAsync("Delete", 2); 
            
            // Проверка: осталась 1 кнопка
            await addRemovePage.CheckNumberOfButtonAsync("Delete", 1);
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

        [Test]
        public async Task LeftBottomFrameTest()
        {
            FramesPage framesPage = new FramesPage(Page);
            await framesPage.OpenFramesPageAsync();
            await framesPage.ClickNestedFramesLinkAsync();
            NestedFramesPage nestedFramesPage = new NestedFramesPage(Page);
            var textFromLeftFrame = await nestedFramesPage.GetTextFromLeftFrameAsync();
            textFromLeftFrame.Should().Contain("LEFT");
            var textFromBottomFrame = await nestedFramesPage.GetTextFromBottomFrameAsync();
            textFromBottomFrame.Should().Contain("BOTTOM");
        }

        [Test]
        public async Task MultipleWindowTest()
        {
            MultipleWindowPage multipleWindowPage = new MultipleWindowPage(Page);
            await multipleWindowPage.OpenMultipleWindowPageAsync();
            var newWindow = await multipleWindowPage.OpenNewWindowAsync();
            await Assertions.Expect(newWindow.Locator("h3")).ToHaveTextAsync("New Window");
            newWindow.Url.Should().Contain("windows/new");
        }

        [Test]
        public async Task JsAlertTest()
        {
            JavaScriptAlertsPage javaScriptAlertsPage = new JavaScriptAlertsPage(Page);
            await javaScriptAlertsPage.OpenAlertsPageAsync();

            IDialog actualDialog = null;
            //подписываемся на событие появления алерта
            //Page.Dialog - это событие
            //когда алерт появится - выполни этот код
            Page.Dialog += async (_, dialog) =>
            {
                actualDialog = dialog;              
                await actualDialog.AcceptAsync();
            };

            await javaScriptAlertsPage.ClickJsAlertButtonAsync();

            actualDialog.Should().NotBeNull();
            actualDialog.Type.Should().Be("alert");
            actualDialog.Message.Should().Be("I am a JS Alert");

            var resultText = await javaScriptAlertsPage.GetResultTextAsync();
            resultText.Should().Be("You successfully clicked an alert");
        }

        [Test]
        public async Task JsPrompttTest()
        {
            JavaScriptAlertsPage javaScriptAlertsPage = new JavaScriptAlertsPage(Page);
            await javaScriptAlertsPage.OpenAlertsPageAsync();

            var textForAlert = "The best prompt!";

            IDialog actualDialog = null;
            //подписываемся на событие появления алерта
            //Page.Dialog - это событие
            //когда алерт появится - выполни этот код
            Page.Dialog += async (_, dialog) =>
            {
                actualDialog = dialog;
                await actualDialog.AcceptAsync(textForAlert);
            };

            await javaScriptAlertsPage.ClickJsPromptButtonAsync();

            actualDialog.Should().NotBeNull();
            actualDialog.Type.Should().Be("prompt");
            actualDialog.Message.Should().Be("I am a JS prompt");
            var resultText = await javaScriptAlertsPage.GetResultTextAsync();
            resultText.Should().Be("You entered: The best prompt!");
        }

        [Test]
        public async Task JsConfirmAccept()
        {
            JavaScriptAlertsPage javaScriptAlertsPage = new JavaScriptAlertsPage(Page);
            await javaScriptAlertsPage.OpenAlertsPageAsync();

            IDialog? actualDialog = null;
            Page.Dialog += async (_, dialog) =>
            {
                actualDialog = dialog;
                await actualDialog.AcceptAsync();
            };

            await javaScriptAlertsPage.ClickJsConfirmButtonAsync();
            actualDialog.Should().NotBeNull();
            actualDialog!.Type.Should().Be("confirm");
            actualDialog.Message.Should().Be("I am a JS Confirm");
            var result = await javaScriptAlertsPage.GetResultTextAsync();
            result.Should().Be("You clicked: Ok");
        }

        [Test]
        public async Task JsConfirmDismiss()
        {
            JavaScriptAlertsPage javaScriptAlertsPage = new JavaScriptAlertsPage(Page);
            await javaScriptAlertsPage.OpenAlertsPageAsync();

            IDialog? actualDialog = null;
            Page.Dialog += async (_, dialog) =>
            {
                actualDialog = dialog;
                await actualDialog.DismissAsync();
            };

            await javaScriptAlertsPage.ClickJsConfirmButtonAsync();
            actualDialog.Should().NotBeNull();
            actualDialog!.Type.Should().Be("confirm");
            actualDialog.Message.Should().Be("I am a JS Confirm");
            var result = await javaScriptAlertsPage.GetResultTextAsync();
            result.Should().Be("You clicked: Cancel");
        }

        [Test]
        public async Task JsPromptWithEmptyValue()
        {
            JavaScriptAlertsPage javaScriptAlertsPage = new JavaScriptAlertsPage(Page);
            await javaScriptAlertsPage.OpenAlertsPageAsync();

            IDialog? actualDialog = null;
            Page.Dialog += async (_, dialog) =>
            {
                actualDialog = dialog;
                await actualDialog.AcceptAsync(string.Empty);
            };

            await javaScriptAlertsPage.ClickJsPromptButtonAsync();

            actualDialog.Should().NotBeNull();
            actualDialog!.Type.Should().Be("prompt");
            actualDialog.Message.Should().Be("I am a JS prompt");
            var result = await javaScriptAlertsPage.GetResultTextAsync();
            result.Should().Be("You entered:");
        }

        [Test]
        public async Task JsPromptDismiss()
        {
            JavaScriptAlertsPage javaScriptAlertsPage = new JavaScriptAlertsPage(Page);
            await javaScriptAlertsPage.OpenAlertsPageAsync();

            IDialog? actualDialog = null;
            Page.Dialog += async (_, dialog) =>
            {
                actualDialog = dialog;
                await actualDialog.DismissAsync();
            };

            await javaScriptAlertsPage.ClickJsPromptButtonAsync();
            actualDialog.Should().NotBeNull();
            actualDialog!.Type.Should().Be("prompt");
            actualDialog.Message.Should().Be("I am a JS prompt");
            var result = await javaScriptAlertsPage.GetResultTextAsync();
            result.Should().Be("You entered: null");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.Heroku
{
    public class JavaScriptAlertsPage
    {
        private readonly IPage Page;
        private ILocator JsAlertButton => Page.GetByRole(AriaRole.Button, new() { Name = "Click for JS Alert" });
        private ILocator JsPromptButton => Page.GetByRole(AriaRole.Button, new() { Name = "Click for JS Prompt" });
        private ILocator JsConfirmButton => Page.GetByRole(AriaRole.Button, new() { Name = "Click for JS Confirm" });
        private ILocator ResultLabel => Page.Locator("#result");

        public JavaScriptAlertsPage(IPage page)
        {
            Page = page;
        }

        public async Task OpenAlertsPageAsync()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/javascript_alerts");
        }

        public async Task ClickJsAlertButtonAsync()
        {
            await JsAlertButton.ClickAsync();
        }

        public async Task<string> GetResultTextAsync()
        {
            return await ResultLabel.InnerTextAsync();
        }

        public async Task ClickJsPromptButtonAsync()
        {
            await JsPromptButton.ClickAsync();
        }

        public async Task ClickJsConfirmButtonAsync()
        {
            await JsConfirmButton.ClickAsync();
        }
    }
}
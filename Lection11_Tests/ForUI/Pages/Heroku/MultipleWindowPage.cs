using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.Heroku
{
    public class MultipleWindowPage
    {
        private readonly IPage Page;
        private ILocator ClickHereLink => Page.GetByRole(AriaRole.Link, new() { Name = "Click Here" });

        public MultipleWindowPage(IPage page)
        {
            Page = page;
        }

        public async Task OpenMultipleWindowPageAsync()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/windows");
        }

        public async Task<IPage> OpenNewWindowAsync()
        {
            return await Page.RunAndWaitForPopupAsync(async () =>
            await ClickHereLink.ClickAsync()
            );
        }
    }
}

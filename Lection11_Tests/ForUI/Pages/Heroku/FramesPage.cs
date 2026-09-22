using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.Heroku
{
    public class FramesPage
    {
        private readonly IPage Page;
        private ILocator NestedFramesLink => Page.GetByRole(AriaRole.Link, new() { Name = "Nested Frames" });

        public FramesPage(IPage page)
        {
            Page = page;
        }

        public async Task OpenFramesPageAsync()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/frames");
        }

        public async Task ClickNestedFramesLinkAsync()
        {
            await NestedFramesLink.ClickAsync();
        }
    }
}

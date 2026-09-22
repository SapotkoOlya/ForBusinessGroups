using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.Heroku
{
    public class NestedFramesPage
    {
        private readonly IPage Page;
        private ILocator LeftFrame => Page.FrameLocator("frame[name='frame-top']")
            .FrameLocator($"frame[name='frame-top']")
            .Locator("body");
        private ILocator BottomFrame => Page.FrameLocator("frame[name='frame-bottom']")
            .Locator("body");

        public NestedFramesPage(IPage page)
        {
            Page = page;
        }

        public async Task<string> GetTextFromLeftFrameAsync()
        {
            return await LeftFrame.InnerTextAsync();
        }

        public async Task<string> GetTextFromBottomFrameAsync()
        {
            return await BottomFrame.InnerTextAsync();
        }
    }
}

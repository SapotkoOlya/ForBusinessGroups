using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Framework
{
    public class PlaywrightFixture : IAsyncDisposable
    {
        public IPlaywright Playwright { get; private set; }
        public IBrowser Browser { get; private set; }

        public async Task InitializeAsync()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 3000,
                Args = new[] { "--start-maximized" }
            });
        }

        public async ValueTask DisposeAsync()
        {
            if(Browser!=null)
            {
                await Browser.CloseAsync();
            }
            Playwright?.Dispose();
        }
    }
}

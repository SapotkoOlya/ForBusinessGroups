using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Framework
{
    public static class BrowserFactory
    {
        public static async Task<(IBrowser Browser, IPlaywright Playwright)> CreateAsync(BrowserType type)
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 3000,
                Args = new[] { "--start-maximized" }
            };

            IBrowser browser = type switch
            {
                BrowserType.Chromium => await playwright.Chromium.LaunchAsync(launchOptions),
                BrowserType.Firefox => await playwright.Firefox.LaunchAsync(launchOptions),
                BrowserType.WebKit => await playwright.Webkit.LaunchAsync(launchOptions),
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };

            return (browser, playwright);
        }
    }

    public enum BrowserType
    {
        Chromium,
        Firefox,
        WebKit
    }
}
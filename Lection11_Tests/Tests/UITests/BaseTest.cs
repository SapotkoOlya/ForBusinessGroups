using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Lection11_Tests.ForUI.Framework;


namespace Lection11_Tests.Tests.UITests
{
    public class BaseTest
    {
        protected IPage Page { get; private set; }
        protected PlaywrightFixture Fixture { get; }

        protected BaseTest()
        {
            Fixture = new PlaywrightFixture();
            Fixture.InitializeAsync().Wait();
        }

        [SetUp]
        public async Task SetUp()
        {
            Page = await Fixture.Browser.NewPageAsync(new BrowserNewPageOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
        }

        [OneTimeTearDown]
        public async Task GlobalTearDown()
        {
            await Fixture.DisposeAsync();
        }

        /*protected ILocatorAssertions Expect(ILocator locator)
        {
            return Assertions.Expect(locator);
        }

        protected IPageAssertions Expect(IPage page)
        {
            return Assertions.Expect(page);
        }*/
    }
}

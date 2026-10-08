using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Pages.DemoQABookStore
{
    public class DeleteBookPopup
    {
        private readonly IPage Page;
        private ILocator DeleteOrNotButton(string option) => Page.Locator($"//button[text()='{option}']");

        public DeleteBookPopup(IPage page)
        {
            Page = page;
        }

        public async Task ClickButtonInPopup(string option)
        {
            await DeleteOrNotButton(option).ClickAsync();
        }
    }
}

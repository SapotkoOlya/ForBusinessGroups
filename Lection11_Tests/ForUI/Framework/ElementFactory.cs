using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Lection11_Tests.ForUI.Framework
{
    public static class ElementFactory
    {
        // Создаём «умную» кнопку с готовыми методами
        public static ButtonComponent CreateButton(IPage page, string testId)
        {
            var locator = page.GetByTestId(testId);
            return new ButtonComponent(locator);
        }

        // Создаём поле ввода
        public static InputComponent CreateInput(IPage page, string testId)
        {
            var locator = page.GetByTestId(testId);
            return new InputComponent(locator);
        }

        // Создаём чекбокс
        public static CheckboxComponent CreateCheckbox(IPage page, string testId)
        {
            var locator = page.GetByTestId(testId);
            return new CheckboxComponent(locator);
        }
    }

    // Обертки-компоненты с удобными методами
    public class ButtonComponent
    {
        private readonly ILocator Locator;

        public ButtonComponent(ILocator locator) => Locator = locator;

        public Task ClickAsync() => Locator.ClickAsync();
        public Task<string> GetTextAsync() => Locator.InnerTextAsync();
    }

    public class InputComponent
    {
        private readonly ILocator Locator;

        public InputComponent(ILocator locator) => Locator = locator;

        public Task FillAsync(string value) => Locator.FillAsync(value);
        public Task ClearAsync() => Locator.ClearAsync();
        public Task<string> GetValueAsync() => Locator.InputValueAsync();
    }

    public class CheckboxComponent
    {
        private readonly ILocator Locator;

        public CheckboxComponent(ILocator locator) => Locator = locator;

        public Task CheckAsync() => Locator.CheckAsync();
        public Task UncheckAsync() => Locator.UncheckAsync();
        public Task<bool> IsCheckedAsync() => Locator.IsCheckedAsync();
    }
}

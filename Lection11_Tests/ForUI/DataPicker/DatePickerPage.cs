using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;

namespace Lection11_Tests.ForUI.DataPicker
{
    public class DatePickerPage
    {
        private readonly IPage Page;

        // Локаторы
        private ILocator SelectDateInput => Page.Locator("#datePickerMonthYearInput");
        private ILocator DateAndTimeInput => Page.Locator("#dateAndTimePickerInput");

        // Локаторы внутри календаря
        private ILocator MonthYearLabel => Page.Locator(".react-datepicker__current-month");
        private ILocator PrevMonthButton => Page.Locator(".react-datepicker__navigation-icon--previous");
        private ILocator NextMonthButton => Page.Locator(".react-datepicker__navigation-icon--next");
        private ILocator YearDropdown => Page.Locator(".react-datepicker__year-read-view");
        private ILocator MonthDropdown => Page.Locator(".react-datepicker__month-read-view");
        private ILocator AllDayCells => Page.Locator(".react-datepicker__day");
        // ── Локаторы времени ──
        private ILocator AllTimeItems => Page.Locator(".react-datepicker__time-list-item");

        // ── Локаторы для динамического выбора ──
        private ILocator DayCell(int day) =>
            Page.Locator(
                $".react-datepicker__day--{day:D3}" +
                ":not(.react-datepicker__day--outside-month)");

        private ILocator YearOption(int year) =>
            Page.Locator($".react-datepicker__year-option").Filter(new() { HasText = year.ToString() });

        private ILocator MonthOption(string month) =>
            Page.Locator(".react-datepicker__month-option").Filter(new() { HasText = month });

        private ILocator TimeOption(string time) =>
            Page.Locator(".react-datepicker__time-list-item").Filter(new() { HasText = time });

        public DatePickerPage(IPage page)
        {
            Page = page;
        }

        public async Task OpenAsync()
        {
            await Page.GotoAsync("https://demoqa.com/date-picker");
        }

        /// Открывает первый пикер и выбирает день по числу.
        public async Task SelectDateAsync(int day)
        {
            await SelectDateInput.ClickAsync();
            // Ждём появления календаря
            await AllDayCells.First.WaitForAsync(new() { Timeout = 10_000 });
            // Кликаем по нужному дню
            await DayCell(day).ClickAsync(new() { Force = true });
        }

        /// Открывает второй пикер, выбирает месяц, год, день и время.
        public async Task SelectDateAndTimeAsync(string month, int year, int day, string time)
        {
            await DateAndTimeInput.ClickAsync();

            await YearDropdown.ClickAsync();
            await YearOption(year).ClickAsync();

            await MonthDropdown.ClickAsync();
            await MonthOption(month).ClickAsync();

            await AllDayCells.First.WaitForAsync(new() { Timeout = 10_000 });
            await DayCell(day).ClickAsync(new() { Force = true });

            // --- Время ---
            await AllTimeItems.First.WaitForAsync(new() { Timeout = 10_000 });
            await TimeOption(time).ClickAsync(new() { Force = true });
        }

        /// Возвращает значение из первого инпута.
        public async Task<string> GetSelectedDateValueAsync()
        {
            return await SelectDateInput.InputValueAsync();
        }

        /// Возвращает значение из второго инпута.
        public async Task<string> GetSelectedDateAndTimeValueAsync()
        {
            return await DateAndTimeInput.InputValueAsync();
        }

        public async Task FillDateAsync(string date)
        {
            await SelectDateInput.ClickAsync();
            await SelectDateInput.PressAsync("Control+a");
            await SelectDateInput.PressAsync("Delete");
            await SelectDateInput.FillAsync(date);
            await SelectDateInput.PressAsync("Enter");
        }

        public async Task FillDateAndTimeAsync(string date)
        {
            await DateAndTimeInput.ClickAsync();
            await DateAndTimeInput.PressAsync("Control+a");
            await DateAndTimeInput.PressAsync("Delete");
            await DateAndTimeInput.FillAsync(date);
            await DateAndTimeInput.PressAsync("Enter");
        }
    }
}

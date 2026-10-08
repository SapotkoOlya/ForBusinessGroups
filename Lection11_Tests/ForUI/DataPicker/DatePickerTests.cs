using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Tests.UITests;
using Lection11_Tests.Utils;
using Lection11_Tests.Constants;

namespace Lection11_Tests.ForUI.DataPicker
{
    [TestFixture]
    public class DatePickerTests : BaseTest
    {
        private DatePickerPage DatePickerPage;

        [SetUp]
        public async Task Setup()
        {
            DatePickerPage = new DatePickerPage(Page);
            await DatePickerPage.OpenAsync();
        }

        [Test]
        public async Task ShouldSelectDateInDatePicker()
        {
            await DatePickerPage.SelectDateAsync(15);

            var selectedDate = await DatePickerPage.GetSelectedDateValueAsync();

            Assert.That(selectedDate, Contains.Substring("15"),
                $"Ожидалось, что выбранная дата будет содержать '15', но получили: {selectedDate}");
        }

        [Test]
        public async Task ShouldSelectDateAndTimeInDateTimePicker()
        {
            await DatePickerPage.SelectDateAndTimeAsync("October", 2026, 13, "09:30");
            var selectedValue = await DatePickerPage.GetSelectedDateAndTimeValueAsync();

            Assert.That(selectedValue, Contains.Substring("October"),
                $"Ожидалось, что месяц будет October, но получили: {selectedValue}");
            Assert.That(selectedValue, Contains.Substring("2026"),
                $"Ожидалось, что год будет 2026, но получили: {selectedValue}");
            Assert.That(selectedValue, Contains.Substring("9:30"),
                $"Ожидалось, что время будет 9:30, но получили: {selectedValue}");
        }

        [Test]
        public async Task ShoudTypeDateInDatePicker()
        {
            var date = DateTimeUtils.GetFutureDateString(-6, DateTimeConstants.MonthDayYearSlashFormat);
            await DatePickerPage.FillDateAsync(date);
            var selectedDate = await DatePickerPage.GetSelectedDateValueAsync();
            Assert.That(selectedDate, Is.EqualTo(date),
                $"Ожидалось, что день будет {date}, но получили {selectedDate}");
        }

        [Test]
        public async Task ShoudTypeDateAndTimeInDatePicker()
        {
            var date = DateTimeUtils.GetFutureDateString(6, DateTimeConstants.MonthDayYearTimeFormat);
            await DatePickerPage.FillDateAndTimeAsync(date);
            var selectedDate = await DatePickerPage.GetSelectedDateAndTimeValueAsync();
            Assert.That(selectedDate, Is.EqualTo(date),
                $"Ожидалось, что день будет {date}, но получили {selectedDate}");
        }
    }
}

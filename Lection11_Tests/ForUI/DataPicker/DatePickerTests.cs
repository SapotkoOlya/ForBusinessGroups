using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Tests.UITests;

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

            Assert.That(selectedDate, Is.Not.Empty, "Дата не должна быть пустой");
            Assert.That(selectedDate, Contains.Substring("15"),
                $"Ожидалось, что выбранная дата будет содержать '15', но получили: {selectedDate}");
        }

        [Test]
        public async Task ShouldSelectDateAndTimeInDateTimePicker()
        {
            await DatePickerPage.SelectDateAndTimeAsync("October", 2026, 6, "09:30");

            var selectedValue = await DatePickerPage.GetSelectedDateAndTimeValueAsync();

            Assert.That(selectedValue, Is.Not.Empty, "Значение не должно быть пустым");
            Assert.That(selectedValue, Contains.Substring("October"),
                $"Ожидалось, что месяц будет October, но получили: {selectedValue}");
            Assert.That(selectedValue, Contains.Substring("2026"),
                $"Ожидалось, что год будет 2026, но получили: {selectedValue}");
            Assert.That(selectedValue, Contains.Substring("9:30"),
                $"Ожидалось, что время будет 9:30, но получили: {selectedValue}");
        }

        [Test]
        public async Task ShouldTypeDateInDatePicker()
        {
            
            await DatePickerPage.TypeDateAsync("10/15/2026");

            var selectedDate = await DatePickerPage.GetSelectedDateValueAsync();

            Assert.That(selectedDate, Is.Not.Empty, "Дата не должна быть пустой");
            Assert.That(selectedDate, Contains.Substring("15"),
                $"Ожидалось, что день будет 15, но получили: {selectedDate}");
            Assert.That(selectedDate, Contains.Substring("10"),
                $"Ожидалось, что месяц будет 10, но получили: {selectedDate}");
            Assert.That(selectedDate, Contains.Substring("2026"),
                $"Ожидалось, что год будет 2026, но получили: {selectedDate}");
        }

        [Test]
        public async Task ShouldTypeDateAndTimeInDateTimePicker()
        {
            
            await DatePickerPage.TypeDateAndTimeAsync("October 6, 2026 9:30 AM");

            var selectedValue = await DatePickerPage.GetSelectedDateAndTimeValueAsync();

            Assert.That(selectedValue, Is.Not.Empty, "Значение не должно быть пустым");
            Assert.That(selectedValue, Contains.Substring("October"),
                $"Ожидалось, что месяц будет October, но получили: {selectedValue}");
            Assert.That(selectedValue, Contains.Substring("2026"),
                $"Ожидалось, что год будет 2026, но получили: {selectedValue}");
            Assert.That(selectedValue, Contains.Substring("9:30"),
                $"Ожидалось, что время будет 9:30, но получили: {selectedValue}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Storages.ForUI.Builders;
using Lection11_Tests.Enums;
using Lection11_Tests.Storages.ForUI.Models;

namespace Lection11_Tests.Tests.UITests
{
    [TestFixture]
    public class DemoQATest : BaseTest
    {
        [Test]
        public async Task FillStudentRegistrationForm()
        {
            await Page.GotoAsync("https://demoqa.com/automation-practice-form");

            StudentRegistrationBuilder builder = new StudentRegistrationBuilder();
            var studentData = builder.WithFirstName("Rajesh")
                .WithLastName("Kutrapalli")
                .WithGender(GenderType.Male)
                .Build();
            await FillAllFormFieldAsync(studentData);
        }

        //ЭТО В КЛАССЕ СТРАНИЦЫ, ПО ВСЕМ ПРАВИЛАМ!
        //ТУТ ЭТО ДЛЯ ДЕМОНСТРАЦИИ, КАК РАБОТАТЬ С ПАТТЕРНОМ И КАК ПЕРЕДАВАТЬ ДАННЫЕ
        public async Task FillAllFormFieldAsync(StudentRegistrationFormModel studentData)
        {
            await Page.Locator("#firstName").FillAsync(studentData.FirstName);
            await Page.Locator("#lastName").FillAsync(studentData.LastName);
        }
    }
}

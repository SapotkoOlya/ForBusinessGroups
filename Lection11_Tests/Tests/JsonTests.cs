using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Lection11_Tests.Models.ForJsonRead;
using FluentAssertions;

namespace Lection11_Tests.Tests
{
    public class JsonTests
    {
        /*private List<User> users;

        [OneTimeSetUp]
        public void SetUp()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "UsersData.json");
            string data = File.ReadAllText(path);
            var root = JsonConvert.DeserializeObject<Root>(data);
            users = root!.Data;
        }

        // 1. Количество пользователей
        [Test]
        public void Users_Count_Should_Be_10()
        {
            int count = users.Count;
            count.Should().Be(10);
        }

        // 2. Первый пользователь должен быть Alice Johnson
        [Test]
        public void First_User_Should_Be_Alice()
        {
            var firstUser = users.First();
            firstUser.Profile.FullName.Should().Be("Alice Johnson");
        }

        // 3. Проверка, что все ID уникальны
        [Test]
        public void All_Ids_Should_Be_Unique()
        {
            var ids = users.Select(u => u.Id).ToList();
            ids.Should().OnlyHaveUniqueItems();
        }

        // 4. Проверка, что есть хотя бы один премиум‑пользователь
        [Test]
        public void Should_Have_At_Least_One_Premium_User()
        {
            var premiumUsers = users.Where(u => u.Profile.Tags.Contains("premium")).ToList();
            premiumUsers.Should().NotBeEmpty();
        }

        // 5. Проверка, что все города непустые
        [Test]
        public void All_Cities_Should_Not_Be_Empty()
        {
            var cities = users.Select(u => u.Profile.Address.City).ToList();
            cities.Should().OnlyContain(c => !string.IsNullOrWhiteSpace(c));
        }

        // 6. Проверка, что есть пользователь из Stockholm
        [Test]
        public void Should_Contain_User_From_Stockholm()
        {
            var stockholmUser = users.FirstOrDefault(u => u.Profile.Address.City == "Stockholm");
            stockholmUser.Should().NotBeNull();
        }

        // 7. Проверка, что все пользователи имеют хотя бы один тег
        [Test]
        public void All_Users_Should_Have_Tags()
        {
            var tagsCounts = users.Select(u => u.Profile.Tags.Count).ToList();
            tagsCounts.Should().OnlyContain(count => count > 0);
        }

        // 8. Проверка, что возраст всех пользователей в диапазоне 18–60
        [Test]
        public void All_Ages_Should_Be_In_Range()
        {
            var ages = users.Select(u => u.Profile.Age).ToList();
            ages.Should().OnlyContain(age => age >= 18 && age <= 60);
        }

        // 9. Проверка, что у всех пользователей есть координаты
        [Test]
        public void All_Users_Should_Have_Geo()
        {
            var geos = users.Select(u => u.Profile.Address.Geo).ToList();
            geos.Should().NotContainNulls();
        }

        // 10. Проверка, что есть хотя бы один пользователь с ролью admin
        [Test]
        public void Should_Have_Admin_User()
        {
            var admins = users.Where(u => u.Roles.Contains("admin")).ToList();
            admins.Should().NotBeEmpty();
        }

        // 11. Координаты находятся в диапазоне Швеции
        [Test]
        public void GeoCoordinates_Should_Be_Within_Sweden()
        {
            // arrange
            var geos = users.Select(u => u.Profile.Address.Geo).ToList();

            // act
            bool allValid = geos.All(g =>
                g.Lat >= 55 && g.Lat <= 69 &&
                g.Lng >= 11 && g.Lng <= 24);

            // assert
            allValid.Should().BeTrue("все координаты должны соответствовать территории Швеции");
        }

        // 12. Комбинация город и фулл нейм не повторяется
        [Test]
        public void CityAndFullName_Should_Be_Unique()
        {
            // arrange
            var pairs = users
                .Select(u => $"{u.Profile.FullName}:{u.Profile.Address.City}")
                .ToList();

            // act
            bool unique = pairs.Distinct().Count() == pairs.Count;

            // assert
            unique.Should().BeTrue("имя + город не должны повторяться");
        }

        // 13. Сортировка по расстоянию до Стокгольма
        [Test]
        public void Users_Should_Be_Sorted_By_Distance_To_Stockholm()
        {
            // arrange
            var stockholm = new Geo { Lat = 59.3293, Lng = 18.0686 };

            var distances = users
                .Select(u => new
                {
                    User = u,
                    Distance = Distance(u.Profile.Address.Geo, stockholm)
                })
                .OrderBy(x => x.Distance)
                .ToList();

            // act
            var closest = distances.First();
            var farthest = distances.Last();

            // assert
            closest.User.Profile.Address.City.Should().Be("Stockholm");
            farthest.Distance.Should().BeGreaterThan(200); // Швеция большая
        }

        // 14. Валидация улиц
        [Test]
        public void Streets_Should_Be_Valid()
        {
            // arrange
            var streets = users.Select(u => u.Profile.Address.Street).ToList();

            // act
            bool hasHouseNumber = streets.All(s => s.Any(char.IsDigit));
            bool startsWithLetter = streets.All(s => char.IsLetter(s[0]));
            bool notOnlyDigits = streets.All(s => !s.All(char.IsDigit));

            // assert
            hasHouseNumber.Should().BeTrue("улица должна содержать номер дома");
            startsWithLetter.Should().BeTrue("улица должна начинаться с буквы");
            notOnlyDigits.Should().BeTrue("улица не может состоять только из цифр");
        }

        private double Distance(Geo a, Geo b)
        {
            double R = 6371; // радиус Земли
            double dLat = (b.Lat - a.Lat) * Math.PI / 180;
            double dLng = (b.Lng - a.Lng) * Math.PI / 180;

            double lat1 = a.Lat * Math.PI / 180;
            double lat2 = b.Lat * Math.PI / 180;

            double h = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            return 2 * R * Math.Asin(Math.Sqrt(h));
        }*/

    }
}

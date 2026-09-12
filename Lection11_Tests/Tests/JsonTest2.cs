using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Lection11_Tests.Models.ForJson2;
using Lection11_Tests.Models.ForJsonRead;
using Newtonsoft.Json;

namespace Lection11_Tests.Tests
{
    public class JsonTest2
    {
        private OrderData order;

        [OneTimeSetUp]
        public void SetUp()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "OrderData.json");
            string data = File.ReadAllText(path);
            order = JsonConvert.DeserializeObject<OrderData>(data); 
        }

        [Test]
        public void Should_Print_All_Items()
        {
            foreach (var item in order.Items)
            {
                TestContext.WriteLine(
                    $"{item.ProductId} | {item.Name} | qty={item.Quantity} | price={item.Price}"
                );
            }
            order.Items.Should().NotBeNull();
            order.Items.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public void Should_Calculate_Total_With_Linq()
        {
            var sum = order.Items
                .Select(i => i.Price * i.Quantity)
                .Sum();

            TestContext.WriteLine($"Calculated sum: {sum}");

            sum.Should().Be(order.Summary.ItemsTotal);
        }

        [Test]
        public void Should_Find_Electronics()
        {           
            var electronics = order.Items
                .Where(i => i.Category == "Electronics")
                .ToList();

            foreach (var item in electronics)
            {
                TestContext.WriteLine($"Electronics: {item.Name}");
            }

            electronics.Should().NotBeEmpty();
            electronics.Should().OnlyContain(i => i.Category == "Electronics");
            electronics.Count().Should().Be(2);
        }

        [Test]
        public void Should_Check_Delivery_Status()
        {
            if (order.Delivery.Status == "in_progress")
            {
                TestContext.WriteLine(
                    $"Delivery in progress. Estimated date: {order.Delivery.EstimatedDate}"
                );
            }

            order.Delivery.Status.Should().Be("in_progress");
        }

        [Test]
        public void Should_Check_Payment_Status()
        {

            order.Payment.Status.Should().Be("paid");
            order.Payment.TransactionId.Should().NotBeNullOrWhiteSpace();

            TestContext.WriteLine($"Transaction ID: {order.Payment.TransactionId}");
        }

        [Test]
        public void Should_Find_Most_Expensive_Item()
        {
            
            var mostExpensive = order.Items
                .OrderByDescending(i => i.Price)
                .First();

            mostExpensive.Should().NotBeNull();
            mostExpensive.Price.Should().Be(129.99m);
            mostExpensive.Name.Should().Be("Wireless Headphones");

            TestContext.WriteLine($"Most expensive: {mostExpensive.Name} ({mostExpensive.Price})");
        }

        [Test]
        public void Should_Get_All_Item_Names()
        {

            var names = order.Items
                .Select(i => i.Name)
                .ToList();

            names.Should().NotBeEmpty();
            names.Should().Contain("Wireless Headphones");

            foreach (var name in names)
                TestContext.WriteLine(name);
        }

        [Test]
        public void Should_Find_Items_With_Price_Above_50()
        {

            var expensive = order.Items
                .Where(i => i.Price > 50)
                .ToList();

            using (new AssertionScope())
            {
                expensive.Should().NotBeEmpty();
                expensive.Should().Contain(i => i.Name == "Wireless Headphones");
            }
            //expensive.Should().NotBeEmpty();
            //expensive.Should().Contain(i => i.Name == "Wireless Headphones");

            foreach (var item in expensive)
                TestContext.WriteLine($"{item.Name} — {item.Price}");
        }
    }
}

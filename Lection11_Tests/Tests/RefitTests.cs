using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Lection11_Tests.Interfaces;
using Lection11_Tests.Models;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Lection11_Tests.Tests
{
    public class RefitTests
    {
        private IUserApi api;

        [OneTimeSetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services
                .AddRefitClient<IUserApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://reqres.in/api");
                });

            var provider = services.BuildServiceProvider();
            api = provider.GetRequiredService<IUserApi>();
        }

        [Test]
        public async Task Test1()
        {
            var result = await api.GetUserAsync(2);
            TestContext.WriteLine(result.Data.FirstName);
            Assert.That(result.Data.Id, Is.EqualTo(2));
        }

        [Test]
        public async Task Test2()
        {
            var userResponse = await api.GetUserAsync(2);
            UserData user = userResponse.Data;
            Assert.That(user.Email, Is.Not.Null);
        }

        [Test]
        public async Task Test3()
        {
            var request = new CreateUserRequest { Name = "morpheus", Job = "leader" };
            var created = await api.CreateUserAsync(request);
            Assert.That(created.Name, Is.EqualTo("morpheus"));
        }

        [Test]
        public async Task Test4()
        {
            var updated = new CreateUserRequest { Name = "morpheus", Job = "zion resident" };
            var response = await api.UpdateUserAsync(2, updated);
            Assert.That(response.Job, Is.EqualTo("zion resident"));
        }

        [Test]
        public async Task Test5()
        {
            var delResp = await api.DeleteUserAsync(2);
            Assert.That(delResp.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }
    }
}

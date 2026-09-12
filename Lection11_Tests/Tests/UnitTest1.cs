using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Lection11_Tests.Models;

namespace Lection11_Tests.Tests
{
    public class Tests
    {
        private static HttpClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            /*client = new HttpClient
            {
                BaseAddress = new Uri("https://reqres.in/api/")
            };*/
            client = new HttpClient
            {
                BaseAddress = new Uri("https://petstoreapi.com")
            };
            //client.DefaultRequestHeaders.Add("x-api-key", "free_user_3HMdkNLTV9xQwVXyEyCt3544HcE");
        }

        [Test]
        public async Task Test1()
        {
            // GET-запрос — указываем только путь
            using HttpResponseMessage response = await client.GetAsync("/pet/1");
            // Проверяем статус и читаем тело
            response.EnsureSuccessStatusCode(); // выбросит исключение при 4xx/5xx
        }

        [Test]
        public async Task Test2()
        {
            // GET-запрос — указываем только путь
            using HttpResponseMessage response = await client.GetAsync("users/2");
            string jsonGet = await response.Content.ReadAsStringAsync();
            UserResponse userResponse = JsonSerializer.Deserialize<UserResponse>(jsonGet);
            UserData user = userResponse.Data;
        }

        [Test]
        public async Task Test3()
        {
            var request = new CreateUserRequest { Name = "morpheus", Job = "leader" };
            using HttpResponseMessage responseCreateUser = await client.PostAsJsonAsync("users", request);
            string jsonPost = await responseCreateUser.Content.ReadAsStringAsync();
            CreateUserResponse created = JsonSerializer.Deserialize<CreateUserResponse>(jsonPost);
        }

        [Test]
        public async Task Test4()
        {
            var updated = new CreateUserRequest { Name = "morpheus", Job = "zion resident" };
            using HttpResponseMessage putResp = await client.PutAsJsonAsync("users/2", updated);
            putResp.EnsureSuccessStatusCode();
            string putJson = await putResp.Content.ReadAsStringAsync();
        }

        [Test]
        public async Task Test5()
        {
            using HttpResponseMessage delResp = await client.DeleteAsync("users/2");
            var statusCode = (int)delResp.StatusCode;
            var body = await delResp.Content.ReadAsStringAsync();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            client.Dispose();
        }
    }
}
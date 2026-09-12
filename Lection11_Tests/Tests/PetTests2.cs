using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Lection11_Tests.Models.ForPetStore2;
using Lection11_Tests.Interfaces.ForPetStore2;
using Refit;
using FluentAssertions;
using Lection11_Tests.Interfaces;
using System.Net;
using Lection11_Tests.Models.ForJsonRead;
using System.Xml;
using Lection11_Tests.Utils;

namespace Lection11_Tests.Tests
{
    public class PetTests2
    {
        protected IPetApi PetApi;

        [OneTimeSetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
            };

            services.AddRefitClient<IPetApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://petstoreapi.com/v1");
                })
                .ConfigurePrimaryHttpMessageHandler(() => handler);

            var provider = services.BuildServiceProvider();
            PetApi = provider.GetRequiredService<IPetApi>();
        }

        [Test]
        public async Task GetPet_Should_Return_Pet()
        {
            var pet = await PetApi.GetPetAsync("019bb6f1-a7fa-7010-914d-036047048ba4");
            pet.Should().NotBeNull();
        }

        [Test]
        public async Task GetPet_Should_Return_All_Pets()
        {
            var pets = await PetApi.GetAllPetsAsync();
            pets.Should().NotBeNull();            
        }

        [Test]
        public async Task GetPet_Should_Return_All_Pets_By_Status()
        {
            var pets = await PetApi.GetAllPetsByStatusAsync("ADOPTED", 100);
            pets.Data.Should().HaveCount(100);
            var listOfStatus = pets.Data.Select(x => x.Status).ToList();
            listOfStatus.Should().AllBe("ADOPTED");
        }

        [Test]
        public async Task GetRandomPet()
        {
            var pets = await PetApi.GetAllPetsAsync();
            pets.Should().NotBeNull();
            var rndId = RandomUtils.GetRandomItem<PetDTO>(pets.Data).Id;
            var pet = await PetApi.GetPetAsync(rndId);
            pet.Should().NotBeNull();
        }


        /////////////////////////////
        [Test]
        public async Task CreateUser_ShouldReturnUserId()
        {
            var response = await PetApi.CreatePetUserAsync(new CreateUserRequestDTO
            {
                Username = "OlgaTestUser",
                Password = "StrongPass123!"
            });

            response.UserId.Should().NotBeNullOrEmpty();
            response.Username.Should().Be("OlgaTestUser");
        }

        [Test]
        public async Task LoginUser_ShouldReturnUserId()
        {
            var response = await PetApi.LoginPetUserAsync(new LoginRequestDTO
            {
                Username = "OlgaTestUser",
                Password = "StrongPass123!"
            });

            response.UserId.Should().NotBeNullOrEmpty();
            response.Username.Should().Be("OlgaTestUser");
        }

        [Test]
        public async Task GenerateToken_ShouldReturnToken()
        {
            var response = await PetApi.GeneratePetTokenAsync(new LoginRequestDTO
            {
                Username = "OlgaTestUser",
                Password = "StrongPass123!"
            });

            response.Token.Should().NotBeNullOrEmpty();
            response.Status.Should().Be("Success");
        }

        [Test]
        public async Task GetUser_ShouldReturnUserData()
        {
            var login = await PetApi.LoginPetUserAsync(new LoginRequestDTO
            {
                Username = "OlgaTestUser",
                Password = "StrongPass123!"
            });

            var user = await PetApi.GetPetUserAsync(login.UserId);

            user.UserId.Should().Be(login.UserId);
            user.Username.Should().Be("OlgaTestUser");
        }

        [Test]
        public async Task DeleteUser_ShouldReturnSuccessMessage()
        {
            var login = await PetApi.LoginPetUserAsync(new LoginRequestDTO
            {
                Username = "OlgaTestUser",
                Password = "StrongPass123!"
            });

            var response = await PetApi.DeletePetUserAsync(login.UserId);

            response.Message.Should().Contain("deleted");
        }
    }
}    

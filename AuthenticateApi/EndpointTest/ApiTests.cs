using System.Net;
using AuthAPI.DTOs;
using AuthAPI.Models;
using AuthAPI.Services;
using AuthAPI.Services.IServices;
using AuthAPI.Services.TokenManager;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.Options;
using Moq;
using RestSharp;
using RestSharp.Authenticators;

namespace EndpointTest
{
    public class Tests
    {

        private string baseUrl = "https://localhost:7096";

        public RestClient client;

        public ITokenManager tokenManager;
        public IAuth auth;


        [SetUp]
        public void Setup()
        {
            client = new();
            var tokenManagerMock = new Mock<ITokenManager>();
            tokenManager = tokenManagerMock.Object;

            var authService = new Mock<IAuth>();
            auth = authService.Object;
        }

        [Test]
        public void GetAllAchievements()
        {
            RestRequest request = new RestRequest(baseUrl + "/Game/achievements", Method.Get);
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJkNjlkODhlYy0wNTY2LTQ0OGItOGRhOC1lNDhjMjI0NDBkMWEiLCJ1c2VybmFtZSI6InZpdHlhMDcxNyIsInJvbGUiOiJBZG1pbiIsInVzZXJSZWdkYXRlIjoiMDQvMDYvMjAyNCAxNTo0OToxMyIsIm5iZiI6MTcxMjQ5Njk2NSwiZXhwIjo0ODY4MTcwNTY1LCJpYXQiOjE3MTI0OTY5NjUsImlzcyI6ImF1dGgtYXBpIiwiYXVkIjoiYXV0aC1jbGllbnQifQ.irAtQHSeLOxmfHjg70EPkLfGM-kGZSQ1lHB6U5ThqEw";
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");

            RestResponse response = client.Execute(request);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            Console.WriteLine("Elvárt eredmény: Json adat");
            Console.WriteLine($"Kapott eredmény: {response.Content}");
        }

        [Test]
        public void LoginWithCredentials()
        {
            RestRequest request = new RestRequest(baseUrl + "/Auth/login", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.AddBody(new LoginDTO
            {
                UserName = "vitya0717",
                Password = "Alma123@"
            }, ContentType.Json);

            RestResponse response = client.Execute(request);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            Console.WriteLine("Elvárt eredmény: Json adat");
            Console.WriteLine($"Kapott eredmény: {response.Content}");
        }

        [Test]
        public void LoginWithWrongCredentials()
        {
            RestRequest request = new RestRequest(baseUrl + "/Auth/login", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.AddBody(new LoginDTO
            {
                UserName = "vitya0717",
                Password = "Alma1234@"

            }, ContentType.Json);

            RestResponse response = client.Execute(request);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            Console.WriteLine("Elvárt eredmény: Helytelen adatok");
            Console.WriteLine($"Kapott eredmény: {response.Content}");
        }

        [Test]
        public void GetTop2PlayersOfKills()
        {
            RestRequest request = new RestRequest(baseUrl + "/Game/getTopPlayers?statName=kills&limit=2", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            RestResponse response = client.Execute(request);

            response.Should().NotBeNull();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            Console.WriteLine("Elvárt eredmény: Json adat");
            Console.WriteLine($"Kapott eredmény: {response.Content}");
        }

        [Test]
        public void GetTop10PlayersOfKillsWrongName()
        {
            RestRequest request = new RestRequest(baseUrl + "/Game/getTopPlayers?statName=Kils&limit=10", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            RestResponse response = client.Execute(request);

            response.Should().NotBeNull();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            response.Content.Should().Be($"{'"'}Sequence contains no matching element{'"'}");

            Console.WriteLine("Elvárt eredmény: Sequence contains no matching element");
            Console.WriteLine($"Kapott eredmény: {response.Content}");
        }

    }
}
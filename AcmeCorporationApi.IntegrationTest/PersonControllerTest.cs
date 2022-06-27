using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AcmeCorporationApi.IntegrationTest
{
    public class PersonControllerTest
    {

        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PersonControllerTest()
        {
            this._server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            this._client = _server.CreateClient();
        }
        [Fact]
        public async Task Get_AllPersons_OK()
        {
            var response = await _client.GetAsync("/Persons");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Person_ByID_OK()
        {
            var response = await _client.GetAsync("/Persons/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Person_ByID_ID_NOT_FOUND_OK()
        {
            var response = await _client.GetAsync("/Persons/99");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}

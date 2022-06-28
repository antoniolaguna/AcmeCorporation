using AcmeCorporationApi.Controllers;
using AcmeCorporationApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AcmeCorporationApi.Test
{
    public class PersonControllerUnitTest
    {

        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PersonControllerUnitTest()
        {
            this._server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            this._client = _server.CreateClient();
        }

        [Fact]
        public async Task CREATE_PERSON_OK()
        {
            var person = new PersonDto();
            person.Age = 30;
            person.Document = "05709004J";
            person.Name = "Antonio585485lagunafdsf";
            person.DocumentType = "DNI";
            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/Persons", data);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task CREATE_PERSON_INCORRECT_DOCUMENT_TYPE()
        {
            var person = new PersonDto();
            person.Age = 30;
            person.Document = "05709004J";
            person.Name = "Antonio58548523f";
            person.DocumentType = "DNI";
            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/Persons", data);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CREATE_PERSON_INCORRECT_DOCUMENT()
        {
            var person = new PersonDto();
            person.Age = 30;
            person.Document = "05709004L";
            person.Name = "Antoni3f2o58548523f";
            person.DocumentType = "DNI";
            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/Persons", data);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CREATE_PERSON_NAME_NOT_UNIQUE()
        {
            var person = new PersonDto();
            person.Age = 30;
            person.Document = "05709004L";
            person.Name = "Antoni3f2o58548523f";
            person.DocumentType = "DNI";
            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await _client.PostAsync("/Persons", data);

            var response = await _client.PostAsync("/Persons", data);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}

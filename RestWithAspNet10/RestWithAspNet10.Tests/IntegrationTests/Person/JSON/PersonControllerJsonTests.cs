using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Tests.IntegrationTests.Tools;
using RestWithASPNET10.Tests.IntegrationTests.Tools;
using System.Net;
using System.Net.Http.Json;

namespace RestWithAspNet10.Tests.IntegrationTests.Person.JSON
{
    [TestCaseOrderer("RestWithAspNet10.Tests.IntegrationTests.Tools.PriorityOrderer", "RestWithAspNet10.Tests")]
    public class PersonControllerJsonTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static PersonDTO _person;

        public PersonControllerJsonTests(SqlServerFixture sqlFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(sqlFixture.ConnectionString);
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost")
            });
        }
       

        [Fact(DisplayName = "01 - Created Person")]
        [TestPriority(1)]
        public async Task CreatePerson_ShouldReturnCreatedPerson()
        {
            // Arrange
            var request = new PersonDTO
            {
                FirstName = "Lamine",
                LastName = "Yamal",
                Address = "Espanha",
                Gender = "Male",
                Enabled = true
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("api/person/v1", request);

            //Assert
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<PersonDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.FirstName.Should().Be("Lamine");
            created.LastName.Should().Be("Yamal");
            created.Address.Should().Be("Espanha");
            created.Enabled.Should().BeTrue();

            _person = created;
        }

        [Fact(DisplayName = "02 - Update Person")]
        [TestPriority(2)]
        public async Task UpdatePerson_ShouldReturnUpdatedPerson()
        {
            // Arrange
            _person.LastName = "Barcelona";

            // Act
            var response = await _httpClient.PutAsJsonAsync("api/person/v1", _person);

            //Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content.ReadFromJsonAsync<PersonDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().BeGreaterThan(0);
            updated.FirstName.Should().Be("Lamine");
            updated.LastName.Should().Be("Barcelona");
            updated.Address.Should().Be("Espanha");
            updated.Enabled.Should().BeTrue();

            _person = updated;
        }


        

        

        
    }
 }

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Hypermedia.Utils;
using RestWithAspNet10.Tests.IntegrationTests.Tools;
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
            _person.LastName = "Yam";

            // Act
            var response = await _httpClient.PutAsJsonAsync("api/person/v1", _person);

            //Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content.ReadFromJsonAsync<PersonDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().BeGreaterThan(0);
            updated.FirstName.Should().Be("Lamine");
            updated.LastName.Should().Be("Yam");
            updated.Address.Should().Be("Espanha");
            updated.Enabled.Should().BeTrue();

            _person = updated;
        }

        [Fact(DisplayName = "03 - Disable Person By ID")]
        [TestPriority(3)]
        public async Task DisablePersonById_ShouldReturnDisabledPerson()
        {
            //Arrange and Act
            var response = await _httpClient.PatchAsync($"api/person/v1/{_person.Id}", null);

            //Assert
            response.EnsureSuccessStatusCode();

            var disabled = await response.Content.ReadFromJsonAsync<PersonDTO>();

            disabled.Should().NotBeNull();
            disabled.Id.Should().BeGreaterThan(0);
            disabled.FirstName.Should().Be("Lamine");
            disabled.LastName.Should().Be("Yam");
            disabled.Address.Should().Be("Espanha");
            disabled.Enabled.Should().BeFalse();

            _person = disabled;
        }

        [Fact(DisplayName = "04 - Get Person By ID")]
        [TestPriority(4)]
        public async Task GetPersonById_ShouldReturnPerson()
        {
            //Arrange and Act
            var response = await _httpClient.GetAsync($"api/person/v1/{_person.Id}");
            //Assert
            response.EnsureSuccessStatusCode();

            var found = await response.Content.ReadFromJsonAsync<PersonDTO>();

            found.Should().NotBeNull();
            found.Id.Should().Be(_person.Id);
            found.FirstName.Should().Be("Lamine");
            found.LastName.Should().Be("Yam");
            found.Address.Should().Be("Espanha");
            found.Enabled.Should().BeFalse();
        }


        [Fact(DisplayName = "05 - Delete Person By ID")]
        [TestPriority(5)]
        public async Task DeletePersonById_ShouldReturnNoContent()
        {
            //Arrange and Act
            var response = await _httpClient.DeleteAsync($"api/person/v1/{_person.Id}");
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact(DisplayName = "06 - Find All People")]
        [TestPriority(6)]
        public async Task FindAllPerson_ShouldReturnListOfPerson()
        {
            //Arrange and Act
            var response = await _httpClient.GetAsync("api/person/v1/asc/10/1");
                                           // <-- sortDirection=asc, pageSize=10, page=1
            //Assert
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<PagedSearchDTO<PersonDTO>>();
            page.Should().NotBeNull();
            page.CurrentPage.Should().Be(1);


            var list = page?.List;

            list.Should().NotBeNull();
            list.Count.Should().BeGreaterThan(0);

            var first = list.First(p => p.FirstName == "Abbie");
            first.LastName.Should().Be("Bassford");
            first.Address.Should().Be("PO Box 88145");
            first.Enabled.Should().BeFalse();
            first.Gender.Should().Be("Male");

            var third = list.First(p => p.FirstName == "Abner");
            third.LastName.Should().Be("Castilla");
            third.Address.Should().Be("8th Floor");
            third.Enabled.Should().BeFalse();
            third.Gender.Should().Be("Male");

            page.CurrentPage.Should().BeGreaterThan(0);
            page.TotalResults.Should().BeGreaterThan(0);
            page.PageSize.Should().BeGreaterThan(0);
            page.SortDirection.Should().NotBeNull();
        }


    }
}

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Tests.IntegrationTests.Tools;
using System.Net.Http.Json;

namespace RestWithAspNet10.Tests.IntegrationTests.Book.JSON
{
    [TestCaseOrderer(TestConfigs.TestCaseOrdererFullName, TestConfigs.TestCaseOrdererAssembly)]
    public class BookControllerJsonTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static BookDTO _book;

        public BookControllerJsonTests(SqlServerFixture fixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost")
            });
        }

        [Fact(DisplayName = "01 - Created Book")]
        [TestPriority(1)]
        public async Task CreateBook_ShouldReturnCreatedBook()
        {
            // Arrange
            var request = new BookDTO
            {
                Title = "Rest with ASP.NET Core 10",
                Author = "Lamine Yamal",
                Price = 49.99M,
                LaunchDate = new DateTime(1925, 4, 10)
            };
            // Act
            var response = await _httpClient.PostAsJsonAsync("api/book/v1", request);
            //Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<BookDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Title.Should().Be("Rest with ASP.NET Core 10");
            created.Author.Should().Be("Lamine Yamal");
            created.Price.Should().Be(49.99M);
            _book = created;


        }
    }
}

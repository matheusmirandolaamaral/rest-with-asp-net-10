using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Model;
using RestWithAspNet10.Tests.IntegrationTests.Tools;
using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace RestWithAspNet10.Tests.IntegrationTests.HATEOAS
{
    [TestCaseOrderer(TestConfigs.TestCaseOrdererFullName, TestConfigs.TestCaseOrdererAssembly)]
    public class BookControllerHATEOASTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static BookDTO? _book;

        public BookControllerHATEOASTests(SqlServerFixture sqlFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(sqlFixture.ConnectionString);
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost")
            });
        }

        private void AssertLinkPattern(string content, string rel)
        {
            var pattern = $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/book/v1.*?""";
            Regex.IsMatch(content, pattern).Should().BeTrue($"Link with rel='{rel}' should exist and have valid href");
        }

        [Fact(DisplayName = "01 - Create Book ")]
        [TestPriority(1)]
        public async Task CreateBook_ShouldContainHateoasLinks()
        {
            // Arrange
            var request = new BookDTO
            {
                Title = "c# course",
                Author = "Leandro",
                Price = 10.99m,
                LaunchDate = new DateTime(1925, 4, 10)
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("/api/book/v1", request);
            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            _book = await response.Content.ReadFromJsonAsync<BookDTO>();
            AssertLinkPattern(content, "collection");
            AssertLinkPattern(content, "self");
            AssertLinkPattern(content, "create");
            AssertLinkPattern(content, "update");
            AssertLinkPattern(content, "delete");
        }

        [Fact(DisplayName = "02 - Update Book ")]
        [TestPriority(2)]
        public async Task UpdateBook_ShouldContainHateoasLinks()
        {
            _book!.Author = "Leandro Erudio";

            var response = await _httpClient.PutAsJsonAsync("/api/book/v1", _book);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            _book = await response.Content.ReadFromJsonAsync<BookDTO>();
            AssertLinkPattern(content, "collection");
            AssertLinkPattern(content, "self");
            AssertLinkPattern(content, "create");
            AssertLinkPattern(content, "update");
            AssertLinkPattern(content, "delete");
        }

        [Fact(DisplayName = "03 - Get Book By Id")]
        [TestPriority(3)]
        public async Task GetBookById_ShouldContainHateoasLinks()
        {
            var response = await _httpClient.GetAsync($"/api/book/v1/{_book!.Id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            _book = await response.Content.ReadFromJsonAsync<BookDTO>();

            AssertLinkPattern(content, "collection");
            AssertLinkPattern(content, "self");
            AssertLinkPattern(content, "create");
            AssertLinkPattern(content, "update");
            AssertLinkPattern(content, "delete");
        }
        [Fact(DisplayName = "04 - Find Paged Books with HATEOAS")]
        [TestPriority(4)]
        public async Task FindAll_ShouldReturnLinksForEachBook()
        {
            // ---------------------------
            // Arrange
            // ---------------------------
            // In this test, there is no explicit Arrange step, because
            // we are directly calling the API without preparing additional
            // data or mocking dependencies. The system under test is expected
            // to already contain one or more books.

            // ---------------------------
            // Act
            // ---------------------------
            // Perform the HTTP GET request to retrieve all books.
            var response = await _httpClient.GetAsync("api/book/v1/asc/10/1");

            // Read the response content as a string.
            var content = await response.Content.ReadAsStringAsync();

            // ---------------------------
            // Assert
            // ---------------------------
            // Extract all "id" values from the response JSON using Regex.
            var idMatches = Regex.Matches(content, @"""list"":\s*\[\s*{[^}]*""id"":\s*(\d+)");
            idMatches.Count.Should().BeGreaterThan(0, "There should be at least one book");

            // Iterate through each book id found in the response.
            foreach (Match match in idMatches)
            {
                var id = match.Groups[1].Value;

                // Expected hypermedia relations (HATEOAS links).
                var expectedRels = new[] { "collection", "self", "create", "update", "delete" };

                foreach (var rel in expectedRels)
                {
                    // Build the expected regex pattern depending on the relation.
                    // For "self" and "delete", the link must contain the specific id.
                    // For others, the link points to the base endpoint.
                    var pattern = rel switch
                    {
                        "self" or "delete" =>
                            $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/book/v1/{id}""",
                        _ =>
                            $@"""rel"":\s*""{rel}"".*?""href"":\s*""https?://.+/api/book/v1"""
                    };

                    // Assert that the link with the correct "rel" and "href" exists.
                    Regex.IsMatch(content, pattern, RegexOptions.IgnoreCase)
                         .Should().BeTrue($"Link '{rel}' should exist for book {id}");

                    // Assert that each link also contains a "type" attribute.
                    var typePattern = $@"""rel"":\s*""{rel}"".*?""type"":\s*""[^""]+""";
                    Regex.IsMatch(content, typePattern)
                         .Should().BeTrue($"Link '{rel}' must have a type for book {id}");
                }
            }
        }

        [Fact(DisplayName = "05 - Delete Book By ID")]
        [TestPriority(5)]
        public async Task DeleteBookById_ShouldReturnNoContent()
        {
            var response = await _httpClient.DeleteAsync($"api/book/v1/{_book!.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}

using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Hypermedia.Utils;
using RestWithAspNet10.Tests.IntegrationTests.Tools;
using System.Net;
using System.Net.Http.Headers;

namespace RestWithAspNet10.Tests.IntegrationTests.Book.XML
{
    [TestCaseOrderer(TestConfigs.TestCaseOrdererFullName, TestConfigs.TestCaseOrdererAssembly)]
    public class BookControllerXmlTests : IClassFixture<SqlServerFixture>
    {
        private readonly HttpClient _httpClient;
        private static BookDTO _book;

        public BookControllerXmlTests(SqlServerFixture sqlFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(sqlFixture.ConnectionString);
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost")
            });
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
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
            var response = await _httpClient.PostAsync("api/book/v1", XmlHelper.SerializeToXml(request));
            //Assert
            response.EnsureSuccessStatusCode();
            var created = await XmlHelper.ReadFromXmlAsync<BookDTO>(response);
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Title.Should().Be("Rest with ASP.NET Core 10");
            created.Author.Should().Be("Lamine Yamal");
            created.Price.Should().Be(49.99M);
            _book = created;
        }


        [Fact(DisplayName = "02 - Update Book")]
        [TestPriority(2)]
        public async Task UpdateBook_ShouldReturnUpdatedBook()
        {
            // Arrange
            _book.Title = "Rest with ASP.NET Core 10 - Updated";
            // Act
            var response = await _httpClient.PutAsync("api/book/v1", XmlHelper.SerializeToXml(_book));
            //Assert
            response.EnsureSuccessStatusCode();
            var updated = await XmlHelper.ReadFromXmlAsync<BookDTO>(response);
            updated.Should().NotBeNull();
            updated.Id.Should().BeGreaterThan(0);
            updated.Title.Should().Be("Rest with ASP.NET Core 10 - Updated");
            updated.Author.Should().Be("Lamine Yamal");
            updated.Price.Should().Be(49.99M);

            _book = updated;

        }

        [Fact(DisplayName = "03 - Get Book By Id")]
        [TestPriority(3)]
        public async Task GetBookById_ShouldReturnBook()
        {
            // Act
            var response = await _httpClient.GetAsync($"api/book/v1/{_book.Id}");
            //Assert
            response.EnsureSuccessStatusCode();
            var found = await XmlHelper.ReadFromXmlAsync<BookDTO>(response);
            found.Should().NotBeNull();
            found.Id.Should().Be(_book.Id);
            found.Title.Should().Be("Rest with ASP.NET Core 10 - Updated");
            found.Author.Should().Be("Lamine Yamal");
            found.Price.Should().Be(49.99M);
        }

        [Fact(DisplayName = "04 - Get All Books")]
        [TestPriority(4)]
        public async Task GetAllBooks_ShouldReturnListOfBooks()
        {
            // Act
            var response = await _httpClient.GetAsync("api/book/v1/asc/10/1");
            //Assert
            response.EnsureSuccessStatusCode();
            var page = await XmlHelper.ReadFromXmlAsync<PagedSearchDTO<BookDTO>>(response);
            page.Should().NotBeNull();
            page.CurrentPage.Should().Be(1);

            var list = page?.List;

            list.Should().NotBeNull();
            list.Count.Should().BeGreaterThan(0);

            var first = list.First(p => p.Title == "Clean Code: A Handbook of Agile Software Craftsmanship");
            first.Author.Should().Be("Robert C. Martin");

            var nine = list.First(p => p.Title == "Quiet: The Power of Introverts in a World That Can't Stop Talking");
            nine.Author.Should().Be("Susan Cain");

            page.CurrentPage.Should().BeGreaterThan(0);
            page.TotalResults.Should().BeGreaterThan(0);
            page.PageSize.Should().BeGreaterThan(0);
            page.SortDirection.Should().NotBeNull();
        }


        [Fact(DisplayName = "05 - Delete Book")]
        [TestPriority(5)]
        public async Task DeleteBook_ShouldReturnNoContent()
        {
            // Act
            var response = await _httpClient.DeleteAsync($"api/book/v1/{_book.Id}");
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}

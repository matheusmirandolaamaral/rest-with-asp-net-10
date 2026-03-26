using FluentAssertions;
using RestWithAspNet10.Repository.QueryBuilders;

namespace RestWithAspNet10.Tests.UnitTests
{
    public class BookQueryBuilderTests
    {
        private readonly BookQueryBuilder _queryBuilder;

        public BookQueryBuilderTests()
        {
            _queryBuilder = new BookQueryBuilder();
        }

        [Fact]
        public void BuildQueries_ShouldReturnCorrectQueries()
        {
            // Arrange
            var name = "CSHARP Book";
            var sortDirection = "asc";
            var pageSize = 10;
            var page = 2;
            // Act
            var (query, countQuery, sort, size, offset) = _queryBuilder.BuildQueries(name, sortDirection, pageSize, page);
            // Assert
            query.Should().Contain("SELECT * FROM books p WHERE 1 = 1 AND (p.title LIKE '%CSHARP Book%')");
            query.Should().Contain("ORDER BY p.title asc");
            query.Should().Contain("OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY");
            countQuery.Should().Contain("SELECT COUNT(*) FROM books p WHERE 1 = 1 AND (p.title LIKE '%CSHARP Book%')");
            sort.Should().Be("asc");
            size.Should().Be(10);
            offset.Should().Be(10);
        }


        [Fact]
        public void BuildQueries_ShouldHandleInvalidPageAndPageSize()
        {
            // Arrange
            var name = "Java book";
            var sortDirection = "asc";
            var pageSize = 0;
            var page = -1;
            // Act
            var (query, countQuery, sort, size, offset) = _queryBuilder.BuildQueries(name, sortDirection, pageSize, page);
            // Assert
            query.Should().Contain("SELECT * FROM books p WHERE 1 = 1 ");
            query.Should().Contain("ORDER BY p.title asc");
            query.Should().Contain("OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY");
            countQuery.Should().Contain("SELECT COUNT(*) FROM books p WHERE 1 = 1 ");
            sort.Should().Be("asc");
            size.Should().Be(1);
            offset.Should().Be(0);
        }

        [Fact]
        public void BuildQueries_ShouldHandleInvalidSortDirection()
        {
            // Arrange
            var name = "Python book";
            var sortDirection = "invalid";
            var pageSize = 10;
            var page = 1;
            // Act
            var (query, countQuery, sort, size, offset) = _queryBuilder.BuildQueries(name, sortDirection, pageSize, page);
            // Assert
            query.Should().Contain("SELECT * FROM books p WHERE 1 = 1 AND (p.title LIKE '%Python book%')");
            query.Should().Contain("ORDER BY p.title asc");
            query.Should().Contain("OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY");
            countQuery.Should().Contain("SELECT COUNT(*) FROM books p WHERE 1 = 1 AND (p.title LIKE '%Python book%')");
            sort.Should().Be("asc");
            size.Should().Be(10);
            offset.Should().Be(0);
        }

        [Fact]
        public void BuildQueries_ShouldHandleEmptyName()
        {
            // Arrange
            string name = null;
            var sortDirection = "asc";
            var pageSize = 10;
            var page = 1;
            // Act
            var (query, countQuery, sort, size, offset) = _queryBuilder.BuildQueries(name, sortDirection, pageSize, page);
            // Assert
            query.Should().Contain("SELECT * FROM books p WHERE 1 = 1 ");
            query.Should().Contain("ORDER BY p.title asc");
            query.Should().Contain("OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY");
            query.Should().NotContain("AND (p.title LIKE )");
            countQuery.Should().Contain("SELECT COUNT(*) FROM books p WHERE 1 = 1 ");
            sort.Should().Be("asc");
            size.Should().Be(10);
            offset.Should().Be(0);
        }
    }
}

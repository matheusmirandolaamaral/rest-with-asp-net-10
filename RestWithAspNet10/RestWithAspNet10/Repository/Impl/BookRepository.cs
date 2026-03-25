using RestWithAspNet10.Model;
using RestWithAspNet10.Model.Context;
using RestWithAspNet10.Repository.QueryBuilders;

namespace RestWithAspNet10.Repository.Impl
{
    public class BookRepository(MSSQLContext context) : GenericRepository<Book>(context), IBookRepository
    {
        public List<Book> FindByTitle(string title, string author)
        {
            var query = _context.Books.AsQueryable();
            if(!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if(!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.Author.Contains(author));

           // return query.ToList();
            return [.. query];
        }

        public PagedSearch<Book> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var queryBuilder = new BookQueryBuilder();

            var (query, countQuery, sort, size, offset) = queryBuilder.BuildQueries(name, sortDirection, pageSize, page);

            var books = base.FindWithPagedSearch(query);
            var totalResults = base.GetCount(countQuery);

            return new PagedSearch<Book>
            {
                CurrentPage = page,
                List = books,
                PageSize = size,
                SortDirection = sort,
                TotalResults = totalResults
            };
            }
    }
}

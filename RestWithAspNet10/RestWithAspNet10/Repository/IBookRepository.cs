using RestWithAspNet10.Model;

namespace RestWithAspNet10.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        List<Book> FindByTitle(string title, string author);
        PagedSearch<Book> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}

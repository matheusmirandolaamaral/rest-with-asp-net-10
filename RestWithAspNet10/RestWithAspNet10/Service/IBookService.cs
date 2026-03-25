using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Model;

namespace RestWithAspNet10.Service
{
    public interface IBookService
    {
        BookDTO Create(BookDTO book);
        BookDTO? FindById(long id);
        List<BookDTO> FindAll();
        BookDTO? Update(BookDTO book);
        void Delete(long id);
        List<BookDTO> FindByTitle(string title, string author);
        PagedSearch<BookDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}

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
    }
}

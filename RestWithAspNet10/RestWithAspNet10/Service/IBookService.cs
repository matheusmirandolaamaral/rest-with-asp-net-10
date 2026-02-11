using RestWithAspNet10.Model;

namespace RestWithAspNet10.Service
{
    public interface IBookService
    {
        Book Create(Book book);
        Book? FindById(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}

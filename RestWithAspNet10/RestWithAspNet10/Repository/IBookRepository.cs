using RestWithAspNet10.Model;

namespace RestWithAspNet10.Repository
{
    public interface IBookRepository
    {
        Book Create(Book book);
        Book? FindById(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}

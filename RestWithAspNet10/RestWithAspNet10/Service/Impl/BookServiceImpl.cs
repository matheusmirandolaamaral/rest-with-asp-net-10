using Mapster;
using RestWithAspNet10.Data.DTO;
using RestWithAspNet10.Model;
using RestWithAspNet10.Repository;

namespace RestWithAspNet10.Service.Impl
{
    public class BookServiceImpl : IBookService
    {
        private IRepository<Book> _repository;
        public BookServiceImpl(IRepository<Book> repository)
        {
            _repository = repository;
        }


        public List<BookDTO> FindAll()
        {
            return _repository.FindAll().Adapt<List<BookDTO>>();
        }

        public BookDTO? FindById(long id)
        {
            return _repository.FindById(id).Adapt<BookDTO>();
        }


        public BookDTO Create(BookDTO book)
        {
            var entity = book.Adapt<Book>();
            entity = _repository.Create(entity);
            return entity.Adapt<BookDTO>();
        }
        public BookDTO? Update(BookDTO book)
        {
            var entity = book.Adapt<Book>();
            entity = _repository.Update(entity);
            return entity.Adapt<BookDTO>();
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    }
}

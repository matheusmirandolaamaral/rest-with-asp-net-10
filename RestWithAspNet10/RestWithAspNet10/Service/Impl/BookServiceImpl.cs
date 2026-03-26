using Mapster;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Hypermedia.Utils;
using RestWithAspNet10.Model;
using RestWithAspNet10.Repository;

namespace RestWithAspNet10.Service.Impl
{
    public class BookServiceImpl : IBookService
    {
        private IBookRepository _repository;
        public BookServiceImpl(IBookRepository repository)
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

        public List<BookDTO> FindByTitle(string title, string author)
        {
            return _repository.FindByTitle(title, author).Adapt<List<BookDTO>>();
        }

        public PagedSearchDTO<BookDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);
            return result.Adapt<PagedSearchDTO<BookDTO>>();
        }
    }
}

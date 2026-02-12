using RestWithAspNet10.Model;
using RestWithAspNet10.Model.Base;

namespace RestWithAspNet10.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T? FindById(long id);
        List<T> FindAll();
        T? Update(T item);
        void Delete(long id);
        bool Exists(long id);
    }
}

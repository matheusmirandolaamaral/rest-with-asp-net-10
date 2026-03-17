using RestWithAspNet10.Model;

namespace RestWithAspNet10.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person? Disable(long id);
    }
}

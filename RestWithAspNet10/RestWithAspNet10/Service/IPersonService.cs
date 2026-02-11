using RestWithAspNet10.Model;

namespace RestWithAspNet10.Service
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person? FindById(long id);
        List<Person> FindAll();
        Person? Update(Person person);
        void Delete(long id);
    }
}

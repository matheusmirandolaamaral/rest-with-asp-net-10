using RestWithAspNet10.Model;
using RestWithAspNet10.Model.Context;
using RestWithAspNet10.Repository;

namespace RestWithAspNet10.Service.Impl
{
    public class PersonServiceImpl : IPersonService
    {
        private IPersonRepository _repository;
        public PersonServiceImpl(IPersonRepository repository)
        {
            _repository = repository;
        }


        public List<Person> FindAll()
        {
           return _repository.FindAll();
        }

        public Person? FindById(long id)
        {
            return _repository.FindById(id);
        }
        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person? Update(Person person)
        {
            return _repository.Update(person);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        

    }
}

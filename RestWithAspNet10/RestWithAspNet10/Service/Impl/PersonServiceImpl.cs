using RestWithAspNet10.Data.Converter.Impl;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Model;
using RestWithAspNet10.Model.Context;
using RestWithAspNet10.Repository;

namespace RestWithAspNet10.Service.Impl
{
    public class PersonServiceImpl : IPersonService
    {
        private IRepository<Person> _repository;
        private readonly PersonConverter _converter;
        public PersonServiceImpl(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }


        public List<PersonDTO> FindAll()
        {
           return  _converter.ParseList(_repository.FindAll());
        }

        public PersonDTO? FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }
        public PersonDTO Create(PersonDTO person)
        {
            var entity = _converter.Parse(person);
            entity = _repository.Create(entity);
            return _converter.Parse(entity);
        }

        public PersonDTO? Update(PersonDTO person)
        {
            var entity = _converter.Parse(person);
            entity = _repository.Update(entity);
            return _converter.Parse(entity);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        

    }
}

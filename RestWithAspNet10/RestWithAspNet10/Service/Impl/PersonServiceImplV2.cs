using RestWithAspNet10.Data.Converter.Impl;
using RestWithAspNet10.Data.DTO.V2;
using RestWithAspNet10.Model;
using RestWithAspNet10.Model.Context;
using RestWithAspNet10.Repository;

namespace RestWithAspNet10.Service.Impl
{
    public class PersonServiceImplV2
    {
        private IRepository<Person> _repository;
        private readonly PersonConverter _converter;
        public PersonServiceImplV2(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }


       
        public PersonDTO Create(PersonDTO person)
        {
            var entity = _converter.Parse(person);
            entity = _repository.Create(entity);
            return _converter.Parse(entity);
        }

        

        

    }
}

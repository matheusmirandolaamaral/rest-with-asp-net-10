using Mapster;
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
        
        public PersonServiceImpl(IRepository<Person> repository)
        {
            _repository = repository;
           
        }


        public List<PersonDTO> FindAll()
        {
           return  _repository.FindAll().Adapt<List<PersonDTO>>();
        }

        public PersonDTO? FindById(long id)
        {
            
            return _repository.FindById(id).Adapt<PersonDTO>();
        }
        public PersonDTO Create(PersonDTO person)
        {
            var entity = person.Adapt<Person>();
            entity = _repository.Create(entity);
            return entity.Adapt<PersonDTO>();
        }

        public PersonDTO? Update(PersonDTO person)
        {
            var entity = person.Adapt<Person>();
            entity = _repository.Update(entity);
            return entity.Adapt<PersonDTO>();
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        

    }
}

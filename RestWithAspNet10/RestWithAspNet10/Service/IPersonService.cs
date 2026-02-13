using RestWithAspNet10.Data.DTO;
using RestWithAspNet10.Model;

namespace RestWithAspNet10.Service
{
    public interface IPersonService
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO? FindById(long id);
        List<PersonDTO> FindAll();
        PersonDTO? Update(PersonDTO person);
        void Delete(long id);
    }
}

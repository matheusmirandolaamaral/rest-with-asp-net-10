using RestWithAspNet10.Data.DTO.V1;

namespace RestWithAspNet10.Service
{
    public interface IPersonService
    {
        PersonDTO Create(PersonDTO person);
        PersonDTO? FindById(long id);
        List<PersonDTO> FindAll();
        PersonDTO? Update(PersonDTO person);
        void Delete(long id);
        PersonDTO Disable(long id);
        List<PersonDTO> FindByName(string firstName, string lastName);
    }
}

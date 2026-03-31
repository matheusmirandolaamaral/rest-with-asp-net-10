using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Hypermedia.Utils;

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
        PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        Task<List<PersonDTO>> MassCreatingAsync(IFormFile file);
        FileContentResult ExportPage(int page, int pageSize, string sortDirection, string acceptHeader, string name);
    }
}

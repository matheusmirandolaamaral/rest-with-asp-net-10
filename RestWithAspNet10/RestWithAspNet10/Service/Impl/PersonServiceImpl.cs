using Mapster;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Files.Importers.Factory;
using RestWithAspNet10.Hypermedia.Utils;
using RestWithAspNet10.Model;
using RestWithAspNet10.Repository;

namespace RestWithAspNet10.Service.Impl
{
    public class PersonServiceImpl : IPersonService
    {
        private IPersonRepository _repository;
        private readonly FileImporterFactory _fileImporterFactory;
        private readonly ILogger<PersonServiceImpl> _logger;

        public PersonServiceImpl(IPersonRepository repository, FileImporterFactory fileImporterFactory, ILogger<PersonServiceImpl> logger)
        {
            _repository = repository;
            _fileImporterFactory = fileImporterFactory;
            _logger = logger;
        }


        public List<PersonDTO> FindAll()
        {
            return _repository.FindAll().Adapt<List<PersonDTO>>();
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

        public PersonDTO Disable(long id)
        {
            var entity = _repository.Disable(id);
            return entity.Adapt<PersonDTO>();
        }

        public List<PersonDTO> FindByName(string firstName, string lastName)
        {
            return _repository.FindByName(firstName, lastName).Adapt<List<PersonDTO>>();
        }

        public PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {

            var result = _repository.FindWithPagedSearch(name, sortDirection, pageSize, page);

            return result.Adapt<PagedSearchDTO<PersonDTO>>();
        }

        public async Task<List<PersonDTO>> MassCreatingAsync(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                _logger.LogError("File is empty or null.");
                throw new ArgumentException("File is empty or null.");
            }

            using var stream = file.OpenReadStream();
            var fileName = file.FileName;

            try { 
                var importer = _fileImporterFactory.GetImporter(fileName);
                var persons = await importer.ImportFileAsync(stream);

                var entities = persons.Select(dto => _repository.Create(dto.Adapt<Person>())).ToList();

                return entities.Adapt<List<PersonDTO>>();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during mass creation from file: {FileName}", file.FileName);
                throw;
            }

            throw new NotImplementedException();
        }
    }
}


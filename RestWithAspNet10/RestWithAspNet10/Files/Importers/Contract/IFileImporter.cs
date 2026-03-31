using RestWithAspNet10.Data.DTO.V1;

namespace RestWithAspNet10.Files.Importers.Contract
{
    public interface IFileImporter
    {
        Task<List<PersonDTO>> ImportFileAsync(Stream fileStream);
    }
}

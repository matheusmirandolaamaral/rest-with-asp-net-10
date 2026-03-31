using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Data.DTO.V1;

namespace RestWithAspNet10.Files.Exporters.Contract
{
    public interface IFileExporter
    {
        FileContentResult ExportFile(List<PersonDTO> people);
    }
}

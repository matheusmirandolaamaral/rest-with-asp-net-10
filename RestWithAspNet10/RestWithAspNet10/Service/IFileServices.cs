using RestWithAspNet10.Data.DTO.V1;

namespace RestWithAspNet10.Service
{
    public interface IFileServices
    {
        byte[] GetFile(string fileName);
        Task <FileDetailDTO> SaveFileToDisk(IFormFile file);
        Task <List<FileDetailDTO>> SaveFilesToDisk(List<IFormFile> files);
    }
}

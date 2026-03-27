using System.ComponentModel.DataAnnotations;

namespace RestWithAspNet10.Data.DTO.V1
{
    public class MultipleFilesUploadDTO
    {
        [Required]
        public List<IFormFile> Files { get; set; }
    }
}

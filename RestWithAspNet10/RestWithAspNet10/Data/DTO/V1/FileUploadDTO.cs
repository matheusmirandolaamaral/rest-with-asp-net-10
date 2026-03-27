using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace RestWithAspNet10.Data.DTO.V1
{
    public class FileUploadDTO
    {
        [Required]
        public IFormFile File { get; set; }
    }
}

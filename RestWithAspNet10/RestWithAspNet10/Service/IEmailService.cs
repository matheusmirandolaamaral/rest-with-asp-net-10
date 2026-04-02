using RestWithAspNet10.Data.DTO.V1;

namespace RestWithAspNet10.Service
{
    public interface IEmailService
    {
        void SendSimpleEmail(EmailRequestDTO emailRequest);
        Task SendEmailWithAttachment(EmailRequestDTO emailRequest, IFormFile attachment);
    }
}

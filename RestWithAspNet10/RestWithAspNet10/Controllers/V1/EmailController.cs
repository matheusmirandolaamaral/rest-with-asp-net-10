using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Service;
using System.Net.Mail;
using System.Text.Json;

namespace RestWithAspNet10.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailService emailService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(200 , Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult SendEmail([FromBody] EmailRequestDTO emailRequest)
        {
            _logger.LogInformation("Sending email to {to}",  emailRequest.To);
            _emailService.SendSimpleEmail(emailRequest);
            return Ok("Email sent successfully");
        }


        [HttpPost("with-attachment")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendEmailWithAttachment([FromForm] string emailRequest, [FromForm] FileUploadDTO attachment)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            EmailRequestDTO emailRequestDto = JsonSerializer.Deserialize<EmailRequestDTO>(emailRequest, options);

            if(emailRequestDto == null)
            {
                _logger.LogWarning("Email request is null or invalid");
                return BadRequest("Email request is null or invalid");
            }


            if (attachment?.File == null || attachment?.File.Length == 0)
            {
                _logger.LogWarning("Attachment is null or empty");
                return BadRequest("Attachment is null or empty");
            }

            _logger.LogInformation("Sending email with attachment to {to}",emailRequestDto.To );
            await _emailService.SendEmailWithAttachment(emailRequestDto, attachment.File);
            return Ok("Email with attachment sent successfully");
        }
    }
}

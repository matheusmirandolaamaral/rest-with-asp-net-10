using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Data.DTO.V1;
using RestWithAspNet10.Service;


namespace RestWithAspNet10.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            _logger.LogInformation("Fetching all books");
            return Ok(_bookService.FindAll());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            _logger.LogInformation("Fetching books with ID {id}", id);
            var book = _bookService.FindById(id);
            if(book == null)
            {
                _logger.LogWarning("Book with ID {id} not found", id);
                return NotFound();
            }
            return Ok(book);
        }


        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post(BookDTO book)
        {
            _logger.LogInformation("Creating book");
            var books = _bookService.Create(book);
            if(books == null)
            {
                _logger.LogError("Failed to create book");
                return NotFound();
            }
            Response.Headers.Add("X-API-Deprecated", "true");
            Response.Headers.Add("X-API-Deprecation-Date", "2026-12-31");
            return Ok(books);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put(BookDTO book)
        {
            _logger.LogInformation("Updating book with ID {id}", book.Id);
            var updatebook = _bookService.Update(book);
            if(updatebook == null)
            {
                _logger.LogError("Failed to update book with ID {id}", book.Id);
                return NotFound();
            }
            return Ok(updatebook);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting book with ID {id}", id);
            _bookService.Delete(id);
            _logger.LogDebug("Book with ID {id} deleted sucessfully", id);
            return NoContent();
        }
    }

    
}

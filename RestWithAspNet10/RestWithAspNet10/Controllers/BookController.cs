using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10.Data.DTO;
using RestWithAspNet10.Model;
using RestWithAspNet10.Service;


namespace RestWithAspNet10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public IActionResult Get()
        {
            _logger.LogInformation("Fetching all books");
            return Ok(_bookService.FindAll());
        }
        [HttpGet("{id}")]
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
        public IActionResult Post(BookDTO book)
        {
            _logger.LogInformation("Creating book");
            var books = _bookService.Create(book);
            if(books == null)
            {
                _logger.LogError("Failed to create book");
                return NotFound();
            }
            return Ok(books);
        }

        [HttpPut]
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
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting book with ID {id}", id);
            _bookService.Delete(id);
            _logger.LogDebug("Book with ID {id} deleted sucessfully", id);
            return NoContent();
        }
    }

    
}

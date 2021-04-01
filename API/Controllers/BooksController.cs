using System.Threading.Tasks;
using API.Services.BookService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await _bookService.GetAllBooks();
            return Ok(response);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetBooksByCategory(string category)
        {
            var response = await _bookService.GetBooksByCategory(category);
            return Ok(response);
        }

    }

}
using System.Threading.Tasks;
using API.DTO.Book;
using API.Models;
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

        [HttpPost]
        public async Task<IActionResult> AddNewBook(AddNewBookDTO newBookDTO)
        {
            var response = await _bookService.AddNewBook(newBookDTO);
            return Ok(response);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetBooksByCategory(string category)
        {
            var response = await _bookService.GetBooksByCategory(category);
            return Ok(response);
        }

        [HttpPost("{bookId}")]
        public async Task<IActionResult> UpdateBook(UpdateBookDTO updateBook, int bookId)
        {
            var response = await _bookService.UpdateBook(updateBook, bookId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var response = await _bookService.DeleteBook(bookId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }

}
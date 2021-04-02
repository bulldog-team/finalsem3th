using System.Threading.Tasks;
using API.DTO.Book;
using API.Models;
using API.Services.BookService;
using API.Services.PhotoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IPhotoService _photoService;

        public BooksController(IBookService bookService, IPhotoService photoService)
        {
            _bookService = bookService;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await _bookService.GetAllBooks();
            return Ok(response);
        }

        [Authorize(Roles = "Admin, User")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost("add-photo/{bookId}")]
        public async Task<IActionResult> AddPhoto(int bookId, IFormFile file)
        {
            var response = await _bookService.AddPhoto(bookId, file);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("add-photo/{bookId}")]
        public async Task<IActionResult> DeletePhoto(int bookId, string publicId)
        {
            var response = await _bookService.DeletePhoto(bookId, publicId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin, User")]
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

        [Authorize(Roles = "Admin, User")]
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
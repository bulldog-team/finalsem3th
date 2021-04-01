using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Book;
using API.Models;

namespace API.Services.BookService
{
    public interface IBookService
    {
        Task<ResponseServiceModel<IEnumerable<GetBookDTO>>> GetAllBooks();

        Task<ResponseServiceModel<IEnumerable<GetBookDTO>>> GetBooksByCategory(string categoryName);
    }
}
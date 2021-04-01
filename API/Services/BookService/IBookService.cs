using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Book;
using API.Models;

namespace API.Services.BookService
{
    public interface IBookService
    {
        Task<ResponseServiceModel<IEnumerable<GetBookDTO>>> GetAllBooks();
    }
}
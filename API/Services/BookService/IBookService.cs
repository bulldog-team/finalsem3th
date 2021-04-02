using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Book;
using API.Models;
using Microsoft.AspNetCore.Http;

namespace API.Services.BookService
{
    public interface IBookService
    {
        Task<ResponseServiceModel<IEnumerable<GetBookDTO>>> GetAllBooks();

        Task<ResponseServiceModel<IEnumerable<GetBookDTO>>> GetBooksByCategory(string categoryName);

        Task<ResponseServiceModel<AddNewBookDTO>> AddNewBook(AddNewBookDTO book);

        Task<ResponseServiceModel<UpdateBookDTO>> UpdateBook(UpdateBookDTO updateBook, int bookId);

        Task<ResponseServiceModel<UpdateBookDTO>> DeleteBook(int bookId);

        Task<ResponseServiceModel<UpdateBookDTO>> AddPhoto(int bookId, IFormFile file);
    }
}
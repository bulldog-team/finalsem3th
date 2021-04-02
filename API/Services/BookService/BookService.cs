using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.Book;
using API.Models;
using API.Services.PhotoService;
using AutoMapper;
using BackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IConfiguration _config;
        public BookService(IConfiguration config, DataContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
            _config = config;
        }

        public async Task<ResponseServiceModel<AddNewBookDTO>> AddNewBook(AddNewBookDTO book)
        {
            var response = new ResponseServiceModel<AddNewBookDTO>();
            var categoryModel = await _context.categoryModels.FirstOrDefaultAsync(x => x.Category.Equals(book.Category));
            if (categoryModel == null)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            var newBook = new BookModel
            {
                Author = book.Author,
                BookName = book.BookName,
                CategoryId = categoryModel.CategoryId,
                Description = book.Description,
                Thumbnail = book.Thumbnail
            };
            await _context.BookModels.AddAsync(newBook);
            await _context.SaveChangesAsync();
            response.Data = book;

            return response;
        }

        public async Task<ResponseServiceModel<UpdateBookDTO>> AddPhoto(int bookId, IFormFile file)
        {
            var response = new ResponseServiceModel<UpdateBookDTO>();
            var result = await _photoService.AddPhotoAsync(file);

            var book = await _context.BookModels.FirstOrDefaultAsync(c => c.BookId == bookId);

            if (result.Error != null || book == null)
            {
                response.Success = false;
                response.Message = "Something wrong!";
                return response;
            }

            book.Thumbnail = result.SecureUrl.AbsoluteUri;
            book.ThumnailId = result.PublicId;
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<UpdateBookDTO>(book);
            return response;
        }

        public async Task<ResponseServiceModel<UpdateBookDTO>> DeleteBook(int bookId)
        {
            var response = new ResponseServiceModel<UpdateBookDTO>();
            var book = await _context.BookModels.FirstOrDefaultAsync(c => c.BookId == bookId);
            if (book == null)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            _context.BookModels.Remove(book);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<UpdateBookDTO>(book);
            return response;
        }

        public async Task<ResponseServiceModel<UpdateBookDTO>> DeletePhoto(int bookId, string publicId)
        {
            var response = new ResponseServiceModel<UpdateBookDTO>();
            var book = await _context.BookModels.FirstOrDefaultAsync(c => c.BookId == bookId);
            if (book == null || book.ThumnailId != publicId)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }
            var result = await _photoService.DeletePhotoAsync(publicId);
            book.Thumbnail = "";
            book.ThumnailId = "";
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<UpdateBookDTO>(book);
            return response;
        }

        public async Task<ResponseServiceModel<IEnumerable<GetBookDTO>>> GetAllBooks()
        {
            var response = new ResponseServiceModel<IEnumerable<GetBookDTO>>();
            var books = await _context.BookModels.Include(c => c.categoryModel).Select(x => new GetBookDTO
            {
                Author = x.Author,
                Category = x.categoryModel.Category,
                Description = x.Description,
                BookName = x.BookName,
                Thumbnail = x.Thumbnail
            }).ToListAsync<GetBookDTO>();
            response.Data = _mapper.Map<IEnumerable<GetBookDTO>>(books);
            return response;
        }

        public async Task<ResponseServiceModel<IEnumerable<GetBookDTO>>> GetBooksByCategory(string categoryName)
        {
            var response = new ResponseServiceModel<IEnumerable<GetBookDTO>>();

            var books = await _context.BookModels.Include(c => c.categoryModel).Where(c => c.categoryModel.Category.Equals(categoryName)).ToListAsync();

            response.Data = _mapper.Map<IEnumerable<GetBookDTO>>(books);
            return response;
        }

        public async Task<ResponseServiceModel<UpdateBookDTO>> UpdateBook(UpdateBookDTO updateBook, int bookId)
        {
            var response = new ResponseServiceModel<UpdateBookDTO>();
            var category = await _context.categoryModels.FirstOrDefaultAsync(c => c.Category.Equals(updateBook.Category));
            var bookInDatabase = await _context.BookModels.FirstOrDefaultAsync(c => c.BookId == bookId);
            if (bookInDatabase == null || category == null)
            {
                response.Success = false;
                response.Message = "Something wrongs!";
                return response;
            }

            bookInDatabase.Author = updateBook.Author;
            bookInDatabase.BookName = updateBook.BookName;
            bookInDatabase.CategoryId = category.CategoryId;
            bookInDatabase.Description = updateBook.Description;
            bookInDatabase.Thumbnail = updateBook.Thumbnail;

            _context.BookModels.Update(bookInDatabase);
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<UpdateBookDTO>(bookInDatabase);
            return response;
        }

    }
}
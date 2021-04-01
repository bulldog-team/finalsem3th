using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO.Book;
using API.Models;
using AutoMapper;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public BookService(IConfiguration config, DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
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
    }
}
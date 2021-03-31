using API.DTO.Book;
using API.Models;
using AutoMapper;

namespace API.ProfileManager
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<GetBookDTO, BookModel>();
        }
    }
}